using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMSTRYONE.Models
{
    public class LeaveMetaData
    {
        [Display(Name ="Ref No")]
        public int LeaveId { get; set; }
        [Display(Name = "Employee Id/PS Number")]
        public int EmployeeId { get; set; }
        [Display(Name = "Manager Id")]
        public int ManagerId { get; set; }
        [Display(Name ="Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime StartDate { get; set; }
        [Display(Name ="Till Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime EndDate { get; set; }
        [Display(Name ="Leave Status")]
        public string Status { get; set; }
        [Display(Name ="Reason for Leave")]
        public string Reason { get; set; }
        [Display(Name ="Number of Days")]
        public int days { get; set; }
    }
    [MetadataType(typeof(LeaveMetaData))]
    public partial class Leave { }
}