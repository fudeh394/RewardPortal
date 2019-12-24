using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RewardSystemWeb.Services
{
    public class AppAdService : IAdService
    {
      
      
        IAppAdRolesRepository AppAdRolesRepository;
       

        public AppAdService(IAppAdRolesRepository appAdRolesRepository)
        {
          AppAdRolesRepository = appAdRolesRepository;
        }
           


        public bool Authenticate(string userid, string password)
        {
            if (string.IsNullOrEmpty(userid))
                return false;

            if (string.IsNullOrEmpty(password))
                return false;
            else
                return true;
            
        }

        public User GetUserDetail(string userid, string serialNumber)
        {

            return new User { UserId = userid, UserName = userid };

            //return new User
            //{
            //    UserId = userid,
            //    UserName = Convert.ToString(response.Result.Body.ADUserDetailsResult).Split('|')[0].ToString(),
            //    SolId  = userDetail.SolId != null ? userDetail.SolId : "099",
            //    UserRole = userDetail.UserRole != null ? userDetail.UserRole : "HeadOfficeUser",
            //    RoleId= userDetail.RoleId
            //};
        }

        public bool VerifyToken(string userid, string serialNumber)
        {
            //
            return true;
        }


        

        
    }
}
