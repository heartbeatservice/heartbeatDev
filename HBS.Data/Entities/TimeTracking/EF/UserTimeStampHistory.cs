using System;
using System.Collections.Generic;
using System.Linq;

namespace HBS.Data.Entities.TimeTracking.EF
{
    public partial class UserTimeTrackHistory
    {
        public void Save()
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                dbContext.UserTimeTrackHistories.Add(this);
                dbContext.SaveChanges();
            }
        }

        public static List<UserTimeTrackHistory> GetUserTimeStampHistory(string userName)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                var historyList = dbContext.UserTimeTrackHistories.Where(c=>c.UserName.ToLower().Equals(userName.ToLower())).ToList();

                foreach (var utsh in historyList)
                {
                    DateTime time;
                    if (DateTime.TryParse(utsh.ClockInTime, out time))
                        utsh.ClockInTime = string.Format("{0:t}", time);

                    if (DateTime.TryParse(utsh.ClockOutTime, out time))
                        utsh.ClockOutTime = string.Format("{0:t}", time);

                }
                return historyList;
            }
        }
    }
}
