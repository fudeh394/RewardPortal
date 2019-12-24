
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
       //RewardBands = new List<RewardBand>
       //     {
       //          new RewardBand
       //          {
       //               Rank =1, Max=30, Min = 1, Reward = 100
       //          },
       //          new RewardBand
       //          {
       //               Rank =2, Max=60, Min = 31, Reward = 125
       //          },
       //          new RewardBand
       //          {
       //               Rank =3, Max=90, Min = 61, Reward = 150
       //          },
       //          new RewardBand
       //          {
       //               Rank =4, Max=120, Min = 91, Reward = 175
       //          },
       //          new RewardBand
       //          {
       //               Rank =5, Max=100000000, Min = 121, Reward = 200
       //          }
       //     };

            rewardService = new RewardService(RewardBands);

        }

        public ActionResult Index()
        {

            var models = new List<BillingResult>();
            models = MySessions.CurrentUpload;
            return View(models);

        }



        public ActionResult TditItem(long id = 0)
        {
            var model = new CreatePost();
            //if (id > 0)
            //{
            //    model = _service.GetRequest(id, CurrentUser.SolId);
            //}

            return View(model);

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

        private string GetAccountName(string bnkCode)
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
                    return Connection.AgentAccounts.Where(x => x.BankCode.Equals(input, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault<AgentAccount>().AccountName;

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
                        AccountName = string.IsNullOrEmpty(GetAccountName(x.BankCode)) ? x.AccountName : GetAccountName(x.BankCode),
                        AccountNumber = string.IsNullOrEmpty(GetAccount(x.BankCode)) ? FormatAccountNumber(x.AccountNumber) : GetAccount(x.BankCode),
                         AgrntMgrInstCode  = FormatInstitutionCode( x.AgtMgrInstCode),
                          AgrntMgrInstName  = x.AgtMgrInstName,
                        BankCode = FormatBankCode(x.BankCode) , 
                        Narration  = x.Narration,
                       
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


        private byte[] ExportRequest(List<BillingResult> request)
        {
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("S/N");
            headerRow.CreateCell(1).SetCellValue(" AGT MGR INST NAME");
            headerRow.CreateCell(2).SetCellValue(" AGT MGR INST CODE");
            headerRow.CreateCell(3).SetCellValue(" AMOUNT");
            headerRow.CreateCell(4).SetCellValue(" ACCOUNT NUMBER");
            headerRow.CreateCell(5).SetCellValue(" ACCOUNT NAME");
            headerRow.CreateCell(6).SetCellValue(" BANK CODE");
            headerRow.CreateCell(7).SetCellValue("  NARRATION");
            
            int rowNumber = 1;

            foreach (var n in request)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);
                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(n.SN);
                row.CreateCell(1).SetCellValue(n.AgrntMgrInstName);
                row.CreateCell(2).SetCellValue(n.AgrntMgrInstCode);
                row.CreateCell(3).SetCellValue((string.Format("{0:#,0.00}", n.Amount)));
                row.CreateCell(4).SetCellValue(n.AccountNumber);
                row.CreateCell(5).SetCellValue(n.AccountName);
                row.CreateCell(6).SetCellValue(n.BankCode);
                row.CreateCell(7).SetCellValue(n.Narration);
               
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
