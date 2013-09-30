using Facebook;
using SharedLibrary.Models;
using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace Facades.Services
{
    public class FacebookService : ISocialService
    {
        const string APP_ID = "118837091466025";
        const string REDIRECT_URL = "https://www.facebook.com/connect/login_success.html";
        const string TOKEN_PARAMETER = @"access_token=";
        const string EXPIRES_PARAMETER = @"expires_in=";
        const string FACEBOOK_TOKEN_SETTING = "facebook_token";

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
                var facebookClient = new FacebookClient();
                var requestUri = facebookClient.GetLoginUrl(new
                {
                    client_id = APP_ID,
                    redirect_uri = REDIRECT_URL,
                    response_type = "token",
                });

                var callbackUri = new Uri(REDIRECT_URL);
                var authenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.None,
                    requestUri, callbackUri
                    );

                switch (authenticationResult.ResponseStatus)
                {
                    case WebAuthenticationStatus.Success:
                        var accessToken = ParseAuthenticationToken(authenticationResult.ResponseData);
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            result.Result = true;
                            settings.Set(FACEBOOK_TOKEN_SETTING, accessToken);
                        }
                        break;
                    case WebAuthenticationStatus.ErrorHttp:
                    case WebAuthenticationStatus.UserCancel:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result.Result = false;
            }
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
                    dynamic validationResult = await facebookClient.GetTaskAsync("debug_token", new { 
                        input_token = accessToken, 
                        access_token = accessToken 
                    });
                    result = validationResult.data.is_valid;
                }
            }
            catch(Exception ex)
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
