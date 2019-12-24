
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



    public class AgentManagerController : BaseController
    {
        

        Database Connection;
        public AgentManagerController( Database _Connection)
        {
            Connection = _Connection;
        }

        public ActionResult Index()
        {
            var models = new List<AgentManager>();
            models = Connection.AgentManagers.Where(x => !x.IsDeleted).ToList<AgentManager>();
            return View(models);

        }

        public ActionResult Index1()
        {
            var models = new List<AgentAccount>();
            models = Connection.AgentAccounts.Where(x=>!x.IsDeleted).ToList();
            return View(models);

        }


        public ActionResult Index2()
        {
            var models = new List<RewardBand>();
            var res = Connection.RewardBands.ToList();
            models = res.Select(x => new RewardBand
            {
                Max = x.Max,
                Min = x.Min,
                Rank = x.Rank,
                Reward = x.Reward
            }).ToList<RewardBand>();
            return View(models);

        }






        public ActionResult NewItem(int id = 0)
        {
            AgentManager model = new AgentManager();
            if (id > 0)
            {
                model = Connection.AgentManagers.Find(id);
            }

            return View(model);
        }

        public ActionResult NewItem1(int id = 0)
        {
            AgentAccount model = new AgentAccount();

            if (id > 0)
            {
                model = Connection.AgentAccounts.Find(id);
            }
            return View(model);
        }

        public ActionResult NewItem2(int id = 0)
        {
            RewardBand model = new RewardBand();

            if (id > 0)
            {
                var res = Connection.RewardBands.Find(id);
                model.Max = res.Max;
                model.Min = res.Min;
                model.Rank = res.Rank;
                model.Reward = res.Reward;


            }
            return View(model);
        }



        private ActionResult DeleteItem(AgentManager model)
        {
            try
            {
                long id;
                var message = "";
                TempData["message"] = null;

                //get detail and set changes
                var realModel = Connection.AgentManagers.Find(model.Id);

                realModel.IsDeleted = true;
                Connection.SaveChanges();
                TempData["message"] = "Rquest Deleted successful!";
                return RedirectToAction("Index", "AgentManager");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = "Error deleting user. Contact Admin";
                return View(model);
            }


        }

        private ActionResult DeleteItem1(AgentAccount model)
        {
            try
            {
                long id;
                var message = "";
                TempData["message"] = null;

                //get detail and set changes
                var realModel = Connection.AgentAccounts.Find(model.Id);

                realModel.IsDeleted = true;
                Connection.SaveChanges();
                TempData["message"] = "Rquest Deleted successful!";
                return RedirectToAction("Index1", "AgentManager");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = "Error deleting user. Contact Admin";
                return View(model);
            }


        }



        private ActionResult DeleteItem2(RewardBand model)
        {
            try
            {
                long id;
                var message = "";
                TempData["message"] = null;

                //get detail and set changes
                var realModel = Connection.RewardBands.Find(model.Rank);

            
                Connection.SaveChanges();
                TempData["message"] = "Rquest Deleted successful!";
                return RedirectToAction("Index2", "AgentManager");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = "Error deleting user. Contact Admin";
                return View(model);
            }


        }






        private ActionResult EditItem(AgentManager model)
        {
            try
            {
                var item = Connection.AgentManagers.Find(model.Id);
                if (item != null && item.Id > 0)
                {
                    item.AgentManagerCode = model.AgentManagerCode;
                    item.AgentManagerName = model.AgentManagerName;
                    item.Id = model.Id;
                    item.IsDeleted = false;
                    Connection.SaveChanges();
                    TempData["message"] = "Request updated successful!";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index", "AgentManager");
        }

        private ActionResult EditItem1(AgentAccount model)
        {
            try
            {
                var item = Connection.AgentAccounts.Find(model.Id);
                if (item != null && item.Id > 0)
                {
                    item.AccountName = model.AccountName;
                    item.AccountNumber = model.AccountNumber;
                    item.BankCode = model.BankCode;
                    item.Id = model.Id;
                    item.IsDeleted = false;
                    Connection.SaveChanges();
                    TempData["message"] = "Request updated successful!";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index1", "AgentManager");
        }



        private ActionResult EditItem2(RewardBand model)
        {
            try
            {
                var item = Connection.RewardBands.Find(model.Rank);
                if (item != null && item.Rank > 0)
                {
                    item.Reward = model.Reward;
                    item.Min = model.Min;
                    item.Max = model.Max;
                   
                    Connection.SaveChanges();
                    TempData["message"] = "Request updated successful!";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index2", "AgentManager");
        }


        public ActionResult AuthItem(long id)
        {
           
            return null;
        }





        [HttpPost]
        public ActionResult NewItem(AgentManager model, string button)
        {
            try
            {
                long id;
                var message = "";
                TempData["message"] = null;
                if (button != null && button.Equals("update"))
                    return this.EditItem(model);
                if (button != null && button.Equals("delete"))
                    return this.DeleteItem(model);

                Connection.AgentManagers.Add(model);
                Connection.SaveChanges();

                TempData["message"] = "Rquest was successful!";
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("Index", "AgentManager");
        }


        [HttpPost]
        public ActionResult NewItem1(AgentAccount model, string button)
        {
            try
            {
                long id;
                var message = "";
                TempData["message"] = null;
                if (button != null && button.Equals("update"))
                    return this.EditItem1(model);
                if (button != null && button.Equals("delete"))
                    return this.DeleteItem1(model);

                Connection.AgentAccounts.Add(model);
                Connection.SaveChanges();

                TempData["message"] = "Rquest was successful!";

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("Index1", "AgentManager");
        }





        [HttpPost]
        public ActionResult NewItem2(RewardBand model, string button)
        {
            try
            {
                long id;
                var message = "";
                TempData["message"] = null;
                if (button != null && button.Equals("update"))
                    return this.EditItem2(model);
                if (button != null && button.Equals("delete"))
                    return this.DeleteItem2(model);

                var req = new Incentive()
                {
                    Max = model.Max,
                    Rank = model.Rank,
                    Reward = model.Reward,
                    Min = model.Min
                };
                Connection.RewardBands.Add(req);
                Connection.SaveChanges();

                TempData["message"] = "Rquest was successful!";

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("Index1", "AgentManager");
        }


    }
}
