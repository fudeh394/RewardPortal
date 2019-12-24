using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RewardSystemWeb.Entities;

namespace RewardSystemWeb.Repositories
{
    public class AppAdRolesRepository : IAppAdRolesRepository
    {


        Database Connection;
        public AppAdRolesRepository(Database dbFactory)
        {
            Connection = dbFactory;
        }
        public IList<AppAdRole> AppAdRoles()
        {
            return null;
            //string query = "select * from App_To_AdRoles";
            //IList<AppAdRole> resourceList = Connection.ap.(query);
            //return resourceList;
        }

        public int GetApprole(string adRole)
        {
            var result = 0;
            try
            {
                result = this.AppAdRoles().Where(x => x.FinacleRole.Equals(adRole, StringComparison.OrdinalIgnoreCase)).FirstOrDefault<AppAdRole>().AppRole;

            }
            catch (Exception ex)
            {
                Logger.Error("User Ad Role not mapped", ex);
            }

            return result;
       }
    }
}
