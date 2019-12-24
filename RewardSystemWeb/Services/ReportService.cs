using RewardSystemWeb.Interfaces;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RewardSystemWeb.Entities;
using RewardSystemWeb.Models;
using RewardSystemWeb;

namespace TBGReportUpload.Services
{
    public class ReportService : IReportService
    {
        Database Connection;
        IBranchTransactionService Uploadservice;
        IRequest RequestService;
        

        public ReportService(Database connection, IBranchTransactionService uploadservice, IRequest request)
        {
            Connection = connection;
            Uploadservice = uploadservice;
            RequestService = request;
        }


        public List<UploadModel> ApprovedItems(ReportRequestModel model)
        {
            var result = new List<UploadModel>();
            try
            {
                result =Connection.UploadModels.Where(x => (x.IsBatchApproved && !x.IsDeleted)
                && (model.SolId == null || x.SolId.Equals(model.SolId, StringComparison.InvariantCultureIgnoreCase))               
                && (x.DateCreated >= model.FromDate  &&  x.DateCreated   <= model.ToDate)
                ).Take(model.Preview ? 15 : 1000000).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error("ApprovedItems|| Error", ex);
            }

            return result;
        }



        public List<UploadModel> AllItems(ReportRequestModel model)
        {
            var result = new List<UploadModel>();
            try
            {
                result = Connection.UploadModels.Where(x => (!x.IsDeleted)
                 && (model.SolId == null || x.InitiatingBranchSolID.Equals(model.SolId, StringComparison.InvariantCultureIgnoreCase))
                 && (x.DateCreated >= model.FromDate && x.DateCreated <= model.ToDate)
                 && (model.DeskCode == null || x.DeskCode.Equals(model.DeskCode, StringComparison.InvariantCultureIgnoreCase))
                 //&& (model.UserId == null || x..Equals(model.UserId, StringComparison.InvariantCultureIgnoreCase))
                ).Take(model.Preview ? 1000 : 1000000).ToList();


            }
            catch (Exception ex)
            {
                Logger.Error("AllItems|| Error", ex);
            }

            return result;
        }

        //public List<ReportModel> SelectApprovedDudCheques(ReportRequestModel model)
        //{
        //    var result = new List<ReportModel>();
        //    try
        //    {              
        //        result = PreRequestService.GetAllPreRequests().Where(x => (x.IsApproved && !x.IsDeleted)                 
        //        && (model.SolId == null || x.SolId.Equals(model.SolId, StringComparison.CurrentCultureIgnoreCase))
        //        && (model.AccountNumber == null || x.AccountNumber == model.AccountNumber)
        //        && (model.ChequeNumber == null || x.ChequeNumber == model.ChequeNumber)
        //        //&& ((x.CreatedDate >= model.FromDate  && x.CreatedDate  <= model.ToDate))
        //        ).Select(y => new ReportModel
        //        {
        //            AccountNumber = y.AccountNumber,
        //            Amount = y.Amount,
        //            ChequeNumber = y.ChequeNumber,
        //            ChequePresentmentDate = y.ChequePresentmentDate,
        //            CreatedDate = y.CreatedDate,
        //            CustomerName = y.CustomerName,
        //            SolId = y.SolId,
        //            PreRequestId = y.PreRequestId,
        //            RequestId = y.PreRequestId,
        //            Status = "Approved",
        //            ResponseCode = "--",
        //            ResponseDesc = "--",
        //             ApprovedDate = y.ApprovedDate
        //        }).Take(model.Preview? 15:1000000).ToList<ReportModel>();

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("SelectApprovedDudCheques|| Error", ex);
        //    }

        //    return result;
        //}

        //public List<ReportModel> SelectDeclinedDudCheques(ReportRequestModel model)
        //{
        //    var result = new List<ReportModel>();
        //    try
        //    {


