using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class WeekManager
    {
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public string WeekStartEndDateDisplay
        {
            get
            {

                if (WeekEndDate.Year > WeekStartDate.Year)
                    return string.Format("{0:m}", WeekStartDate) + " " + WeekStartDate.Year + "-" +
                           string.Format("{0:m}", WeekEndDate) + " " + WeekEndDate.Year;

                return string.Format("{0:m}", WeekStartDate) + "-" + string.Format("{0:m}", WeekEndDate);


            }
        }
        public int WeekNumber
        {
            get
            {
                var ciCurr = CultureInfo.CurrentCulture;
                var weekNum = ciCurr.Calendar.GetWeekOfYear(WeekStartDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return weekNum;
            }
        }


        public WeekManager()
        {
        }
        public WeekManager(DateTime givenDate)
        {
            while (givenDate.DayOfWeek != DayOfWeek.Monday)
            {
                givenDate = givenDate.AddDays(-1);
            }
            WeekStartDate = givenDate.Date;
            WeekEndDate = givenDate.AddDays(6).Date;
        }
        public static List<WeekManager> GetWeekList(DateTime givenDate, int numberOfPriorWeeks, bool includeThisWeek)
        {
            var weekList = new List<WeekManager>();

            if (includeThisWeek)
            {
                while (givenDate.DayOfWeek != DayOfWeek.Monday)
                {
                    givenDate = givenDate.AddDays(-1);
                }

                weekList.Add(new WeekManager
                    {
                        WeekStartDate = givenDate.Date,
                        WeekEndDate = givenDate.AddDays(6).Date
                    });
            }

            if (numberOfPriorWeeks != 0)
            {
                givenDate = givenDate.AddDays(-(numberOfPriorWeeks * 7));

                if (!includeThisWeek)
                {
                    while (givenDate.DayOfWeek != DayOfWeek.Monday)
                    {
                        givenDate = givenDate.AddDays(-1);
                    }
                }

                for (int i = 0; i < numberOfPriorWeeks; i++)
                {
                    weekList.Add(new WeekManager
                    {
                        WeekStartDate = givenDate.Date.AddDays(i * 7),
                        WeekEndDate = givenDate.AddDays((i * 7) + 6).Date
                    });
                }
            }
            return weekList.OrderByDescending(c => c.WeekNumber).ToList();
        }
    }
}