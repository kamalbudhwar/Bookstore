using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Helper
{
    public class MyCustomValidatatlonAttribute : ValidationAttribute
    {
        public MyCustomValidatatlonAttribute(String text)
        {
            Text = text;
        }
        public String Text{get;set;}
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null) {
                string bookname = value.ToString();
                if (bookname.Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("value is empty");
        }
    }
}
