using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Data;
using BookStore.Helper;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100,MinimumLength=4)]
        //[Required(ErrorMessage ="Please enter the Title of the Book")]
        [MyCustomValidatatlonAttribute("java")]
        public String Title { get; set; }
        [StringLength(100, MinimumLength = 4)]
        [Required(ErrorMessage = "Please enter the Author Name of the Book")]
        public String Author { get; set; }
        [StringLength(100, MinimumLength = 10)]
        public String Description { get; set; }
        public String Category { get; set; }
        [Required(ErrorMessage ="Please enter the Total Number of Pages of the Book")]
        [Display(Name ="Total Pages")]
        public int? TotalPages { get; set; }

        [Required(ErrorMessage = "Please enter the Language of the book")]
        public int LanguageId { get; set; }

        public String Language { get; set; }
    }
}
