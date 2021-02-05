using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Language
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String des { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