        //        result = PreRequestService.GetAllPreRequests().Where(x => (x.IsDeclined && !x.IsDeleted)
        //      && (model.SolId == null || x.SolId.Equals(model.SolId, StringComparison.CurrentCultureIgnoreCase))
        //        && (model.AccountNumber == null || x.AccountNumber == model.AccountNumber)
        //        && (model.ChequeNumber == null || x.ChequeNumber == model.ChequeNumber)
        //        //&& ((x.CreatedDate >= model.FromDate && x.CreatedDate <= model.ToDate))
        //       ).Select(y => new ReportModel
        //       {
        //           AccountNumber = y.AccountNumber,
        //           Amount = y.Amount,
        //           ChequeNumber = y.ChequeNumber,
        //           ChequePresentmentDate = y.ChequePresentmentDate,
        //           CreatedDate = y.CreatedDate,
        //           CustomerName = y.CustomerName,
        //           SolId = y.SolId,
        //           PreRequestId = y.PreRequestId,
        //           RequestId = y.PreRequestId,
        //           Status = "Declined",
        //           ResponseCode = "--",
        //           ResponseDesc = "--",
        //           ApprovedDate = y.ApprovedDate
        //       }).Take(model.Preview ? 15 : 1000000).ToList<ReportModel>();



        //    }
        //    catch (Exception ex)
        //    {
        //       // Log.Error("SelectDeclinedDudCheques|| Error", ex);
        //    }

        //    return result;
        //}

        //public List<ReportModel> SelectPostedDudCheques(ReportRequestModel model)
        //{
        //    var result = new List<ReportModel>();
        //    try
        //    {



        //     result = RequestService.GetAllRequests().Where(x => (x.IsPosted && !x.IsDeleted)
        //    && (model.SolId == null || x.SolId.Equals(model.SolId, StringComparison.CurrentCultureIgnoreCase))
        //        && (model.AccountNumber == null || x.AccountNumber == model.AccountNumber)
        //        && (model.ChequeNumber == null || x.ChequeNumber == model.ChequeNumber)
        //        //&& ((x.CreatedDate >= model.FromDate && x.CreatedDate <= model.ToDate))
        //     ).Select(y => new ReportModel
        //     {
        //         AccountNumber = y.AccountNumber,
        //         Amount = y.Amount,
        //         ChequeNumber = y.ChequeNumber,
        //         ChequePresentmentDate = y.ChequePresentmentDate,
        //         CreatedDate = y.CreatedDate,
        //         CustomerName = y.CustomerName,
        //         SolId = y.SolId,
        //         PreRequestId = y.PreRequestId,
        //         RequestId = y.PreRequestId,
        //         Status = "Posted",
        //         ResponseCode = y.ResponseCode,
        //         ResponseDesc = y.ResponseDesc,
        //         PostedDate = y.PostedDate
        //     }).Take(model.Preview ? 15 : 1000000).ToList<ReportModel>();
        //    }
        //    catch (Exception ex)
        //    {

        //        Log.Error("SelectPostedDudCheques|| Error", ex);
        //    }

        //    return result;
        //}



        public byte[] ExportRequest(List<ReportModel> request)
        {
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("RM Code");
            headerRow.CreateCell(1).SetCellValue("Desk Code");
            headerRow.CreateCell(2).SetCellValue("Team Code");
            headerRow.CreateCell(3).SetCellValue("Payer Name");
            headerRow.CreateCell(4).SetCellValue("Payer AccountNumber");
            headerRow.CreateCell(5).SetCellValue("Beneficiary AccountNumber");
            headerRow.CreateCell(6).SetCellValue("Payment Date");
            headerRow.CreateCell(7).SetCellValue("Payment Type");
            headerRow.CreateCell(8).SetCellValue("Payment ReferenceNo");
            headerRow.CreateCell(9).SetCellValue("Amount");
            headerRow.CreateCell(10).SetCellValue("Initiating BranchName");
            headerRow.CreateCell(11).SetCellValue("Initiating Branch Sol ID");
            headerRow.CreateCell(12).SetCellValue("Product Category");
            headerRow.CreateCell(13).SetCellValue("Collection Platform");

            int rowNumber = 1;

            foreach (var n in request)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);
                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(n.RMCode);
                row.CreateCell(1).SetCellValue(n.DeskCode);
                row.CreateCell(2).SetCellValue(n.TeamCode);
                row.CreateCell(3).SetCellValue((n.PayerName));
                row.CreateCell(4).SetCellValue(n.PayerAccountNumber);
                row.CreateCell(5).SetCellValue(n.BeneficiaryAccountNumber);
                row.CreateCell(6).SetCellValue(n.PaymentDate);
                row.CreateCell(7).SetCellValue(n.PaymentType);
                row.CreateCell(8).SetCellValue(n.PaymentReferenceNo);
                row.CreateCell(9).SetCellValue(Convert.ToString(n.Amount));
                row.CreateCell(10).SetCellValue(n.InitiatingBranchName);
                row.CreateCell(11).SetCellValue(n.InitiatingBranchSolID);
                row.CreateCell(12).SetCellValue(n.ProductCategory);
                row.CreateCell(13).SetCellValue(Convert.ToString(n.CollectionPlatform));
            }

