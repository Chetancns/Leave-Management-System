using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LMSTRYONE.Services
{
    public class ErrorLogService
    {
        // Whenever an unexpcted error occurs The error is log to the Errorlog textfile.
        // If the error occurs when the user is logged in the Employee ID else Employee ID is 0.
        // Log is in the format of (EmployeeID,ErrorMessage,Date and Time)
        public void LogError(Exception e,int EmpId=0000)
        {
            Debug.WriteLine(e.Message);
            Debug.WriteLine(EmpId);
            string oline = string.Format("Employee_ID={0},Error_Message={1},Date={2}", EmpId, e.Message, DateTime.Now);
            StreamWriter sw = new StreamWriter(@"C:\Users\Dell\OneDrive\Documents\LTI Project\LMSTRYONE\Errorlog.txt", true);
            sw.WriteLine(oline);
            sw.Close();
        }
    }
}