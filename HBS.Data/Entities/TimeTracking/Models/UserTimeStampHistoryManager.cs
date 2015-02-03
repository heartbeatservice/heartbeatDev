using System.Collections.Generic;
using HBS.Data.Entities.TimeTracking.EF;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class UserTimeStampHistoryManager
    {

        public void Add(List<string> roleNames)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
            }
        }
    }
}
