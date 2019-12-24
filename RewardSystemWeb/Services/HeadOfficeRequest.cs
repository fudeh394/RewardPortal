using RewardSystemWeb.Entities;
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
    public class HeadOfficeRequest : IHeadOfficeRequests
    {
        Database  Connection;
        IHeadOfficeTransactionService HeadOfficeTransactionService;
        IAuditsRepository AuditsRepository;
        public HeadOfficeRequest(Database dbFactory, IHeadOfficeTransactionService service,IAuditsRepository auditsRepository )
        {
            Connection = dbFactory;
            HeadOfficeTransactionService = service;
            AuditsRepository = auditsRepository;
        }

        public bool AuthItem(long  Id, string userName, string solId, ref string message)
        {
            var res = false;
            try
            {
                var req = Connection.HoRequests.Find(Id);
                req.RequestStatus = Constants.REQUESTSTATUS_APPROVED;
                Connection.SaveChanges();

                Connection.Database.ExecuteSqlCommand(@"UPDATE [dbo].[VerifiedRecords] SET [FlowStatus] = @FlowStatus, 
[IsBatchApproved] = @IsBatchApproved WHERE BatchNumber = @BatchNumber", new SqlParameter("FlowStatus", Constants.REQUESTSTATUS_APPROVED)
        , new SqlParameter("IsBatchApproved", true)
        , new SqlParameter("BatchNumber", req.BatchNumber));
                res = true;
                message = "Request Approved Successful";


                AuditsRepository.AddActivity(new Audit
                {
                    AuditDescription = string.Format("Approved Head Office Items with BatchNumber {0}", req.BatchNumber),
                    EventId = req.Id,
                    CreatedBy = userName,
                    CreatedDate = DateTime.Now,
                    Comment = "Request is Approved"
                });
            }
            catch (Exception ex)
            {
                message = "Error saving request";
                Logger.Error(ex);
            }

            return res;
        }

        public long AddItem(CreatePost model, string userName, string solId, ref string message)
        {
            var req = new RewardSystemWeb.Models.HoRequest
            {
                BatchNumber = model.BatchNumber,
                CreatedDate = model.CreatedDate,
                FilePath = model.Path,
                IsDeleted = false,
                RequestStatus = Constants.REQUESTSTATUS_PENDING,
                SolId = solId, CreatedBy = userName,
                OriginalFileName = model.OriginalFileName 
            };

            Connection.HoRequests.Add(req);
            Connection.SaveChanges();
            
            if(req.Id > 0)
            {


                var items = model.ItemsHo.Select(x => new VerifiedRecord 
                {
                    IsDeleted = false,
                    Month = DateTime.Now.Month.ToString(),
                    DateCreated = DateTime.Now,
                   //SolId = solId,
                    RMStaffID = x.RMStaffID           ,
                    Amount = x.Amount,
                    BatchNumber = model.BatchNumber,
                    BeneficiaryAccountNumber = x.BeneficiaryAccountNumber,
                    BUCode = x.BUCode,
                    DeskCode = x.DeskCode,
                    FlowStatus = x.FlowStatus,
                    InitiatingBranchName = x.InitiatingBranchName,
                    InitiatingBranchSolID = x.InitiatingBranchSolID,
                    IsBatchApproved = x.IsBatchApproved,
                    IsBatchValidated = x.IsBatchValidated,
                    PayementDate = x.PayementDate,
                    PayerAccountNumber = x.PayerAccountNumber,
                    PayerName = x.PayerName,
                    PayerType = x.PayerType,
                    PaymentReferenceNumber = x.PaymentReferenceNumber,
                    ProductCategory = x.ProductCategory,
                    TeamCode = x.TeamCode,
                    BeneficiaryName = x.BeneficiaryName,
                    CollectionPlatform = x.CollectionPlatform
                }).ToList ();

                //add  the items
                HeadOfficeTransactionService.AddItems(items, userName, solId, ref message);
                //log audit request
                AuditsRepository.AddActivity(new Audit
                {
                    AuditDescription = string.Format("Uploaded File with BatchNumber {0} containing {1} Transactions", model.BatchNumber,model.ItemsHo.Count),
                    EventId = req.Id,
                    CreatedBy = userName,
                    CreatedDate = DateTime.Now,
                    Comment = "Request is yet to be approved"
                });
            }

            return req.Id;
        }

        public List<Models.CreatePost> ConfirmedItems(string userName, string solId)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmItem(long id, string userName, string solId, ref string message)
        {
            throw new NotImplementedException();
        }

        public Models.CreatePost GetConfirmationItem(long id, string userName, string solId)
        {
            throw new NotImplementedException();
        }

        public List<Models.CreatePost> GetPendingItems(string userName, string solId)
        {
            var res = new List<CreatePost>();  
            
            res = Connection.HoRequests.Where(x => !x.IsDeleted && x.RequestStatus == Constants.REQUESTSTATUS_PENDING && x.SolId.Equals(solId, StringComparison.InvariantCultureIgnoreCase)).Select(y=>new CreatePost {

                 BatchNumber = y.BatchNumber, OriginalFileName = y.OriginalFileName ,CreatedBy = y.CreatedBy , CreatedDate = y.CreatedDate  , IsDeleted = y.IsDeleted, Id = y.Id , Path = y.FilePath, RequestStatus = y.RequestStatus, SolId = y.SolId 

            }).ToList();


            return res;
        }

        public Models.CreatePost  GetRequest(long id1, string solId)
        {
            var res = Connection.HoRequests.Where(y => y.Id == id1).FirstOrDefault();
            return new CreatePost
            {
                BatchNumber = res.BatchNumber,
                CreatedBy = res.CreatedBy,
                CreatedDate = res.CreatedDate,
                IsDeleted = res.IsDeleted,
                Id = res.Id,
                Path = res.FilePath,
                RequestStatus = res.RequestStatus,
                SolId = res.SolId, 
                OriginalFileName = res.OriginalFileName
                , ItemsHo = HeadOfficeTransactionService.GetItems(res.BatchNumber.ToString())
            };

       

        }



        private  Models.CreatePost GetRequest(long id1)
        {
            var res = Connection.HoRequests.Where(y => y.Id == id1).FirstOrDefault();
            return new CreatePost
            {
                BatchNumber = res.BatchNumber,
                CreatedBy = res.CreatedBy,
                CreatedDate = res.CreatedDate,
                IsDeleted = res.IsDeleted,
                Id = res.Id,
                Path = res.FilePath,
                RequestStatus = res.RequestStatus,
                SolId = res.SolId, OriginalFileName= res.OriginalFileName 
                ,
                ItemsHo = HeadOfficeTransactionService.GetItems(res.BatchNumber.ToString())
            };



        }


        public long UpdateItem(CreatePost model, string userName, string solId, ref string message)
        {
            throw new NotImplementedException();
        }
    }
}
