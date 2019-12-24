using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RewardSystemWeb.Models;

namespace RewardSystemWeb.Interfaces
{
    public interface IAuditsRepository
    {

        IList<Audit> GetAllActivities();
        Audit GetActivityId(int idActivity);
        void AddActivity(Audit audits);
        void UpdateActivity(int id, Audit audits);
        void DelecteActivity(int idActivity);
    }
}
