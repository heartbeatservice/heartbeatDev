using System;
using System.Collections.Generic;
using System.Linq;
using HBS.Data.Entities.TimeTracking.Models;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class WeeklyTimeTrack
    {
        public WeeklyTimeTrack()
        {
            DailyTimeTracks = new List<DailyTimeTrack>();
        }
        public int WeekNumber { get; set; }
        public DateTime? WeekStartDate { get; set; }
        public List<WeekManager> WeekList { get; set; }
        public DateTime? WeekEndDate { get; set; }
        public string WeekStartEndDateDisplay
        {
            get
            {
                if (WeekStartDate.HasValue && WeekEndDate.HasValue)
                {
                    if (WeekEndDate.Value.Year > WeekStartDate.Value.Year)
                        return string.Format("{0:m}", WeekStartDate.Value) + " " + WeekStartDate.Value.Year + "-" +
                               string.Format("{0:m}", WeekEndDate.Value) + " " + WeekEndDate.Value.Year;

                    return string.Format("{0:m}", WeekStartDate.Value) + "-" + string.Format("{0:m}", WeekEndDate.Value);
                }
                return string.Empty;
            }
        }
        public List<DailyTimeTrack> DailyTimeTracks { get; set; }

        public TimeSpan TotalTimeForTheWeek
        {
            get
            {
                var totalTimeForTheWeek = new TimeSpan();
                return DailyTimeTracks != null ? DailyTimeTracks.Aggregate(totalTimeForTheWeek, (current, ttfd) => current.Add(ttfd.TotalTimeForTheDay)) : totalTimeForTheWeek;
            }
        }
        public string TotalTimeForTheWeekDisplay
        {
            get
            {
                //return string.Format("{0:%h} hours {0:%m} minutes", TotalTimeForTheWeek);
                return string.Format("{0} hours {1} minutes", Math.Floor(TotalTimeForTheWeek.TotalHours), (TotalTimeForTheWeek.TotalMinutes % 60));
            }
        }
        
        public double EmployeeHourlyRate { get; set; }
        public double EmployeePayForThePeriod
        {
            get
            {
                const double totalEmployeePayForThePeriod = new double();
                return DailyTimeTracks != null
                           ? DailyTimeTracks.Aggregate(totalEmployeePayForThePeriod,
                                                       (current, ttfd) => (current += ttfd.EmployeePayForThePeriod))
                           : totalEmployeePayForThePeriod;
            }
        }
        public string EmployeePayForThePeriodDisplay
        {
            get { return string.Format("{0:#,#.##}", EmployeePayForThePeriod); }
        }
    }
}