            //Write the Workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return output.ToArray();
        }


        //public byte[] ExportPreRequest(List<ReportModel> request)
        //{
        //    var workbook = new HSSFWorkbook();

        //    //Create new Excel Sheet
        //    var sheet = workbook.CreateSheet();
        //    var headerRow = sheet.CreateRow(0);
        //    headerRow.CreateCell(0).SetCellValue("PreRequestId");
        //    headerRow.CreateCell(1).SetCellValue("CustomerName");
        //    headerRow.CreateCell(2).SetCellValue("AccountNumber");
        //    headerRow.CreateCell(3).SetCellValue("Amount");
        //    headerRow.CreateCell(4).SetCellValue("ChequeNumber");
        //    headerRow.CreateCell(5).SetCellValue("ChequePresentmentDate");
        //    headerRow.CreateCell(6).SetCellValue("Status");
        //    headerRow.CreateCell(7).SetCellValue("CreatedDate");
        //    headerRow.CreateCell(8).SetCellValue("ApprovedDate");
        //    headerRow.CreateCell(9).SetCellValue("PostedDate");
        //    headerRow.CreateCell(10).SetCellValue("SolId");
        //    headerRow.CreateCell(11).SetCellValue("ResponseCode");
        //    headerRow.CreateCell(12).SetCellValue("ResponseDesc");
            

        //    int rowNumber = 1;

        //    foreach (var n in request)
        //    {
        //        //Create a new Row
        //        var row = sheet.CreateRow(rowNumber++);
        //        //Set the Values for Cells
        //        row.CreateCell(0).SetCellValue(Convert.ToString(n.PreRequestId));
        //        row.CreateCell(1).SetCellValue(n.CustomerName);
        //        row.CreateCell(2).SetCellValue(n.AccountNumber);
        //        row.CreateCell(3).SetCellValue(Convert.ToString(n.Amount));
        //        row.CreateCell(4).SetCellValue(n.ChequeNumber);
        //        row.CreateCell(5).SetCellValue(n.ChequePresentmentDate);
        //        row.CreateCell(6).SetCellValue(n.Status);
        //        row.CreateCell(7).SetCellValue(n.CreatedDate);
        //        row.CreateCell(8).SetCellValue(n.ApprovedDate);
        //        row.CreateCell(9).SetCellValue(n.PostedDate);
        //        row.CreateCell(10).SetCellValue(n.SolId);
        //        row.CreateCell(11).SetCellValue(n.ResponseCode);
        //        row.CreateCell(12).SetCellValue(n.ResponseDesc);
               
        //    }

        //    //Write the Workbook to a memory stream
        //    MemoryStream output = new MemoryStream();
        //    workbook.Write(output);

        //    return output.ToArray();
        //}

        
        //List<UploadModel> IReportService.SelectApprovedDudCheques(ReportRequestModel model)
        //{
        //    throw new NotImplementedException();
        //}

        //List<UploadModel> IReportService.SelectDeclinedDudCheques(ReportRequestModel model)
        //{
        //    throw new NotImplementedException();
        //}
    }


   
}
