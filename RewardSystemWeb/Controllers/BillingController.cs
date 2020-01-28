
using Npoi.Mapper;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using RewardSystem;
using RewardSystemWeb.Entities;
using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RewardSystemWeb.Controllers
{



    public class BillingController : BaseController
    {
        private readonly IAuditsRepository _auditsRepository;
        //private readonly IRequest _service;
        Database Connection;

        private readonly List<RewardBand> RewardBands;
        private readonly RewardService rewardService;


        public BillingController(IAuditsRepository auditsRepository, Database _Connection)
        {
            Connection = _Connection;
            _auditsRepository = auditsRepository;

            RewardBands = Connection.RewardBands.Select(x => new RewardBand
            {
                Max = x.Max,
                Min = x.Min,
                Rank = x.Rank,
                Reward = x.Reward
            }).ToList<RewardBand>();
       
            rewardService = new RewardService(RewardBands);

        }

        public ActionResult Index()
        {

            var models = new List<BillingResult>();
            models = MySessions.CurrentUpload;
            return View(models);

        }



        public ActionResult TditItem(int Count = 0)
        {
            var models = new List<AgentResultDetail>();
            if(Count > 0)
            {
                models = rewardService.GetComputationDetail(Count).Select(x => new AgentResultDetail {
                 Amount = x.Amount, Band = x.Band, Count = x.Count, DateRange = x.DateRange, Price = x.Price, TotalCount = x.TotalCount}).ToList<AgentResultDetail>();
                MySessions.CurrentAgentResultDetail = models;
            }
            

            return View(models);

        }


        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return CurrentUser.SolId + "_" + DateTime.Now.ToString("yyyyMMddhhmmss")
                      + "_"
                      + Guid.NewGuid().ToString()
                      + Path.GetExtension(fileName);
        }





        public ActionResult NewItem(long id = 0)
        {
            CreatePost model = new CreatePost();
            if (id > 0)
            {
                var preRequest = new CreatePost();
                model = new CreatePost
                {
                    BatchNumber = preRequest.BatchNumber,
                    RequestStatus = preRequest.RequestStatus,
                    Id = preRequest.Id,
                    Items = MySessions.CurrentUpload,// _branchService.GetItemsByBatchNumber(preRequest.BatchNumber),
                    IsDeleted = preRequest.IsDeleted,
                    SolId = preRequest.SolId
                   ,
                    CreatedDate = preRequest.CreatedDate,
                    Path = preRequest.Path
                };
            }
            return View(model);
        }



        private ActionResult DeleteItem(CreatePost model)
        {

            long id;
            var message = "";

            return View(model);
        }

        private string FormatAccountNumber(string acct)
        {
            if (acct != null && acct.Length == 10)
                return acct;

            if (acct != null && acct.Length < 10)
            {
                acct = int.Parse(acct).ToString("D10");
                return acct;
            }
            else
            {
                return string.Empty;
            }
        }

        private string FormatBankCode(string bnkCode)
        {
            if (bnkCode != null && bnkCode.Length == 3)
                return bnkCode;

            if (bnkCode != null && bnkCode.Length < 3)
            {
                bnkCode = int.Parse(bnkCode).ToString("D3");
                return bnkCode;
            }
            else
            {
                return string.Empty;
            }
        }

        private string FormatInstitutionCode(string bnkCode)
        {
            if (bnkCode != null && bnkCode.Length == 5)
                return bnkCode;

            if (bnkCode != null && bnkCode.Length < 5)
            {
                bnkCode = int.Parse(bnkCode).ToString("D5");
                return bnkCode;
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetAccountName(string bnkCode, string acct)
        {
            var input = string.Empty;
            if (bnkCode != null && bnkCode.Length == 3)
                input= bnkCode;

            if (bnkCode != null && bnkCode.Length < 3)
            {
                bnkCode = int.Parse(bnkCode).ToString("D3");
                input = bnkCode;
            }
            if(string.IsNullOrEmpty(input) || string.IsNullOrEmpty(bnkCode))
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    return Connection.AgentAccounts.Where(x => (x.BankCode.Equals(input, StringComparison.InvariantCultureIgnoreCase) && x.AccountNumber.Equals(acct, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault<AgentAccount>().AccountName;

                }
                catch (Exception)
                {

                    return string.Empty;
                }
            }
        }

        private string GetAccount(string bnkCode)
        {
            var input = string.Empty;
            if (bnkCode != null && bnkCode.Length == 3)
                input = bnkCode;

            if (bnkCode != null && bnkCode.Length < 3)
            {
                bnkCode = int.Parse(bnkCode).ToString("D3");
                input = bnkCode;
            }
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(bnkCode))
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    return Connection.AgentAccounts.Where(x => x.BankCode.Equals(input, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault<AgentAccount>().AccountNumber;

                }
                catch (Exception)
                {
                    return string.Empty;
                    
                }
           }
        }


        private List<BillingResult> GetExcelItems(string path)
        {
            var result = new List<BillingResult>();
            try
            {
                IWorkbook workbook;
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    
                    workbook = WorkbookFactory.Create(file);
                }
                
                var importer = new Mapper(workbook);
                var items = importer.Take<BillingReport>(0).Select(y => y.Value).ToList<BillingReport>();


                var batchID = Guid.NewGuid().ToString();

                if (items != null)
                {
                    result = items.Select(x => new BillingResult
                    {
                        Amount = rewardService.GetReward(x.Count),
                        AccountName = string.IsNullOrEmpty(GetAccountName(x.BankCode, FormatAccountNumber(x.AccountNumber))) ? x.AccountName : GetAccountName(x.BankCode, FormatAccountNumber(x.AccountNumber)),
                        AccountNumber =  FormatAccountNumber(x.AccountNumber), 
                         AgrntMgrInstCode  = FormatInstitutionCode( x.AgtMgrInstCode),
                          AgrntMgrInstName  = x.AgtMgrInstName,
                        BankCode = FormatBankCode(x.BankCode) , 
                        Narration  = MySessions.CurrentFileName,
                       Count = x.Count, AgentCode = x.AgentCode,
                        SN = x.SN


                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }


            return result;
        }


        
        private ActionResult EditItem(CreatePost model)
        {

            return View(model);
        }


        public ActionResult AuthItem(long id)
        {
            var message = "";
          

            return RedirectToAction("Index");
        }

        public ActionResult DownloadBillingResult()
        {
            var models = new List<BillingResult>();
            models = MySessions.CurrentUpload;

            if(models != null && models.Count>0)
            {
                var output = ExportRequest(models);

                string filename = string.Format("BillingResult-{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"));

                TempData["message"] = "Successfully exported";
                return File(output, "application/vnd.ms-excel", filename);

            }


            return View(models);
        }





        public ActionResult DownloadBillingResult1()
        {
            var models = new List<AgentResultDetail>();
            models = MySessions.CurrentAgentResultDetail;

            if (models != null && models.Count > 0)
            {
                var output = ExportRequest1(models);

                string filename = string.Format("BillingDetailsResult-{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"));

                TempData["message"] = "Successfully exported";
                return File(output, "application/vnd.ms-excel", filename);

            }


            return View(models);
        }


        private byte[] ExportRequest(List<BillingResult> request)
        {
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("S/N");
            headerRow.CreateCell(1).SetCellValue(" AGT MGR INST NAME");
            headerRow.CreateCell(2).SetCellValue(" AGT MGR INST CODE");
            headerRow.CreateCell(3).SetCellValue(" AGENT CODE");
            headerRow.CreateCell(4).SetCellValue(" COUNT");
            headerRow.CreateCell(5).SetCellValue(" AMOUNT");
            headerRow.CreateCell(6).SetCellValue(" ACCOUNT NUMBER");
            headerRow.CreateCell(7).SetCellValue(" ACCOUNT NAME");
            headerRow.CreateCell(8).SetCellValue(" BANK CODE");
            headerRow.CreateCell(9).SetCellValue("  NARRATION");
            
            int rowNumber = 1;

            foreach (var n in request)
            {
                //Create a new Row
                var id = rowNumber++;
                var row = sheet.CreateRow(id);
                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(id);
                row.CreateCell(1).SetCellValue(n.AgrntMgrInstName);
                row.CreateCell(2).SetCellValue(n.AgrntMgrInstCode);
                row.CreateCell(3).SetCellValue(n.AgentCode);
                row.CreateCell(4).SetCellValue(n.Count);
                row.CreateCell(5).SetCellValue((string.Format("{0:#,0.00}", n.Amount)));
                
                row.CreateCell(6).SetCellValue(n.AccountNumber);
                row.CreateCell(7).SetCellValue(n.AccountName);
                row.CreateCell(8).SetCellValue(n.BankCode);
                row.CreateCell(9).SetCellValue(n.Narration);
               
            }

            //Write the Workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return output.ToArray();
        }



        private byte[] ExportRequest1(List<AgentResultDetail> request)
        {
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Date Range");
            headerRow.CreateCell(1).SetCellValue("Total Enrolment");
            headerRow.CreateCell(2).SetCellValue("Band");
            headerRow.CreateCell(3).SetCellValue("Count Within Band");
            headerRow.CreateCell(4).SetCellValue("Price");
            headerRow.CreateCell(5).SetCellValue("Amount");
         
            

            int rowNumber = 1;

            foreach (var n in request)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);
                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(n.DateRange);
                row.CreateCell(1).SetCellValue(n.TotalCount);
                row.CreateCell(2).SetCellValue(n.Band);
                row.CreateCell(3).SetCellValue(n.Count);
                row.CreateCell(4).SetCellValue((string.Format("{0:#,0.00}", n.Price)));
                row.CreateCell(5).SetCellValue((string.Format("{0:#,0.00}", n.Amount)));
                

            }

            //Write the Workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return output.ToArray();
        }


        [HttpPost]
        public ActionResult NewItem(CreatePost model, string button)
        {
            long id;
            var message = "";
            TempData["message"] = null;

            if (model.Id > 0)
            {

                if (button.Equals("update"))
                    return this.EditItem(model);
                if (button.Equals("delete"))
                    return this.DeleteItem(model);
            }

            model.IsDeleted = false;
            model.CreatedDate = DateTime.Now;
            model.SolId = CurrentUser.SolId;
            model.BatchNumber = Guid.NewGuid();
            if (model.FilePath != null && model.FilePath.ContentLength > 0)
            {
                try
                {

                    if (!(model.FilePath.FileName.EndsWith("xls") || model.FilePath.FileName.EndsWith("xlsx")))
                    {
                        message = "Wrong format exception";
                        Logger.Error("$NewItem ||| Error uploading front image -invalid file format");
                        TempData["error"] = message;
                        return View(model);
                    }
                    model.OriginalFileName = Path.GetFileName(model.FilePath.FileName);
                    MySessions.CurrentFileName = Path.GetFileNameWithoutExtension(model.OriginalFileName);
                    var uniqueFileName = GetUniqueFileName(model.OriginalFileName);
                    var uploads = Path.Combine((string)ConfigurationManager.AppSettings["upload_path"], "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    model.FilePath.SaveAs(filePath);
                    model.Path = filePath;
                    model.Items = GetExcelItems(filePath);

                }
                catch (Exception e)
                {
                    message = "Error uploading front image";
                    Logger.Error("$NewItem ||| Error uploading front image", e);
                }

            }

            if (model.Items != null && model.Items.Count > 0)
            {
                MySessions.CurrentUpload = model.Items;
                TempData["message"] = "Rquest was successful!";
                return RedirectToAction("Index", "Billing");
            }
            else
            {
                message = "No item in the uploaded excel file";
            }

            TempData["error"] = message;
            return View(model);
        }
    }
}
