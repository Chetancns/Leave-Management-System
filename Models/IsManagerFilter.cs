using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMSTRYONE.Models
{
    public class IsManagerFilter:ActionFilterAttribute
    {
        public int EmpID { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Debug.WriteLine("I Passed through the Filter");
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Employee" },
                                          { "action", "Index" }
                                         });
        }
    }
}