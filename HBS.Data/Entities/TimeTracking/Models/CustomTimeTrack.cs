using System;
using System.Collections.Generic;
using System.Linq;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class CustomTimeTrack
    {
        public CustomTimeTrack()
        {
            DailyTimeTracks = new List<DailyTimeTrack>();
        }

        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public DateTime? CustomStartDate { get; set; }
        public DateTime? CustomEndDate { get; set; }
        public string CustomStartEndDateDisplay
        {
            get
            {
                if (CustomStartDate.HasValue && CustomEndDate.HasValue)
                {
                    if (CustomEndDate.Value.Year > CustomStartDate.Value.Year)
                        return string.Format("{0:m}", CustomStartDate.Value) + " " + CustomStartDate.Value.Year + "-" +
                               string.Format("{0:m}", CustomEndDate) + " " + CustomEndDate.Value.Year;

                    return string.Format("{0:m}", CustomStartDate.Value) + "-" + string.Format("{0:m}", CustomEndDate.Value);
                }
                return string.Empty;
            }
        }
        public List<DailyTimeTrack> DailyTimeTracks { get; set; }
        public TimeSpan TotalTimeWorkedForSelectedPeriod
        {
            get
            {
                var totalTimeForSelectedPeriod = new TimeSpan();
                return DailyTimeTracks != null ? DailyTimeTracks.Aggregate(totalTimeForSelectedPeriod, (current, ttfd) => current.Add(ttfd.TotalTimeForTheDay)) : totalTimeForSelectedPeriod;
            }
        }
        public string TotalTimeWorkedForSelectedPeriodDisplay
        {
            get
            {
                //return string.Format("{0:%h} hours {0:%m} minutes", TotalTimeWorkedForSelectedPeriod);
                return string.Format("{0} hours {1} minutes", Math.Floor(TotalTimeWorkedForSelectedPeriod.TotalHours), (TotalTimeWorkedForSelectedPeriod.TotalMinutes % 60));
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
                                                       (current, dailyPayment) => (current += dailyPayment.EmployeePayForThePeriod))
                           : totalEmployeePayForThePeriod;
            }
        }
        public string EmployeePayForThePeriodDisplay
        {
            get { return string.Format("{0:#,#.##}", EmployeePayForThePeriod); }
        }
    }
}