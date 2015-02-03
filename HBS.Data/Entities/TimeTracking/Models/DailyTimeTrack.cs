using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class DailyTimeTrack
    {
        public DateTime StampDate { get; set; }
        public List<TimeTrack> TimeTrackList { get; set; }
        public string SubmitButtonText { get; set; }
        public TimeSpan TotalTimeForTheDay
        {
            get
            {
                var totalTimeForTheDay = new TimeSpan();
                return TimeTrackList.Aggregate(totalTimeForTheDay, (current, ttl) => ttl.TotalTime != null ? current.Add(ttl.TotalTime.Value) : new TimeSpan());
            }
        }
        public string TotalTimeForTheDayDisplay
        {
            get
            {
                //return string.Format("{0:%h} hours {0:%m} minutes", TotalTimeForTheDay);
                return string.Format("{0} hours {1} minutes", Math.Floor(TotalTimeForTheDay.TotalHours), (TotalTimeForTheDay.TotalMinutes % 60));
            }
        }
        public string StampDateForDisplay
        {
            get { return StampDate.DayOfWeek.ToString() + "<br />" + string.Format("{0:d}", StampDate); }
        }

        public double EmployeeHourlyRate { get; set; }
        public double EmployeePayForThePeriod
        {
            get { return EmployeeHourlyRate * TotalTimeForTheDay.TotalHours; }
        }
        public string EmployeePayForThePeriodDisplay
        {
            get { return string.Format("{0:#,#.##}", EmployeePayForThePeriod); }
        }
        public DailyTimeTrack()
        {
            EmployeeHourlyRate = 0d;
            TimeTrackList = new List<TimeTrack>();
            SubmitButtonText = WebConfigurationManager.AppSettings["ClockInText"];
            StampDate = DateTime.Now.Date;
        }
        public DailyTimeTrack(DateTime stampDate, double hourlyRate)
            : this()
        {
            StampDate = stampDate;
            EmployeeHourlyRate = hourlyRate;
        }

    }
}
