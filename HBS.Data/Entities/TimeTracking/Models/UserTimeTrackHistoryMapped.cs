using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HBS.Data.Entities.TimeTracking.EF;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class UserTimeTrackHistoryMapped
    {
        public int TimeTrackId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string ClockInTime { get; set; }
        public string ClockOutTime { get; set; }
        public DateTime StampDate { get; set; }
        public string UserIp { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdateBy { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> ClockInTimeList { get; set; }
        public IEnumerable<SelectListItem> ClockOutTimeList { get; set; }

        public UserTimeTrackHistoryMapped()
        {
            UserName = string.Empty;
            ClockInTime = string.Empty;
            ClockOutTime = string.Empty;
            IsDeleted = false;
            StampDate = DateTime.Now.Date;
            if (!string.IsNullOrEmpty(UserName))
                Users = new SelectList(MembershipUserExtended.GetExtendedMembershipUserCollection(), "UserName", "FullName", UserName);
            else
                Users = new SelectList(MembershipUserExtended.GetExtendedMembershipUserCollection(), "UserName", "FullName");

            ClockInTimeList = new SelectList(GetTimeListOfADay(15), ClockInTime);
            ClockOutTimeList = new SelectList(GetTimeListOfADay(15), ClockOutTime);
        }

        List<string> GetTimeListOfADay(int interval)
        {
            var sbTimeList = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                var index = (60 / interval);
                var intervalIncrement = 0;

                for (int j = 0; j < index; j++)
                {
                    sbTimeList.Add(string.Format("{0}:{1}:00", i < 10 ? "0" + i.ToString() : i.ToString(), intervalIncrement == 0 ? "0" + intervalIncrement.ToString() : intervalIncrement.ToString()));

                    intervalIncrement += interval;
                }
            }

            return sbTimeList.Where(c => !c.Equals("23:60")).ToList();
        }

        public int Save()
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                var utth = new UserTimeTrackHistory
                    {
                        UserId = UserId,
                        UserName = UserName,
                        ClockInTime = ClockInTime,
                        ClockOutTime = ClockOutTime,
                        StampDate = StampDate,
                        UserIP = UserIp,
                        IsDeleted = IsDeleted,
                        CreatedBy = CreatedBy,
                        CreatedDate = CreatedDate
                    };
                dbContext.UserTimeTrackHistories.Add(utth);
                dbContext.SaveChanges();
                return utth.TimeTrackId;
            }
        }
        public UserTimeTrackHistoryMapped Get(int timeTrackId)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                var userTimeTrackRecord =
                    dbContext.UserTimeTrackHistories.FirstOrDefault(c => c.TimeTrackId == timeTrackId);
                if (userTimeTrackRecord != null)
                {
                    var timeTrackMappedRecord = new UserTimeTrackHistoryMapped()
                        {
                            TimeTrackId = userTimeTrackRecord.TimeTrackId,
                            UserName = userTimeTrackRecord.UserName,
                            UserId = userTimeTrackRecord.UserId,
                            ClockInTime = userTimeTrackRecord.ClockInTime,
                            ClockOutTime = userTimeTrackRecord.ClockOutTime,
                            StampDate = userTimeTrackRecord.StampDate,
                            IsDeleted = userTimeTrackRecord.IsDeleted,
                            CreatedBy = userTimeTrackRecord.CreatedBy,
                            CreatedDate = userTimeTrackRecord.CreatedDate
                        };
                    return timeTrackMappedRecord;
                }
                return new UserTimeTrackHistoryMapped();
            }
        }
    }
}
