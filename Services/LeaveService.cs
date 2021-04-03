using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSTRYONE.ViewModel;
using LMSTRYONE.Models;
using System.Diagnostics;
using System.Data.Entity;

namespace LMSTRYONE.Services
{
    public class LeaveService
    {

        //calculate the Days of the leave by excluding the Weekends and Holidays;

        public int CaluDays(DateTime sDate,DateTime eDate)
        {
            try
            {
                List<string> HolidayList = Holidays();
                int days= (eDate - sDate).Days + 1;
                for (DateTime index = sDate; index <= eDate; index = index.AddDays(1))
                {
                    if (index.DayOfWeek == DayOfWeek.Saturday || index.DayOfWeek == DayOfWeek.Sunday || HolidayList.Contains(index.ToShortDateString()))
                    {
                        days--;
                    }
                }
                return days;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //To check if the user has at least L1 Manager.
        public bool ManagerCheck(int EmployeeId)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    Manager m = db.Managers.Where(q => q.EmployeeId == EmployeeId).Where(q=>q.PriorityLevel=="L1").FirstOrDefault();
                    if (m != null)
                    {
                        Debug.WriteLine(m.ManagerId + " " + m.EmployeeId);
                        return true;
                    } 
                    else
                    {
                        Debug.WriteLine("There is no L1 Manager");
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public void ApplyLeave(int EmployeeId,ApplyLeaveViewModel leave)
        {
            try
            {
                
                using(LMSEntities db=new LMSEntities())
                {
                    Manager m = db.Managers.Where(q => q.EmployeeId == EmployeeId).Where(q => q.PriorityLevel == "L1").FirstOrDefault();
                    Leave applyleave = new Leave();
                    applyleave.EmployeeId = EmployeeId;
                    applyleave.ManagerId = m.ManagerId;
                    applyleave.StartDate = leave.StartDate;
                    applyleave.EndDate = leave.EndDate;
                    applyleave.Reason = leave.Reason;
                    applyleave.days = leave.days;
                    applyleave.Status = Status.ForApproval;
                    db.Leaves.Add(applyleave);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //To check if the user is a Manager or not
        public bool IsManagerCheck(int EmpId)
        {
            try
            {
                using(LMSEntities db=new LMSEntities())
                {
                    Manager m = db.Managers.Where(q => q.ManagerId == EmpId).FirstOrDefault();
                    if (m != null)
                    {
                        //Debug.WriteLine(m.ManagerId + " " + m.EmployeeId);
                        return true;
                    }
                    else
                    {
                        //Debug.WriteLine("The user is not a Manager");
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Leave> LeavesToBeApproved(int EmpId)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    var leave = from l in db.Leaves where l.ManagerId==EmpId && l.Status==Status.ForApproval select l;
                    return leave.ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<Leave> LeaveStatus(int EmpId)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    var leave = from l in db.Leaves where l.EmployeeId==EmpId select l;
                    return leave.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Leave LeaveApplication(int LeaveId)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    Leave l = db.Leaves.Include(q => q.Employee).Where(q =>q.LeaveId==LeaveId).FirstOrDefault();
                    return l;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool LeaveApproval(int LeaveId,int EmpId)
        {
            try
            {
                using (LMSEntities db=new LMSEntities())
                {
                    Leave l = db.Leaves.Find(LeaveId);
                    //if (EmpId != l.ManagerId) return false;
                    l.Status = Status.Approved;
                    db.Entry(l).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool LeaveReject(int LeaveId, int EmpId)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    Leave l = db.Leaves.Find(LeaveId);
                    //if (EmpId != l.ManagerId) return false;
                    l.Status = Status.Rejected;
                    db.Entry(l).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //To list the LeaveApplication for L2 or L3 Managers.
        public List<Leave> LeavesList(int EmpId)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    var Eid =from m in db.Managers where m.ManagerId == EmpId &&( m.PriorityLevel=="L2" || m.PriorityLevel=="L3") select m.EmployeeId;
                    foreach(int i in Eid) {
                        Debug.WriteLine(EmpId);
                        Debug.WriteLine(i); }
                    var leave = from l in db.Leaves where Eid.Contains(l.EmployeeId) && l.Status == Status.ForApproval select l;
                    return leave.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //this is the list of the Holidays for the current year.
        public List<string> Holidays()
        {
            List<string> HolidayList = new List<string>();
            HolidayList.Add((new DateTime(2021, 01, 01)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 01, 26)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 03, 29)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 04, 02)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 04, 13)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 05, 14)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 09, 10)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 10, 15)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 11, 04)).ToShortDateString());
            HolidayList.Add((new DateTime(2021, 12, 24)).ToShortDateString());
            return HolidayList;
        }
    }
}