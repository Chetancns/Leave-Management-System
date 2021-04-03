using LMSTRYONE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSTRYONE.ViewModel
{
    public class RegisterViewModel
    {
        [Key]
        [Display(Name = "Employee Id/PS Number")]
        [Required(ErrorMessage = "Please Enter the Employee Id")]
        [EmployeeCheck(ErrorMessage = "Please Enter the correct Employee Id")]
        [RegistrationCheck(ErrorMessage = "Your already Registered Please login")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Please Enter the Password")]
        [MinLength(8, ErrorMessage = "Password must be minmum 8 char")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassowrd { get; set; }
        public string Salt { get; set; }
    }
}