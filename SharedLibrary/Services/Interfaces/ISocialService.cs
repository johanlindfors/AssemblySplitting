using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services.Interfaces
{
    public interface ISocialService
    {
        Task<SocialResponse<UserInfo>> SignIn();
        Task<bool> ValidateToken();
        Task<bool> SignOut();
    }
}
