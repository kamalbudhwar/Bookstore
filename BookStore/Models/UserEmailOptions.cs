using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class UserEmailOptions
    {
        public List<String> ToEmails { get; set; }
        public String Subject { get; set; }

        public String Body { get; set; }
        public List<KeyValuePair<String, String>> Placeholders { get; set; }

    }
}
