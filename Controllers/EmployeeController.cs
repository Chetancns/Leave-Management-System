using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSTRYONE.Services;

namespace LMSTRYONE.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        ErrorLogService log = new ErrorLogService();
        EmployeeService empService = new EmployeeService();
        // GET: Employee
        public ActionResult Index()
        {
            try
            {
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"];
                    TempData.Remove("Message");
                }
                if (TempData["Error"] != null)
                {
                    ViewBag.error = TempData["Error"];
                    TempData.Remove("Error");
                }
                ViewBag.EName = empService.GetEmployeeName(Convert.ToInt32(User.Identity.Name));
                return View();
            }
            catch (Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }

        }
        [ChildActionOnly]
        public ActionResult EmployeeName()
        {
            ViewBag.Name = empService.GetEmployeeName(Convert.ToInt32(User.Identity.Name));
            return PartialView("_EName");
        }

     
    }
}