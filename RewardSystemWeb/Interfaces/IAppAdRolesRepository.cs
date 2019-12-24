using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Interfaces
{
    

    public interface IAppAdRolesRepository
    {
        IList<AppAdRole> AppAdRoles();
        int GetApprole(string adRole);
        
    }
}
