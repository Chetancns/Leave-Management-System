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
    
    public partial class Manager
    {
        public int ID { get; set; }
        public int ManagerId { get; set; }
        public int EmployeeId { get; set; }
        public string PriorityLevel { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
    }
}