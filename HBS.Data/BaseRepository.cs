using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Data
{
    public class BaseRepository
    {
        public string PrescienceRxConnectionString { get; set; }


        public BaseRepository()
        {
            PrescienceRxConnectionString = ConfigurationManager.ConnectionStrings["SchedulingConnectionString"].ConnectionString;
        }
    }
}
