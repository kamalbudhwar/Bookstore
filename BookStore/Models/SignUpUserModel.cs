using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please Enter your First Name")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter your Last Name")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        [Required(ErrorMessage ="Please Enter your email")]
        [Display(Name ="Email")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Please Enter your Date of Birth")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Enter your Strong Password")]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required(ErrorMessage = "Please Enter your Password again")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }
    }
}
