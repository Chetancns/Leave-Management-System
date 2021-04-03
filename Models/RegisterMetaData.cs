using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSTRYONE.Models
{
    public class RegisterMetaData
    {
        [Display(Name ="Employee Id/PS Number")]
        [Required(ErrorMessage ="Please Enter the Employee Id")]
        [EmployeeCheck(ErrorMessage ="Please Enter the correct Employee Id")]
        [RegistrationCheck(ErrorMessage ="Your already Registered Please login")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Please Enter the Password")]
        [MinLength(8,ErrorMessage ="Password must be minmum 8 char")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Salt { get; set; }
    }
    [MetadataType(typeof(RegisterMetaData))]
    public partial class Register { }
}