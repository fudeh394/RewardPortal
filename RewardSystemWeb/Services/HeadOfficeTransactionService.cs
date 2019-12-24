using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RewardSystemWeb.Entities;

namespace RewardSystemWeb.Services
{
    public class HeadOfficeTransactionService :IHeadOfficeTransactionService
    {
        Database Connection;
        
        IAuditsRepository ActivityService;

        public HeadOfficeTransactionService(Database dbFactory, IAuditsRepository activityService)
        {
            Connection = dbFactory;
           
            ActivityService = activityService;
           
        }

       
     

       

       

        public  List<VerifiedRecord> GetItemsByBatchNumber(Guid bacthNumber)
        {
            return Connection.VerifiedRecords.Where(x => x.BatchNumber.Equals(bacthNumber) && !x.IsDeleted).ToList<VerifiedRecord>();
        }

        

      

        VerifiedRecord GetRequest(long id1)
        {
            return Connection.VerifiedRecords.Find(id1);
        }

        public long AddItem(VerifiedRecord model, string userName, string solId, ref string message)
        {
            try
            {
                model.IsDeleted = false;
                model.Month = DateTime.Now.Month.ToString();
                model.DateCreated = DateTime.Now;
                model.InitiatingBranchSolID = model.InitiatingBranchSolID;
                model.RMStaffID = userName;
                Connection.VerifiedRecords.Add(model);
                Connection.SaveChanges();
                return model.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AddItems(List<VerifiedRecord> models, string userName, string solId, ref string message)
        {
            try
            {
                Connection.VerifiedRecords.AddRange(models);
                Connection.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public long UpdateItem(VerifiedRecord model, string userName, string solId, ref string message)
        {
            try
            {
                var x = Connection.VerifiedRecords.Find(model.ID);
                if (x.ID > 0)
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
                    //x.SolId = model.SolId;
                    x.TeamCode = model.TeamCode;
                    Connection.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error($"UpdateItem::" + model.BatchNumber + "|||" + model.InitiatingBranchSolID + "|||" + model.BUCode, e);
            }
            return model.ID;
        }

        List<VerifiedRecord> IHeadOfficeTransactionService.GetItems(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                return Connection.VerifiedRecords.ToList();
            else
                return Connection.VerifiedRecords.Where(x => x.BatchNumber.Equals(new Guid(guid)) && !x.IsDeleted).ToList<VerifiedRecord>();
        }

        VerifiedRecord IHeadOfficeTransactionService.GetRequest(long id1)
        {
            return Connection.VerifiedRecords.Where(x => x.ID.Equals(id1) && !x.IsDeleted).FirstOrDefault<VerifiedRecord>();
        }

       
    }
}
