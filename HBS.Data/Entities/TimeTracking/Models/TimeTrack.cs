using System;
using System.Web.Configuration;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class TimeTrack
    {
        public int TimeTrackId { get; set; }
        public string ClockInTimeDisplay { get; set; }
        public TimeSpan? ClockInTime { get; set; }
        public string ClockInTimeForJs
        {
            get
            {
                return ClockInTime.HasValue ? ClockInTime.Value.ToString() : string.Empty;
            }
        }
        public string ClockOutTimeDisplay { get; set; }
        public TimeSpan? ClockOutTime { get; set; }
        public string ClockOutTimeForJs
        {
            get
            {
                return ClockOutTime.HasValue ? ClockOutTime.Value.ToString() : string.Empty;
            }
        }
        public string TotalTimeDisplay { get; set; }
        public TimeSpan? TotalTime { get; set; }

        public TimeTrack()
        {
        }
        public TimeTrack(int timeTrackId, DateTime stampDate, string clockInTime, string clockOutTime)
        {
            TimeTrackId = timeTrackId;
            var clockInDateTime = Convert.ToDateTime(clockInTime);
            ClockInTimeDisplay = string.Format("{0:t}", clockInDateTime);
            ClockInTime = clockInDateTime.TimeOfDay;

            if (!string.IsNullOrEmpty(clockOutTime))
            {
                var clockOutDateTime = Convert.ToDateTime(clockOutTime);
                ClockOutTimeDisplay = string.Format("{0:t}", clockOutDateTime);
                ClockOutTime = clockOutDateTime.TimeOfDay;

                TotalTime = ClockOutTime.Value.Subtract(ClockInTime.Value);
                TotalTimeDisplay = string.Format("{0:%h} hours {0:%m} minutes", TotalTime);
                //TotalTimeDisplay = string.Format("{0} hours {1} minutes", Math.Floor(TotalTime.TotalHours), (TotalTime.TotalMinutes % 60));
            }
            else
            {
                //Earlier = -1,
                //Later = 1,
                //TheSame = 0
                if (stampDate.Date.CompareTo(DateTime.Now.Date) == -1)
                {
                    var clockOutDateTime = Convert.ToDateTime(WebConfigurationManager.AppSettings["AutomaticTimeOutLimit"]);
                    ClockOutTimeDisplay = string.Format("{0:t}", clockOutDateTime);
                    ClockOutTime = clockOutDateTime.TimeOfDay;

                    TotalTime = ClockOutTime.Value.Subtract(ClockInTime.Value);
                    TotalTimeDisplay = string.Format("{0:%h} hours {0:%m} minutes", TotalTime);
                    //TotalTimeDisplay = string.Format("{0} hours {1} minutes", Math.Floor(TotalTime.TotalHours), (TotalTime.TotalMinutes % 60));
                }
            }
        }

    }
}