using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using HBS.Data.Entities.TimeTracking.EF;
using HBS.Data.Entities.TimeTracking.Infrastructure;
using HBS.Data.Entities.TimeTracking.ViewModels;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public static class TimeTrackManager
    {
        public static DailyTimeTrack GetCurrentDayClockInOutTime(string userName)
        {
            var startDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]).Date;
            var endDate = startDate.AddDays(1);
            using (var dbContext = new TimeTrackingEntities())
            {
                var currentUser =
                       dbContext.ExtendedUserProfiles.FirstOrDefault(c => c.UserName.ToLower().Equals(userName));

                var userClockInOutTimeList = (from utsh in dbContext.UserTimeTrackHistories
                                              where utsh.IsDeleted==false &&
                                              utsh.UserName.ToLower().Equals(userName.ToLower()) &&
                                              (utsh.StampDate >= startDate && utsh.StampDate < endDate) &&
                                              utsh.ClockInTime.Length > 0
                                              select utsh).ToList();

                if (userClockInOutTimeList.Any())
                {
                    var userTimeStampHistory = userClockInOutTimeList.FirstOrDefault();
                    if (userTimeStampHistory != null)
                    {
                        var dailyTimeTrack = new DailyTimeTrack(userTimeStampHistory.StampDate, currentUser.HourlyRate.HasValue ? currentUser.HourlyRate.Value : 0)
                            {
                                TimeTrackList =
                                    GetTimeTrackList(userClockInOutTimeList, userTimeStampHistory.StampDate).
                                        OrderByDescending(c => c.ClockInTime).ThenByDescending(
                                            d => d.ClockOutTime).ToList()
                            };

                        dailyTimeTrack.SubmitButtonText = dailyTimeTrack.TimeTrackList.Any(c => c.ClockOutTime == null) ? WebConfigurationManager.AppSettings["ClockOutText"] : WebConfigurationManager.AppSettings["ClockInText"];
                        return dailyTimeTrack;
                    }
                }
                return new DailyTimeTrack();
            }
        }

        static IEnumerable<TimeTrack> GetTimeTrackList(IEnumerable<UserTimeTrackHistory> userTimeTrackHistories, DateTime givenDate)
        {
            return userTimeTrackHistories.Select(utsh => new TimeTrack(utsh.TimeTrackId, givenDate, utsh.ClockInTime, utsh.ClockOutTime)).ToList();
        }

        public static WeeklyTimeTrackWeekListViewModel GetCurrentWeekClockInOutTime(string userName)
        {
            var weekManager = new WeekManager(WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]));
            return GetClockInOutTime(userName, weekManager.WeekStartDate, weekManager.WeekEndDate);
        }

        public static WeeklyTimeTrackWeekListViewModel GetClockInOutTime(string userName, DateTime startDate, DateTime endDate)
        {
            var weeklyTimeTrack = GetWeeklyClockInOutTimeByDate(userName, startDate, endDate);
            var selectedValue = weeklyTimeTrack.WeekStartDate.HasValue
                                    ? weeklyTimeTrack.WeekStartDate.Value.ToString()
                                    : string.Empty;
            return new WeeklyTimeTrackWeekListViewModel
                        {
                            WeeklyTimeTrack = weeklyTimeTrack,
                            SelectedValue = selectedValue,
                            UserName = userName,
                            WeekList = new SelectList(WeekManager.GetWeekList(WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]), Convert.ToInt32(WebConfigurationManager.AppSettings["NumberOfPriorWeeks"]), true), "WeekStartDate", "WeekStartEndDateDisplay", selectedValue)
                        };
        }

        public static WeeklyTimeTrack GetWeeklyClockInOutTimeByDate(string userName, DateTime startDate, DateTime endDate)
        {
            var weekEndDateToSearchInDatabase = endDate.AddDays(1);

            var weeklyTimeTrack = new WeeklyTimeTrack
            {
                WeekStartDate = startDate.Date,
                WeekEndDate = endDate.Date,
                DailyTimeTracks = new List<DailyTimeTrack>()
            };
            using (var dbContext = new TimeTrackingEntities())
            {
                var currentUser =
                    dbContext.ExtendedUserProfiles.FirstOrDefault(c => c.UserName.ToLower().Equals(userName));
                var userWeeklyClockInOutTimings = (from utsh in dbContext.UserTimeTrackHistories
                                                   where utsh.IsDeleted == false &&
                                                     utsh.UserName.ToLower().Equals(userName.ToLower()) &&
                                                     (utsh.StampDate >= startDate && utsh.StampDate < weekEndDateToSearchInDatabase) &&
                                                     utsh.ClockInTime.Length > 0
                                                   select utsh).ToList();

                if (userWeeklyClockInOutTimings.Any())
                {
                    var weekDay = startDate.Date;

                    while (weekDay.Date <= endDate.Date)
                    {
                        var dailyUserStampList =
                            userWeeklyClockInOutTimings.Where(
                                daily =>
                                daily.StampDate >= weekDay &&
                                daily.StampDate < weekDay.AddDays(1)).ToList();

                        var dailyTimeTrack = new DailyTimeTrack(startDate, currentUser.HourlyRate.HasValue ? currentUser.HourlyRate.Value : 0)
                        {
                            StampDate = weekDay,
                            TimeTrackList =
                                GetTimeTrackList(dailyUserStampList, weekDay).
                                    OrderByDescending(c => c.ClockInTime).ThenByDescending(
                                        d => d.ClockOutTime).ToList()
                        };


                        weeklyTimeTrack.DailyTimeTracks.Add(dailyTimeTrack);
                        weeklyTimeTrack.DailyTimeTracks = weeklyTimeTrack.DailyTimeTracks.OrderByDescending(c => c.StampDate).ToList();
                        weekDay = weekDay.AddDays(1);
                    }
                }
            }
            return weeklyTimeTrack;
        }
        public static DailyTimeTrack GetDailyClockInOutTimeByDate(string userName, DateTime startDate, DateTime endDate)
        {
            var weekEndDateToSearchInDatabase = endDate.AddDays(1);

           
            using (var dbContext = new TimeTrackingEntities())
            {
                var currentUser =
                    dbContext.ExtendedUserProfiles.FirstOrDefault(c => c.UserName.ToLower().Equals(userName));
                var userTimeTrackHistoryForRequestedDay = (from utsh in dbContext.UserTimeTrackHistories
                                                           where utsh.IsDeleted == false &&
                                                            utsh.UserName.ToLower().Equals(userName.ToLower()) &&
                                                            (utsh.StampDate >= startDate && utsh.StampDate < weekEndDateToSearchInDatabase) &&
                                                            utsh.ClockInTime.Length > 0
                                                            select utsh).ToList();

                if (userTimeTrackHistoryForRequestedDay.Any())
                {
                        var dailyTimeTrack = new DailyTimeTrack(startDate, currentUser.HourlyRate.HasValue ? currentUser.HourlyRate.Value : 0)
                        {
                            StampDate = startDate,
                            TimeTrackList =
                                GetTimeTrackList(userTimeTrackHistoryForRequestedDay, startDate).
                                    OrderByDescending(c => c.TimeTrackId).ThenByDescending(
                                        d => d.ClockInTime).ToList()
                        };
                    return dailyTimeTrack;
                }
            }
            return new DailyTimeTrack();
        }

        public static CustomTimeTrack GetUserTimeTrackHistoryForSpecifiedPeriod(string userName, DateTime startDate, DateTime endDate)
        {
            var weekEndDateToSearchInDatabase = endDate.AddDays(1);

            var customTimeTrack = new CustomTimeTrack
            {
                CustomStartDate = startDate.Date,
                CustomEndDate = endDate.Date,
                UserName=userName,
                DailyTimeTracks = new List<DailyTimeTrack>()
            };

            using (var dbContext = new TimeTrackingEntities())
            {
                var currentUser = dbContext.ExtendedUserProfiles.FirstOrDefault(c => c.UserName.ToLower().Equals(userName));

                customTimeTrack.EmployeeName = currentUser.FirstName + " " + currentUser.LastName;

                var userClockInOutTimings = (from utsh in dbContext.UserTimeTrackHistories
                                             where utsh.IsDeleted == false &&
                                               utsh.UserName.ToLower().Equals(userName.ToLower()) &&
                                               (utsh.StampDate >= startDate && utsh.StampDate < weekEndDateToSearchInDatabase) &&
                                               utsh.ClockInTime.Length > 0
                                             select utsh).ToList();

                if (userClockInOutTimings.Any())
                {
                    var currentDay = startDate.Date;

                    while (currentDay <= endDate.Date)
                    {
                        var dailyUserStampList =
                            userClockInOutTimings.Where(
                                daily =>
                                daily.StampDate >= currentDay && daily.StampDate < currentDay.AddDays(1)).ToList();

                        var dailyTimeTrack = new DailyTimeTrack(startDate, currentUser.HourlyRate.HasValue ? currentUser.HourlyRate.Value : 0)
                        {
                            StampDate = currentDay,
                            TimeTrackList =
                                GetTimeTrackList(dailyUserStampList, currentDay).
                                    OrderByDescending(c => c.ClockInTime).ThenByDescending(
                                        d => d.ClockOutTime).ToList()
                        };


                        customTimeTrack.DailyTimeTracks.Add(dailyTimeTrack);
                        customTimeTrack.DailyTimeTracks = customTimeTrack.DailyTimeTracks.OrderByDescending(c => c.StampDate).ToList();
                        currentDay = currentDay.AddDays(1);
                    }
                }
            }

            return customTimeTrack;
        }

        public static void GetUserAndHistory(string userName, DateTime startDate)
        {
            var weekEndDateToSearchInDatabase = startDate.AddDays(2);
            using (var dbContext = new TimeTrackingEntities())
            {
                var userClockInOutTimeList = (from utsh in dbContext.UserTimeTrackHistories
                                              join user in dbContext.ExtendedUserProfiles on utsh.UserId equals user.UserId
                                              where utsh.IsDeleted == false &&  utsh.UserName.ToLower().Equals(userName.ToLower()) && utsh.ClockInTime.Length > 0
                                              select new
                                                  {
                                                      historylist = utsh,
                                                      currentuser = user
                                                  }).ToList();

                var user1 = userClockInOutTimeList.FirstOrDefault().currentuser;
                var history = userClockInOutTimeList.FirstOrDefault().historylist;

            }
        }

        [Authorize]
        public static void TrackClockInOutTime(string userName)
        {
            var startDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]).Date;
            var endDate = startDate.AddDays(1);
            SetDefaultClockoutTimeStampToForgottenStamps(userName);
            using (var dbContext = new TimeTrackingEntities())
            {
                var usersLatestRecord = (from utsh in dbContext.UserTimeTrackHistories
                                         where utsh.IsDeleted == false &&
                                             utsh.UserName.ToLower().Equals(userName.ToLower()) &&
                                             (utsh.StampDate >= startDate && utsh.StampDate < endDate) &&
                                             utsh.ClockInTime.Length > 0 && (utsh.ClockOutTime == null || utsh.ClockOutTime.Length == 0)
                                         select utsh).OrderByDescending(c => c.StampDate).ThenByDescending(c => c.CreatedDate).FirstOrDefault();

                if (usersLatestRecord != null) // User has clocked in, Clock user out
                {
                    usersLatestRecord.ClockOutTime = string.Format("{0:t}", WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]));
                    usersLatestRecord.UpdatedBy = userName;
                    usersLatestRecord.UpdatedDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]);
                    usersLatestRecord.UserIP = WebHelpers.GetIpAddress();
                }
                else // User hasn't clocked in yet, Clock user in
                {
                    var userTimeStampHistory = new UserTimeTrackHistory
                        {
                            UserId = MembershipUserExtended.GetUserIdByUserName(userName),
                            UserName = userName,
                            ClockInTime = string.Format("{0:t}", WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"])),
                            StampDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]),
                            CreatedBy = userName,
                            CreatedDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]),
                            UserIP = WebHelpers.GetIpAddress(),
                            IsDeleted = false
                        };
                    dbContext.UserTimeTrackHistories.Add(userTimeStampHistory);
                }
                dbContext.SaveChanges();
            }
        }

        [Authorize]
        public static TimeTrackErrorViewModel UpdateClockInOutTime(int timeTrackId, string stampDate, string selectedUser, string clockInTime, string clockOutTime, string updatedBy)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                var startDate = DateTime.Parse(stampDate);
                var endDate = startDate.AddDays(1);
                var userTimeTrackRecordForStampedDate = dbContext.UserTimeTrackHistories
                                                       .Where(utsh => utsh.IsDeleted == false && utsh.UserName.ToLower().Equals(selectedUser.ToLower()) && (utsh.StampDate >= startDate && utsh.StampDate < endDate))
                                                       .ToList();

                var userTimeTrackRecord = userTimeTrackRecordForStampedDate.FirstOrDefault(utsh => utsh.TimeTrackId == timeTrackId);
                var indexOfSelectedRecord = userTimeTrackRecordForStampedDate.IndexOf(userTimeTrackRecord);


                var errorMessage = new StringBuilder();
                var errorCount = 0;
                var sDate = new DateTime();

                if (userTimeTrackRecord != null)
                {
                    DateTime updatedClockInDateTime;
                    DateTime updatedClockOutDateTime;
                    if (DateTime.TryParse(clockInTime, out updatedClockInDateTime) && DateTime.TryParse(clockOutTime, out updatedClockOutDateTime))
                    {
                        var updatedClockInTime = updatedClockInDateTime.TimeOfDay;
                        var updatedClockOutTime = updatedClockOutDateTime.TimeOfDay;

                        // CHECK IF userTimeTrackRecord object has CloclOutTime set. IF NOT SET IT WITH PROVIDED ClockOutTime
                        if (string.IsNullOrEmpty(userTimeTrackRecord.ClockOutTime))
                            userTimeTrackRecord.ClockOutTime = String.Format("{0:t}", sDate.Add(updatedClockOutTime));

                        var previousTimeTrackClockOutTimeRecord = string.Empty;
                        var nextTimeTrackClockInTimeRecord = string.Empty;

                        if (indexOfSelectedRecord > 0 && (indexOfSelectedRecord - 1) >= 0)
                            previousTimeTrackClockOutTimeRecord =
                                userTimeTrackRecordForStampedDate[indexOfSelectedRecord - 1].ClockOutTime;

                        if ((indexOfSelectedRecord + 1) < userTimeTrackRecordForStampedDate.Count)
                            nextTimeTrackClockInTimeRecord =
                                userTimeTrackRecordForStampedDate[indexOfSelectedRecord + 1].ClockInTime;

                        if (updatedClockOutTime.CompareTo(updatedClockInTime) == -1)
                        {
                            errorMessage.Append("Updated Clock Out time can not be earlier than Updated Clock In time.");
                            errorCount += 1;
                        }
                        else if (!updatedClockInTime.Equals(DateTime.Parse(userTimeTrackRecord.ClockInTime).TimeOfDay))
                        {
                            if ((!string.IsNullOrEmpty(previousTimeTrackClockOutTimeRecord) &&
                                 updatedClockInTime.CompareTo(
                                     DateTime.Parse(previousTimeTrackClockOutTimeRecord).TimeOfDay) == -1) ||
                                (!string.IsNullOrEmpty(nextTimeTrackClockInTimeRecord) &&
                                 updatedClockInTime.CompareTo(DateTime.Parse(nextTimeTrackClockInTimeRecord).TimeOfDay) ==
                                 1))
                            {
                                // error clock in time cannot be earlier then previous checkout time and can not be later then next clockout time
                                errorMessage.Append(
                                    "Updated Clock In time can not be earlier than previous clock out time or later than earlier clock in time");
                                errorCount += 1;
                            }
                        }
                        else if (!updatedClockOutTime.Equals(DateTime.Parse(userTimeTrackRecord.ClockOutTime).TimeOfDay))
                        {
                            // Clock Out time has changed
                            if ((!string.IsNullOrEmpty(previousTimeTrackClockOutTimeRecord) &&
                                 updatedClockOutTime.CompareTo(
                                     DateTime.Parse(previousTimeTrackClockOutTimeRecord).TimeOfDay) == -1) ||
                                (!string.IsNullOrEmpty(nextTimeTrackClockInTimeRecord) &&
                                 updatedClockOutTime.CompareTo(
                                     DateTime.Parse(nextTimeTrackClockInTimeRecord).TimeOfDay) == 1))
                            {
                                errorMessage.Append(
                                    "Updated Clock Out time can not be earlier than previous clock out time or later than earlier clock in time");
                                errorCount += 1;
                            }

                        }
                        else if (
                            !updatedClockInTime.Equals(DateTime.Parse(userTimeTrackRecord.ClockInTime).TimeOfDay) &&
                            (!updatedClockOutTime.Equals(
                                DateTime.Parse(userTimeTrackRecord.ClockOutTime).TimeOfDay)))
                        {
                            // Both clock in and out time has been changed
                            if (
                                updatedClockInTime.CompareTo(
                                    DateTime.Parse(previousTimeTrackClockOutTimeRecord).TimeOfDay) == -1 ||
                                updatedClockInTime.CompareTo(
                                    DateTime.Parse(nextTimeTrackClockInTimeRecord).TimeOfDay) == 1)
                            {
                                errorMessage.Append(
                                    "Updated Clock Out time can not be earlier than previous clock out time or later than earlier clock in time");
                                errorCount += 1;
                            }
                            if (
                                updatedClockOutTime.CompareTo(
                                    DateTime.Parse(previousTimeTrackClockOutTimeRecord).TimeOfDay) == -1 ||
                                updatedClockOutTime.CompareTo(
                                    DateTime.Parse(nextTimeTrackClockInTimeRecord).TimeOfDay) == 1)
                            {
                                errorMessage.Append(
                                    "Updated Clock Out time can not be earlier than previous clock out time or later than earlier clock in time");
                                errorCount += 1;
                            }
                        }

                        if (errorCount == 0)
                        {
                            userTimeTrackRecord.ClockInTime = String.Format("{0:t}", sDate.Add(updatedClockInTime));
                            userTimeTrackRecord.ClockOutTime = String.Format("{0:t}", sDate.Add(updatedClockOutTime));
                            userTimeTrackRecord.UpdatedBy = updatedBy;
                            userTimeTrackRecord.UpdatedDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]);
                            dbContext.SaveChanges();
                        }
                        return new TimeTrackErrorViewModel(GetTimeTrackFromUserTimeTrackHistory(dbContext.UserTimeTrackHistories.FirstOrDefault(utsh => utsh.TimeTrackId == timeTrackId)), errorMessage.ToString());
                    }
                    return new TimeTrackErrorViewModel(GetTimeTrackFromUserTimeTrackHistory(dbContext.UserTimeTrackHistories.FirstOrDefault(utsh => utsh.TimeTrackId == timeTrackId)), "Please provide a valid Clock In/Clock Out time.");
                }
                return new TimeTrackErrorViewModel(GetTimeTrackFromUserTimeTrackHistory(dbContext.UserTimeTrackHistories.FirstOrDefault(utsh => utsh.TimeTrackId == timeTrackId)), string.Empty);
            }

        }

        public static TimeTrack GetTimeTrackFromUserTimeTrackHistory(UserTimeTrackHistory userTimeTrackHistory)
        {
            if (userTimeTrackHistory != null)
            {
                return new TimeTrack(userTimeTrackHistory.TimeTrackId, userTimeTrackHistory.StampDate,
                                       userTimeTrackHistory.ClockInTime,
                                       userTimeTrackHistory.ClockOutTime);
            }
            return new TimeTrack();


        }

        public static void SetDefaultClockoutTimeStampToForgottenStamps(string userName)
        {
            var startDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]).Date;

            using (var dbContext = new TimeTrackingEntities())
            {
                var userForgottenTimeOutStamps = (from utsh in dbContext.UserTimeTrackHistories
                                                  where utsh.IsDeleted == false &&
                                                      utsh.UserName.ToLower().Equals(userName.ToLower()) &&
                                                      utsh.StampDate < startDate &&
                                                      (utsh.ClockInTime.Length > 0 && (utsh.ClockOutTime == null || utsh.ClockOutTime.Length == 0))
                                                  select utsh).ToList();
                if (userForgottenTimeOutStamps.Any())
                {
                    var sDate = new DateTime();
                    foreach (var userForgottenTimeOutStamp in userForgottenTimeOutStamps)
                    {
                        var timeSpan = TimeSpan.Parse(WebConfigurationManager.AppSettings["AutomaticTimeOutLimit"]);
                        userForgottenTimeOutStamp.ClockOutTime = String.Format("{0:t}", sDate.Add(timeSpan));
                        userForgottenTimeOutStamp.UpdatedBy = "sys";
                        userForgottenTimeOutStamp.UpdatedDate = WebHelpers.GetCurrentDateTimeByTimeZoneId(WebConfigurationManager.AppSettings["UserTimeZoneId"]);
                    }
                }
                dbContext.SaveChanges();
            }
        }

        public static bool DeleteTimeTrackRecord(int timeTrackId, string loggedInUserName)
        {
            using (var dbContext = new TimeTrackingEntities())
            {
                var userTimeTrackRecord =
                    dbContext.UserTimeTrackHistories.FirstOrDefault(utsh => utsh.TimeTrackId == timeTrackId);
                
                if (userTimeTrackRecord != null && userTimeTrackRecord.TimeTrackId > 0)
                {
                    dbContext.UserTimeTrackHistories.Remove(userTimeTrackRecord);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
