//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMSTRYONE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Leave
    {
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public int days { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
    }
}
