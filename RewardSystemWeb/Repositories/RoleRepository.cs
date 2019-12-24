using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RewardSystemWeb.Entities;

namespace RewardSystemWeb.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        Database Connection;
        public RoleRepository(Database dbFactory)
        {
            Connection = dbFactory;
        }

        public void AddRole(Role role)
        {

            Connection.Roles.Add(role);
            //Connection.Insert<Role>(role);
        }

        public void DelecteRole(int idRole)
        {
            //Connection.Roles.Remove()
           // Connection.Roles.Remove()
           //Connection.Delete<Role>(idRole);
        }

        public IList<Role> GetAllRoles()
        {
            string query = "SELECT * FROM Roles Where isDeleted = 0 and RequestStatus=1";

            IList<Role> roleList = Connection.Roles.SqlQuery(query).ToList<Role>();
            //IList <Role> roleList = Connection.Fetch<Role>(query);

            return roleList;
        }

        public Role GetRoleId(int idRole)
        {
            Role role = Connection.Roles.Find(idRole);
            return role;
        }

        public void UpdateRole(int id, Role role)
        {
            role.RoleId = id;
            var res = Connection.Roles.Find(id);
            res = role;
            res.RoleId =id;
            Connection.SaveChanges();
        }
    }
}
