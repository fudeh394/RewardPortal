using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Interfaces
{
    public interface ILoginService
    {      

        CurrentUser ValidateCredential(LoginModel model, string userHostAddress, ref string error);
        List<AuditsModel> GetRecentActivities(string userId, string country);
        LoginResult GetAppInfo(LoginModel model, ref string error);
    }
}
