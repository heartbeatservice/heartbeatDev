using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Entities
{
    public class KendoEntity
    {
        public string TaskID{get;set;}
          public string   OwnerID{get;set;}
           public string  Title{get;set;}
           public string Description{get;set;}
          public string StartTimezone{get;set;}
           public string    Start{get;set;}
        public string End{get;set;}
          public string  EndTimezone{get;set;}
          public string  RecurrenceRule{get;set;}
        public string RecurrenceID{get;set;}
       public string  RecurrenceException{get;set;}
       public bool IsAllDay { get; set; }

       public int ProfessionalId { get; set; }

       public int UserId { get; set; }
    }
}
