using Facebook;
using Facebook.Client;
using SharedLibrary.Models;
using SharedLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Facades.Services
{
    public class FacebookService : ISocialService
    {
        const string APP_ID = "118837091466025"; // TODO: Replace AppId with proper Facebook identification
        const string REDIRECT_URL = "https://www.facebook.com/connect/login_success.html";
        const string TOKEN_PARAMETER = @"access_token=";
        const string EXPIRES_PARAMETER = @"expires_in=";
        const string FACEBOOK_TOKEN_SETTING = "facebook_token";
        const string FACEBOOK_TOKEN_EXPIRES_AT_SETTING = "facebook_token_expires_at";

        readonly ISettingsService settings;

        public FacebookService(ISettingsService settings)
        {
            this.settings = settings;
        }

        public async Task<SocialResponse<UserInfo>> SignIn()
        {
            SocialResponse<UserInfo> result = new SocialResponse<UserInfo>();

            try
            {
                var facebookClient = new FacebookSessionClient(APP_ID);
                var session = await facebookClient.LoginAsync();

                if (!string.IsNullOrEmpty(session.AccessToken))
                {
                    result.Result = true;
                    settings.Set(FACEBOOK_TOKEN_SETTING, session.AccessToken);
                    settings.Set(FACEBOOK_TOKEN_EXPIRES_AT_SETTING, session.Expires.ToFileTimeUtc());
                    var userInfo = await GetUserInfo();
                }
                Debug.WriteLine(session.AccessToken);
                Debug.WriteLine(session.FacebookId);
                Debug.WriteLine(session.Expires);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result.Result = false;
            }
            return result;
        }

        private DateTime ParseAuthenticationTokenExpiresAt(string data)
        {
            var result = DateTime.UtcNow;
            try
            {
                var tokenEndsAtIndex = data.IndexOf(EXPIRES_PARAMETER) + EXPIRES_PARAMETER.Length;
                var secondsAsString = data.Substring(tokenEndsAtIndex, data.Length - tokenEndsAtIndex);
                result = DateTime.UtcNow.AddSeconds(double.Parse(secondsAsString));
            }
            catch { }
            return result;
        }

        private string ParseAuthenticationToken(string data)
        {
            var result = string.Empty;
            try
            {
                var tokenBeginsAtIndex = data.IndexOf(TOKEN_PARAMETER) + TOKEN_PARAMETER.Length;
                var tokenEndsAtIndex = data.IndexOf(EXPIRES_PARAMETER);
                result = data.Substring(tokenBeginsAtIndex, tokenEndsAtIndex - tokenBeginsAtIndex - 1);
            }
            catch { }
            return result;
        }

        /*
         {"id":"605294645","name":"Johan Lindfors","first_name":"Johan","last_name":"Lindfors","link":"https://www.facebook.com/johan.lindfors","username":"johan.lindfors","hometown":{"id":"106505586052951","name":"Stockholm, Sweden"},"location":{"id":"103138059726221","name":"Vallentuna"},"work":[{"employer":{"id":"141300472625433","name":"Coderox AB"},"location":{"id":"106505586052951","name":"Stockholm, Sweden"},"position":{"id":"138050029568986","name":"Owner and Founder"},"description":"Coderox AB focuses on building amazing applications and solutions on the Windows Phone 7 platform and ecosystem.","start_date":"2011-09-01"},{"employer":{"id":"230345967120333","name":"Infozone AB"},"location":{"id":"106505586052951","name":"Stockholm, Sweden"},"position":{"id":"108366822551652","name":"Head of Development"},"description":"Leading the Development Business Unit","start_date":"2011-01-01","end_date":"2011-08-01"},{"employer":{"id":"20528438720","name":"Microsoft"},"position":{"id":"186297501410305","name":"Technical Evangelism Manager"},"description":"Ansvarar för den tekniska evangelisering av Microsofts produkter och tekniker mot utvecklare.","start_date":"1998-04-01","end_date":"2010-12-01"}],"education":[{"school":{"id":"111037798920964","name":"Soltorgsgymnasiet"},"type":"High School"},{"school":{"id":"107637795932083","name":"Högskolan Dalarna"},"year":{"id":"137409666290034","name":"1995"},"concentration":[{"id":"163891336996704","name":"Datateknik"}],"type":"College"}],"gender":"male","email":"johan.lindfors@live.com","timezone":2,"locale":"en_US","verified":true,"updated_time":"2013-09-22T12:24:26+0000"}
         */
        private async Task<UserInfo> GetUserInfo()
        {
            UserInfo result = null;
            try
            {
                var accessToken = settings.Get<string>(FACEBOOK_TOKEN_SETTING);
                if (!string.IsNullOrEmpty(accessToken))
                {
                    var facebookClient = new FacebookClient(accessToken);
                    dynamic userInfo = await facebookClient.GetTaskAsync("me");
                    result = new UserInfo()
                    {
                        Id = int.Parse(userInfo.id),
                        Name = userInfo.name,
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        /*
        {
        "data": {
            "app_id": 138483919580948, 
            "application": "Social Cafe", 
            "expires_at": 1352419328, 
            "is_valid": true, 
            "issued_at": 1347235328, 
            "metadata": {
                "sso": "iphone-safari"
            }, 
            "scopes": [
                "email", 
                "publish_actions"
            ], 
            "user_id": 1207059
            }
        }
        */
        public async Task<bool> ValidateToken()
        {
            bool result = false;
            try
            {
                var accessToken = settings.Get<string>(FACEBOOK_TOKEN_SETTING);
                if (!string.IsNullOrEmpty(accessToken))
                {
                    var facebookClient = new FacebookClient(accessToken);
                    dynamic validationResult = await facebookClient.GetTaskAsync("debug_token", new
                    {
                        input_token = accessToken,
                        access_token = accessToken
                    });
                    result = validationResult.data.is_valid;
                    if (result)
                    {
                        var userInfo = await GetUserInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task<bool> SignOut()
        {
            var result = false;
            try
            {
                settings.Remove(FACEBOOK_TOKEN_SETTING);

                // TODO: Send logout event
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
