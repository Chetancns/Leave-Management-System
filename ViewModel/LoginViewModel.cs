using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LMSTRYONE.Models;

namespace LMSTRYONE.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        [Display(Name = "Employee Id/PS Number")]
        [Required(ErrorMessage = "Please Enter the Employee Id")]
        [EmployeeCheck(ErrorMessage = "Please Enter the correct Employee Id")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please Enter the Password")]
        [MinLength(8, ErrorMessage = "Password must be minmum 8 char")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}