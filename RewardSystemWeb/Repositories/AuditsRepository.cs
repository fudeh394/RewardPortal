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
    public class AuditsRepository : IAuditsRepository
    {

        Database Connection;
        public AuditsRepository(Database dbFactory)
        {
            Connection = dbFactory;
        }

        public void AddActivity(Activity activity)
        {
            //Connection.Audits
            //Connection.Insert<Activity>(activity);
        }

        public void AddActivity(Audit audits)
        {
            //Connection.Audits.Add(audits);
            //Connection.SaveChanges();
            //Connection.Insert<Audit>(audits);
        }

        public void DelecteActivity(int idActivity)
        {
            //Connection.Delete<Activity>(idActivity);
        }

        public Audit GetActivityId(int idActivity)
        {
            //Audit activity = Connection.Audits.Find(idActivity);
            //return activity;
            return new Audit { AuditDescription = "", BvnrequestId = 1, Comment = "Somethings", CreatedBy = "test", CreatedDate = DateTime.Now, EventId = 1, Id = 1 };


        }

        public IList<Audit> GetAllActivities()
        {
            //string query = "SELECT * FROM Audits";
            //IList<Audit> resourceList = Connection.Audits.SqlQuery(query).ToList();

            var resourceList = new List<Audit>
            {
                 new Audit{  AuditDescription = "", BvnrequestId = 1, Comment ="Somethings", CreatedBy = "test", CreatedDate = DateTime.Now, EventId = 1, Id = 1}
            };
            return resourceList;
        }

        public void UpdateActivity(int id, Audit audits)
        {
            //audits.Id = id;
            //var res = Connection.Audits.Find(id);
            //res = audits;
            //Connection.SaveChanges();
               
            //Connection.Update(audits);
        }



       


        

      

       

       
    }
}
