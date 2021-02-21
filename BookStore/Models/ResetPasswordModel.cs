using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public String UserId { get; set; }
        [Required]
        public String Token { get; set; }
        [Required,DataType(DataType.Password)]
        public String NewPassword { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("NewPassword")]
        public String ConfirmPassword { get; set; }
        public bool isSuccess { get; set; }

    }
}
