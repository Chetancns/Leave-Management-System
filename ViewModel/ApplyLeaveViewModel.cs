using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMSTRYONE.ViewModel
{
    public class ApplyLeaveViewModel
    {
        [Key]
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MMM-dd}")]
        public System.DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Till Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MMM-dd}")]
        public System.DateTime EndDate { get; set; }
        [Required]
        [Display(Name ="Reason to Apply Leave")]
        public string Reason { get; set; }
        [Required]
        public int days { get; set; }
    }
}