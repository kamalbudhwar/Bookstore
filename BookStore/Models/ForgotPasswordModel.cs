using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ForgotPasswordModel
    {
        [Required,EmailAddress,Display(Name ="Enter the Registered Email")]
        public String Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
