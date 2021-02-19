using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SMTPConfigModel
    {
        public String SenderAddress { get; set; }
        public String SenderDispalyName { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public int  Port { get; set; }
        public String Host { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCrendentials { get; set; }
        public bool IsBodyHTML { get; set; }
    }
}
 