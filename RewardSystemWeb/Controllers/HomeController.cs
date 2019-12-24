using Newtonsoft.Json;
using RewardSystemWeb.Entities;
using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RewardSystemWeb.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILoginService _service;
        //private readonly IAuditsRepository AuditsRepository;
        Database Connection;

        public HomeController(ILoginService service, Database _Connection)
        {
            _service = service;
            //AuditsRepository = auditsRepository;
            Connection = _Connection;
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }


        public ActionResult DownloadSampleFile()
        {

            if (CurrentUser == null)
                return RedirectToAction("Login", "Home");
            var activities = _service.GetRecentActivities(CurrentUser.UserId, CurrentUser.Country);
            if (activities.Count > 0)
                activities = activities.OrderByDescending(x => x.TimeStamp).ToList();

            ViewBag.RecentActivities = activities;
            return Download();

        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@ConfigurationManager.AppSettings["sample_path"]);
            string fileName = "SampleReportFormat.xlsx";
            if (CurrentUser.Role == 5 || CurrentUser.Role == 6)
            {
                fileBytes = System.IO.File.ReadAllBytes(@ConfigurationManager.AppSettings["sample_path_headofficeSample"]);
                fileName = "SampleHeadOfficeReportFormat.xlsx";
            }


            //AuditsRepository.AddActivity(new Audit
            //{
            //    AuditDescription = string.Format("Dowloaded Sample TBG Upload File Fomart"),
            //    EventId = 1,
            //    CreatedBy = CurrentUser.UserId,
            //    CreatedDate = DateTime.Now,
            //    Comment = "Request is Approved"
            //});


            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //[Authorize]
        public ActionResult Index()
        {
            
            if (CurrentUser == null)
                return RedirectToAction("Login", "Home");
            ViewBag.RecentActivities = _service.GetRecentActivities(CurrentUser.UserId, CurrentUser.Country);
            return View();
        }



        //[AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            TempData["error"] = null;
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
      
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                var error = "Login failed, Invalid credentials";
                var user = _service.ValidateCredential(model, this.Request.UserHostAddress, ref error);
                if (user != null && user.Role > 0)
                {

                    
                    Session["currentUser"] = JsonConvert.SerializeObject(user);
                    var str = (string)Session["currentUser"];
                    var currentUser = JsonConvert.DeserializeObject<CurrentUser>((string)Session["currentUser"]);

                    var activities = _service.GetRecentActivities(CurrentUser.UserId, CurrentUser.Country);
                    if (activities.Count > 0)
                        activities = activities.OrderByDescending(x => x.TimeStamp).ToList();

                    ViewBag.RecentActivities = activities;
                    return RedirectToLocal(returnUrl);
                }

                error = "Login failed, Invalid credentials";
                ViewBag.ReturnUrl = "";
                TempData["error"] = error;
                return View(model);
            }
            catch (Exception e)
            {
                Logger.Error("Login failed, Invalid credentials", e);
            }
            return View(model);
        }

        public ActionResult Error()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}