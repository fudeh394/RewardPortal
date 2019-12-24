using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Models
{
    public class CurrentUser
    {
        public string UserId { set; get; }
        public int Role { set; get; }
        public string SolId { set; get; }
        public string Country { set; get; }       
        public string FirstName { get; set; }
        public string Status { get; set; }
        public DateTime LastLogin { get; set; }
    }

 
    public class PortalSetting
    {
        public int Id { get; set; }
        public bool IsWindowActive { set; get; }
        public int RequestStatus { set; get; }
        public bool IsTestMode { set; get; }
        public bool IsDeleted { set; get; }
        public bool IsCurrentWindow { set; get; }
        public string ApprovedBy { get; set; }
        public string WindowName { get; set; }
        public DateTime WindowStartDate { get; set; } = DateTime.Now;
        public DateTime WindowEndDate { get; set; } = DateTime.Now;
        public DateTime ApprovedDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
    }

    public class AuditsModel
    {
        public DateTime TimeStamp { get; set; }
        public string AuditDescription { get; set; }
        public string Name { get; set; }
    }

    public class LoginModel
    {
       
        public string SolId { set; get; }        
        public string UserId { set; get; }       
        public string Password { set; get; }       
        public string Token { set; get; }
        public string TokenSerialNo { set; get; }
    }
    public class LoginResult
    {
        public bool Status { get; set; }
        public string UserRole { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public MenuDetail LoginInfo { get; set; }
    }
    public class ResourceGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ResourceObject> apps { get; set; }
    }
    public class MenuDetail
    {
        public User UserInfo { get; set; }

        public IList<ResourceGroup> Groups { get; set; }
    }


    public class ResourceObject
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public string TileIcon { get; set; }
        public string TileColor { get; set; }
        public int TileType { get; set; }        
    }

    public class LoginResultObject : Resource
    {

        public string RoleName { get; set; }
        public string GroupName { get; set; }


    }
}
