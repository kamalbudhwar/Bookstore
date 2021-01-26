using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }
        public int TotalPages { get; set; }
        public String Language { get; set; }
    }
}
