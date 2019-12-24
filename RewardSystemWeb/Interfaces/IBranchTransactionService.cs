using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardSystemWeb.Interfaces
{
    public interface IBranchTransactionService
    {

        List<UploadModel> GetItemsByBatchNumber(Guid bacthNumber);
        List<UploadModel> GetItems(string solId);
        UploadModel GetRequest(long id1, string solId);
        long AddItem(UploadModel model, string userName, string solId, ref string message);
        void AddItems(List<UploadModel> model, string userName, string solId, ref string message);
        long UpdateItem(UploadModel model, string userName, string solId, ref string message);
       

    }

    public interface IHeadOfficeTransactionService
    {

        List<VerifiedRecord> GetItemsByBatchNumber(Guid bacthNumber);
        List<VerifiedRecord> GetItems(string solId);
        VerifiedRecord  GetRequest(long id);
        long AddItem(VerifiedRecord model, string userName, string solId, ref string message);
        void AddItems(List<VerifiedRecord> models, string userName, string solId, ref string message);
        long UpdateItem(VerifiedRecord model, string userName, string solId, ref string message);


    }
}
