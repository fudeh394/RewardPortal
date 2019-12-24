using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RewardSystemWeb.Entities;

namespace RewardSystemWeb.Services
{
    public class BranchTransactionService : IBranchTransactionService
    {
        Database Connection;
        
        IAuditsRepository ActivityService;

        public BranchTransactionService(Database dbFactory, IAuditsRepository activityService)
        {
            Connection = dbFactory;
           
            ActivityService = activityService;
           
        }

        public long AddItem(UploadModel  model, string userName, string solId, ref string message)
        {
            try
            {
                model.IsDeleted = false;
                model.Month = DateTime.Now.Month.ToString();
                model.DateCreated = DateTime.Now;
                model.SolId = solId;
                model.RMStaffID = userName;
                Connection.UploadModels.Add(model);
                Connection.SaveChanges();
                return model.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }       
        }

        public void AddItems(List<UploadModel> models, string userName, string solId, ref string message)
        {


            try
            {
                Connection.UploadModels.AddRange(models);
                Connection.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }





        }

        public List<UploadModel> GetItemsByBatchNumber(Guid bacthNumber)
        {
            return Connection.UploadModels.Where(x => x.BatchNumber.Equals(bacthNumber) && !x.IsDeleted ).ToList<UploadModel>();
        }

        public List<UploadModel> GetItems(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                return Connection.UploadModels.ToList();
            else
                return Connection.UploadModels.Where(x => x.BatchNumber.Equals(new Guid(guid)) && !x.IsDeleted).ToList<UploadModel>();
        }

     

        public UploadModel GetRequest(long id1, string solId)
        {
            return Connection.UploadModels.Where(x => x.ID.Equals(id1) && x.SolId.Equals(solId) && !x.IsDeleted).FirstOrDefault<UploadModel>();
        }

        public long UpdateItem(UploadModel model, string userName, string solId, ref string message)
        {
            try
            {
                var x = Connection.UploadModels.Find(model.ID);
               if(x.ID>0)
                {
                    x.Amount = model.Amount;
                    x.BeneficiaryAccountNumber = model.BeneficiaryAccountNumber;
                    x.BUCode = model.BUCode;
                    x.DateCreated = model.DateCreated;
                    x.DeskCode = model.DeskCode;
                    x.FlowStatus = model.FlowStatus;
                    x.InitiatingBranchName = model.InitiatingBranchName;
                    x.InitiatingBranchSolID = model.InitiatingBranchSolID;
                    x.IsBatchApproved = model.IsBatchApproved;
                    x.IsBatchValidated = model.IsBatchValidated;
                    x.IsDeleted = model.IsDeleted;
                    x.Month = model.Month;
                    x.PayementDate = model.PayementDate;
                    x.PayerAccountNumber = model.PayerAccountNumber;
                    x.PayerName = model.PayerName;
                    x.PayerType = model.PayerType;
                    x.PaymentReferenceNumber = model.PaymentReferenceNumber;
                    x.ProductCategory = model.ProductCategory;
                    x.RMStaffID = model.RMStaffID;
                    x.SolId = model.SolId;
                    x.TeamCode = model.TeamCode;
                    Connection.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error($"AddItem::" + model.BatchNumber + "|||" + model.SolId + "|||" + model.BUCode, e);
            }
            return model.ID;
        }

        
    }
}
