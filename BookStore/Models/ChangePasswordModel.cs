using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ChangePasswordModel
    {
        [Required ,DataType(DataType.Password),Display(Name ="Current Password")]
        public String CurrentPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public String NewPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Confirm New Password")]
        [Compare("NewPassword",ErrorMessage="New password does not match") ]
        public String ConfirmNewPassword { get; set; }
    }
}
