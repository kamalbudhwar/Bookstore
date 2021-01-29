using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Books
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }
        public int TotalPages { get; set; }
        public String Language { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
