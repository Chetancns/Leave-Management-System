using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMSTRYONE.Services;

namespace LMSTRYONE.Controllers
{
    public class HomeController : Controller
    {
        ErrorLogService log = new ErrorLogService();
        public ActionResult Index()
        {
            try
            {
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"];
                    TempData.Remove("Message");
                }
                return View();
            }
            catch(Exception e)
            {
                log.LogError(e);
                return View("Error");
            }
        }

        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch(Exception e)
            {
                log.LogError(e);
                return View("Error");
            }
        }

        public ActionResult Contact()
        {try
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            catch (Exception e) 
            { 
                log.LogError(e); 
                return View("Error"); 
            }
        }
    }
}