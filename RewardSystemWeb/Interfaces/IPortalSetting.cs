using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Interfaces
{
    public interface IPortalSetting
    {
        List<PortalSetting> GetAllItems();
        PortalSetting GetItemId(int Id);
        int AddItem(PortalSetting item,string userName, string solId, ref string message);        
        void UpdateItem(int id, PortalSetting item);
        void DeleteItem(int id);
        bool AuthItem(long Id, string userName, string solId, ref string message);
        bool DeclineItem(long Id, string userName, string solId, ref string message);
    }
}
