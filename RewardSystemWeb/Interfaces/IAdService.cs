using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RewardSystemWeb.Models;

namespace RewardSystemWeb.Interfaces
{
    public interface IAdService
    {

        bool Authenticate(string userid, string password);
        bool VerifyToken(string userid, string serialNumber);
        Models.User GetUserDetail(string userid, string serialNumber);
    }

    public interface IRoleRepository
    {
        IList<Role> GetAllRoles();
        Role GetRoleId(int idRole);
        void AddRole(Role role);
        void UpdateRole(int id, Role role);
        void DelecteRole(int idRole);
    }

    public interface IResourceRepository
    {
        IList<Resource> GetAllResources();
        Resource GetResourceId(int idResource);
        void AddResource(Resource resource);
        void UpdateResource(int id, Resource resource);
        void DelecteResource(int idResource);
    }
}
