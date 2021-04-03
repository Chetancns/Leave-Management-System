using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSTRYONE.Models;

namespace LMSTRYONE.Services
{
    public class EmployeeService
    {
        // This method is used to get the Employee Name using the Employee ID from the database.
        public string GetEmployeeName(int Empid)
        {
            try
            {
                string Name = "";
                using (LMSEntities db = new LMSEntities())
                {
                    Name = db.Employees.Where(q => q.EmployeeId == Empid).Select(q => q.EmployeeName).FirstOrDefault();
                    return Name;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}