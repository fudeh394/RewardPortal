using System;
using System.Collections.Generic;
using System.Linq;
using RewardSystemWeb.Models;
using RewardSystemWeb.Entities;
using RewardSystemWeb.Interfaces;

namespace RewardSystemWeb.Services
{
    public class LoginService : ILoginService
    {
       
        IUserRepository UserService;
        IAuditsRepository AuditsService;
        IAdService AdService;
        private readonly Database _db;
        public  LoginService(Database db,IUserRepository userService, IAuditsRepository auditsService, IAdService adService)
        {           
          
            UserService = userService;
            AuditsService = auditsService;
            AdService = adService;
            _db = db;
        }


        public List<AuditsModel> GetRecentActivities(string userId, string country)
        {
            return AuditsService.GetAllActivities().Where(y=>y.CreatedBy.Equals(userId,StringComparison.CurrentCultureIgnoreCase)).Select(x=>new AuditsModel {  AuditDescription = x.AuditDescription, TimeStamp = x.CreatedDate, Name =x.CreatedBy} ).OrderByDescending(y=>y.TimeStamp).ToList();
        }

        /// <summary>
        /// Authentic User and Token via AD and Entrust
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool AuthenticateUser(string userid, string password, string token)
        {
            var result = true;  
            return result;
        }

       /// <summary>
       /// Get User Detail from AD
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        private User GetUserDetail(LoginModel model)
        {
            //check if user exist
            //var result = UserService.GetAllUsers().Where(x => !x.IsDeleted && x.UserId.Equals(model.UserId,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault<User>();
            //if(result != null && !string.IsNullOrEmpty(result.UserName))               
            //    return result;
            //result = AdService.GetUserDetail(model.UserId, model.Token);
            //if (result != null && !string.IsNullOrEmpty(result.UserName))
            //{
            //    result.LastLoginDate = DateTime.Now;
            //   // result.UserStatus = Constants.USER_LOGIN_STATUS_ACTIVE;
            //    result.IsDeleted = false;
            //    result.Id= UserService.AddUserAndGetId(result);

            //}

            var result = new User
            {
                ApprovedBy = "test1",
                ApprovedDate = DateTime.Now,
                CreatedBy = "Test1",
                CreatedDate = DateTime.Now,
                Id = 1,
                IsDeleted = false,
                LastLoginDate = DateTime.Now,
                RequestStatus = 1,
                RoleId = 1,
                SolId = "1",
                UserId = "test",
                UserName = "test",
                UserRole = "Inputer"
            };
            return result;
        }

        public CurrentUser ValidateCredential(LoginModel model, string userHostAddress, ref string error)
        {
            var user = new CurrentUser();
            var events = new Event();

            try
            {               
                error = "Wrong username or password";
                if (this.AuthenticateUser(model.UserId, model.Password, model.Token))
                {
                    var userDetail = this.GetUserDetail(model);
                    if((userDetail==null)||string.IsNullOrEmpty(userDetail.UserRole))
                    {
                        Logger.Error($"ValidateCredential::failed");
                        return null;
                    }





                    user = new CurrentUser
                    {
                        FirstName = userDetail.UserName,
                        SolId = userDetail.SolId,
                        LastLogin = DateTime.Now,
                        UserId = model.UserId,
                        Status = Constants.USER_LOGIN_STATUS_ACTIVE,
                        Role = userDetail.RoleId
                    };
                }
                else
                {
                    Logger.Error($"ValidateCredential::failed");
                    return null;
                }

                //Logger activity
                AuditsService.AddActivity(
                    new Audit
                    {
                        AuditDescription = $"Logged in successfully @{userHostAddress}",
                        //Name = "Login",
                        EventId = events.Id,
                        CreatedBy = user.UserId,
                        CreatedDate = DateTime.Now,
                        Comment = events.EventDescription
                    }
                    );
            }
            catch (Exception e)
            {
                Logger.Error($"ValidateCredential::" + model.SolId +"|||"+model.UserId , e);
            }
            return user;
        }



        public LoginResult GetAppInfo(LoginModel model, ref string error)
        {
            var loginResult = new LoginResult();
            try
            {
               loginResult = RetrieveUserInformation(UserService.GetAllUsers().Where(x=>x.UserId.Equals(model.UserId,StringComparison.OrdinalIgnoreCase)).FirstOrDefault<User>().RoleId);
            }
            catch (Exception  e)
            {
               Logger.Error("Error something went wrong", e);
            }

            return loginResult;
        }

        private LoginResult RetrieveUserInformation(int role)
        {
            try
            {
                var loginResult = new LoginResult();
                //var loginDetail = new MenuDetail();
                //var result = new List<ResourceGroup>();
                //object[] colors = { "green", "blue", "purple", "yellow" };


                //var rec = (from a in _db.Resources
                //join ar in _db.RoleResources on a.ResourceId equals ar.ResourceId
                //           join g in _db.Groups on a.GroupId equals g.Id
                //           join r in _db.Roles  on ar.RoleId equals r.RoleId
                //           where r.RoleId == role && ar.IsDeleted == false && r.IsDeleted == false
                //           orderby g.Id descending

                //           select new
                //           {
                //               Id = a.ResourceId,
                //               Name = a.ResourceName,
                //               Group = a.GroupId,
                //               Description = a.ResourceName,
                //               a.IsDeleted,
                //               TileIcon = a.Icon,
                //               TileColor = a.Color,
                //               Url = a.ResourceUrl,
                //               g.GroupName,
                //               r.RoleName

                //           }
                //          );



                //var departments = rec.Select(x => x.GroupName).Distinct();
                //int j = 0;
                //foreach (var dept in departments as List<string>)
                //{
                //    //department here is groups : a change caused this naming issue
                //    var departmnt = new ResourceGroup
                //    {
                //        Name = dept,
                //        Id = j,
                //        apps = rec.Where(y => y.GroupName == dept)
                //        .Select(x => new ResourceObject()
                //        {
                             
                //            Group = x.GroupName,
                //            Name = x.Name,
                //            Description = x.Description,
                //            Id = x.Id,
                //            IsDeleted = x.IsDeleted,
                //            //Logo_Url = x.Logo_Url,
                //            TileColor = x.TileColor,
                //            TileIcon = x.TileIcon,
                //            Url = x.Url
                //        }).ToList<ResourceObject>()
                //    };
                //    departmnt.apps[0].TileType = 0;
                //    result.Add(departmnt);
                //    j++;

                //}


                //loginDetail.UserInfo = new User() { };
                //loginDetail.Groups = result;
                //loginResult.LoginInfo = loginDetail;
                return loginResult;


            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
