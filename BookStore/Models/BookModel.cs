using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Data;
using BookStore.Helper;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100,MinimumLength=4)]
        [Required(ErrorMessage ="Please enter the Title of the Book")]
        public String Title { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter the Author Name of the Book")]
        public String Author { get; set; }
        [StringLength(200, MinimumLength = 4)]
        public String Description { get; set; }
        public String Category { get; set; }
        [Required(ErrorMessage ="Please enter the Total Number of Pages of the Book")]
        [Display(Name ="Total Pages")]
        public int? TotalPages { get; set; }

        [Required(ErrorMessage = "Please enter the Language of the book")]
        public int LanguageId { get; set; }

        public String Language { get; set; }

        [Display(Name ="Cover Photo")]
        [Required(ErrorMessage ="Choose the Cover photo of your book")]
        public IFormFile CoverPhoto { get; set;}

        [Display(Name = "Gallery Photo")]
        [Required(ErrorMessage = "Choose the Gallery photos for your book")]
        public IFormFileCollection GalleryFiles { get; set; }
        public String CoverImageUrl { get; set; }

        public List<GalleryModel> Gallery { get; set; }

    }
}
