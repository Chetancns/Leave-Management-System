using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LMSTRYONE.Models
{
    public class RegistrationCheckAttribute:ValidationAttribute
    {
        public RegistrationCheckAttribute() { }
        public override bool IsValid(object value)
        {
            using(LMSEntities db=new LMSEntities())
            {
                int Id = Convert.ToInt32(value);
                if (db.Registers.Any(q => q.EmployeeId == Id))
                {
                    Debug.WriteLine("User Present in the Registration Table");
                    //The user is already present in the Register table so ask user to login
                    return false;
                }
                else
                {
                    Debug.WriteLine("User is not Present in the Registration Table");
                    //The user is new so allow him to Register
                    return true;
                }
            }
        }
    }
}