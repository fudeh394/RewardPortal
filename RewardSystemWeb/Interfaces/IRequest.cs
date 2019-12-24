using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Interfaces
{
    public interface IRequest
    {

        List<CreatePost> GetPendingItems(string userName, string solId);
        CreatePost GetRequest(long id1, string solId);
        //long AddItem(Request model, string userName, string solId, ref string message);
        //long UpdateItem(Request model, string userName, string solId, ref string message);
        long AddItem(CreatePost  model, string userName, string solId, ref string message);
        bool AuthItem(long Id, string userName, string solId, ref string message);
        long UpdateItem(CreatePost model, string userName, string solId, ref string message);

        CreatePost GetConfirmationItem(long id, string userName, string solId);
        bool ConfirmItem(long id, string userName, string solId, ref string message);
        List<CreatePost> ConfirmedItems(string userName, string solId);


    }

    public interface IHeadOfficeRequests
    {

        List<CreatePost> GetPendingItems(string userName, string solId);
        CreatePost GetRequest(long id1, string solId);
        //long AddItem(Request model, string userName, string solId, ref string message);
        //long UpdateItem(Request model, string userName, string solId, ref string message);
        long AddItem(CreatePost model, string userName, string solId, ref string message);
        bool AuthItem(long Id, string userName, string solId, ref string message);
        long UpdateItem(CreatePost model, string userName, string solId, ref string message);

        CreatePost GetConfirmationItem(long id, string userName, string solId);
        bool ConfirmItem(long id, string userName, string solId, ref string message);
        List<CreatePost> ConfirmedItems(string userName, string solId);


    }

    
}
