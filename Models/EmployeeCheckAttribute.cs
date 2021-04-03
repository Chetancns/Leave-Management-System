using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LMSTRYONE.Models
{
    public class EmployeeCheckAttribute:ValidationAttribute
    {
        public EmployeeCheckAttribute() { }
        public override bool IsValid(object value)
        {
            int Id = Convert.ToInt32(value);
            using (LMSEntities db=new LMSEntities())
            {
                if (db.Employees.Any(q => q.EmployeeId == Id))
                {
                   // Debug.WriteLine("The Employees is present");
                    return true;
                }
                else {
                    //Debug.WriteLine("The Employee is not present");
                    return false;
                   
                } 
            }
        }
    }
}