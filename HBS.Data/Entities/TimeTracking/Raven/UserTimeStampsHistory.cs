using System.Collections.Generic;

namespace HBS.Data.Entities.TimeTracking.Raven
{
    public class UserTimeStampsHistory
    {
        public string UserName { get; set; }
        public string Month { get; set; }
        public List<UserTimeStamps> History { get; set; }
    }
}