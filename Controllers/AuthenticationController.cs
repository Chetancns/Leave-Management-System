using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMSTRYONE.Models;
using LMSTRYONE.Services;
using LMSTRYONE.ViewModel;

namespace LMSTRYONE.Controllers
{
    public class AuthenticationController : Controller
    {
        ErrorLogService log = new ErrorLogService();

        // GET: Authentication
        public ActionResult Register()
        {
            try
            {
                return View();
            }
            catch(Exception e)
            {
                log.LogError(e);
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AuthenticationService auth = new AuthenticationService();
                    auth.Register(reg);
                    TempData["Message"] = "Registration was Successfully and Please Login with same credentials";
                    return RedirectToAction("UserLogin", "Authentication");
                }
                return View(reg);
            }
            catch(Exception e)
            {
                log.LogError(e);
                return View("Error");
            }
           
        }
        public ActionResult UserLogin()
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
                    Debug.WriteLine("Hii");
                    ViewBag.error = TempData["Error"];
                    TempData.Remove("Error");
                }
                return View();
            }
            catch (Exception e)
            {
                log.LogError(e);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult UserLogin(LoginViewModel logUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AuthenticationService auth = new AuthenticationService();
                    if (auth.UserCheck(logUser.EmployeeId))
                    {
                        bool validUser = auth.Login(logUser);
                        
                        if (validUser)
                        {
                            FormsAuthentication.SetAuthCookie(Convert.ToString(logUser.EmployeeId), false);
                            TempData["Message"] = "Login was Successful";
                            return RedirectToAction("Index", "Employee");
                        }
                        else
                        {
                            ViewBag.error = "Please Enter the correct Employee Id and Password";
                            return View();
                        }
                        
                    }
                    else
                    {
                        ViewBag.error = "Please Register first to Login to the application";
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please Enter the valid values");
                    return View();
                }
            }
            catch (Exception e)
            {
                log.LogError(e);
                return View("Error");
            }
        }
        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                TempData["Message"] = "Log Out was Successful";
                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                log.LogError(e);
                return View("Error");
            }
        }
    }
}