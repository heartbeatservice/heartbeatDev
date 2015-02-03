using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Entities
{
    public class EmailMessage
    {
       public  string FullName { get; set; }
        public string Question { get; set; }
        public string SenderEmail { get; set; }

        public string ToEmail { get; set; }

        public string serverAddress { get; set; }
        public string pwd { get; set; }

        public string Captcha { get; set; }

    }
}
