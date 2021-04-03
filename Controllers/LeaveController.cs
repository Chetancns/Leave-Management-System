using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSTRYONE.ViewModel;
using LMSTRYONE.Services;
using LMSTRYONE.Models;
using System.Diagnostics;

namespace LMSTRYONE.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        ErrorLogService log = new ErrorLogService();
        // GET: Leave
        public ActionResult ApplyeLeave()
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
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult ApplyeLeave(ApplyLeaveViewModel leave)
        {
            try
            {
                LeaveService service = new LeaveService();
                if (leave.StartDate > leave.EndDate)
                {
                    ViewBag.error = "Start Date cannot be greater than the End Date.";
                    return View();
                }
                int EmpId = Convert.ToInt32(User.Identity.Name);
                //Checking if the user has a atleast L1 Manager.
                if (service.ManagerCheck(EmpId)==false)
                {
                    //System.Diagnostics.Debug.WriteLine("L1 Manger");
                    ViewBag.error = "You don't have a L1 Manager. Please correct this before Applying Leave.";
                    return View();
                }
                //Calculate the days between the StartDate and EndDate after removing the Weekends and Holidays. 
                leave.days = service.CaluDays(leave.StartDate, leave.EndDate);
                if (leave.days <= 0)
                {
                    ViewBag.error = "Please check if u are applying the leave on the weekends or Holidays.";
                    return View();
                }
                service.ApplyLeave(EmpId, leave);
                TempData["Message"] = "Leave is applied for "+leave.days+" and Submitted for Approval.";
                return RedirectToAction("LeaveStatus", "Leave");
            }
            catch (Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }
        
        public ActionResult ApproveLeave()
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
                LeaveService service = new LeaveService();
                int EmpId = Convert.ToInt32(User.Identity.Name);
                //Checking if the user is a manager or not. if not redictect to the index.
                if (!service.IsManagerCheck(EmpId))
                {
                    TempData["Error"] = "You Must be a Manager to access this functionality";
                    return RedirectToAction("Index", "Employee");

                }
                else
                {
                    //Getting the list of the Leaves which is to be approved by Him/She.
                    List<Leave> leave = service.LeavesToBeApproved(EmpId);
                    return View(leave);
                }
            }
            catch (Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }
        public ActionResult LeaveStatus()
        {
            try
            {
                LeaveService service = new LeaveService();
                int EmpId = Convert.ToInt32(User.Identity.Name);
                List<Leave> leave = service.LeaveStatus(EmpId);
                return View(leave);
            }
            catch(Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }
        //Displaying the Single Leave Application.
        public ActionResult LeaveApplication(int id)
        {
            try
            {
                LeaveService service = new LeaveService();
                int EmpId = Convert.ToInt32(User.Identity.Name);
                Leave Leave = service.LeaveApplication(id);
                return View(Leave);
            }
            catch(Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }
        public ActionResult LeaveApproval(int id)
        {
            try
            {
                LeaveService service = new LeaveService();
                if (service.LeaveApproval(id, Convert.ToInt32(User.Identity.Name)))
                {
                    TempData["Message"] = "The Leave applied is successfully Approved";
                    return RedirectToAction("ApproveLeave", "Leave");
                }
                else
                {
                    TempData["Error"] = "Sorry some Error Occurred";
                    return RedirectToAction("ApproveLeave", "Leave");
                }
            }
            catch(Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }
        public ActionResult LeaveReject(int id)
        {
            try
            {
                LeaveService service = new LeaveService();
                if (service.LeaveReject(id, Convert.ToInt32(User.Identity.Name)))
                {
                    TempData["Message"] = "The Leave applied is successfully Rejected";
                    return RedirectToAction("ApproveLeave", "Leave");
                }
                else
                {
                    TempData["Error"] = "Sorry some Error Occurred";
                    return RedirectToAction("ApproveLeave", "Leave");
                }
                
            }
            catch (Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }

        [ChildActionOnly]
        public ActionResult LeaveList()
        {
            
            try
            {
                LeaveService service = new LeaveService();
                int EmpId = Convert.ToInt32(User.Identity.Name);
                List<Leave> leave = service.LeavesList(EmpId);
                return PartialView("_MLeave",leave);

            }
            catch (Exception e)
            {
                log.LogError(e, Convert.ToInt32(User.Identity.Name));
                return View("Error");
            }
        }
    }
    
}