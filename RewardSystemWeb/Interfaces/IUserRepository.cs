using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Interfaces
{
    public interface IUserRepository
    {
        IList<User> GetAllUsers();      
        User GetUserId(int idUser);
        void AddUser(User user);
        int AddUserAndGetId(User user);
        void UpdateUser(int id, User user);
        void DelecteUser(int idUser);
    }
}
