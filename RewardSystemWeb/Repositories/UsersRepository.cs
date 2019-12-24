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
    public class UsersRepository : IUserRepository
    {
        Database Connection;
        public UsersRepository(Database dbFactory)
        {
            Connection = dbFactory;

        }

        public void AddUser(User user)
        {
            try
            {
                Connection.Users.Add(user);
                Connection.SaveChanges();

            }
            catch (Exception e)
            {
                Logger.Error("ERROR ON SAVING FRESH USER", e);
                throw;
            }
        }

        public int AddUserAndGetId(User user)
        {
            try
            {
                return Convert.ToInt32(Connection.Users.Add(user));
               

            }
            catch (Exception e)
            {
                Logger.Error("ERROR ON SAVING FRESH USER", e);
                throw;
            }
        }

        public void DelecteUser(int idUser)
        {
            var req = Connection.Users.Find(idUser);
            req.IsDeleted = true;
            Connection.SaveChanges();
            
        }

        public IList<User> GetAllUsers()
        {
            string query = "SELECT * FROM Users where IsDeleted = 0";
            IList<User> userList = Connection.Users.SqlQuery(query).ToList<User>();

           

            return userList;
        }

        

        public User GetUserId(int idUser)
        {
            User user = Connection.Users.FirstOrDefault(x=>x.Id == idUser && !x.IsDeleted);
            return user;
        }

        public void UpdateUser(int id, User user)
        {
            user.Id = id;
            var currUser = Connection.Users.Find(user);
           
            Connection.SaveChanges();
        }
    }
}
