using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RewardSystemWeb.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            try
            {
                if (CurrentUser == null)
                    RedirectToAction("Login", "Home");
            }
            catch (Exception)
            {

                RedirectToAction("Login", "Home");
            }
        }

        private Models.CurrentUser _currentUser;



        public Models.CurrentUser CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;
                try
                {
                    if (!String.IsNullOrEmpty((string)Session["currentUser"]))
                        return _currentUser = JsonConvert.DeserializeObject<Models.CurrentUser>((string)Session["currentUser"]);
                }
                catch (Exception e)
                {
                    Logger.Error("CurrentUser", e);
                }
                RedirectToAction("LogOff", "Home");
                return null;
            }
        }
    }
}