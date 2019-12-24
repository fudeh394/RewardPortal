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
    public class ResourceRepository : IResourceRepository
    {
        Database Connection;
        public ResourceRepository(Database dbFactory)
        {
            Connection = dbFactory;
        }

        public void AddResource(Resource resource)
        {
            //Connection.Insert<Resource>(resource);
            Connection.Resources.Add(resource);
            Connection.SaveChanges();
        }

        public void DelecteResource(int idResource)
        {
            //var res = Connection.
            //Connection.Delete<Resource>(idResource);
        }

        public IList<Resource> GetAllResources()
        {
            string query = "SELECT * FROM Resources";
            IList<Resource> resourceList = Connection.Resources.SqlQuery(query).ToList();
            //IList<Resource> resourceList = Connection.Fetch<Resource>(query);

            return resourceList;
        }

        public Resource GetResourceId(int idResource)
        {
            //Resource resource = Connection.SingleById<Resource>(idResource);
            Resource resource = Connection.Resources.Find(idResource);
            return resource;
        }

        public void UpdateResource(int id, Resource resource)
        {
            resource.ResourceId = id;
            var res = Connection.Resources.Find(id);
            res = resource;
            Connection.SaveChanges();
        }
    }
}
