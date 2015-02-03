using System;
using System.Web;

namespace HBS.Data.Entities.TimeTracking.Infrastructure
{
    public static class WebHelpers
    {
        public static string GetIpAddress()
        {
            string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            return !string.IsNullOrEmpty(ipList) ? ipList.Split(',')[0] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static DateTime GetCurrentDateTimeByTimeZoneId(string timeZoneId)
        {
            return DateTime.Now.ConvertToGivenTimeZoneDateTime(timeZoneId);
        }
        public static DateTime GetCurrentDateTimeByTimeZoneId(int numberOfDaysToAdd, string timeZoneId)
        {
            return DateTime.Now.AddDays(numberOfDaysToAdd).ConvertToGivenTimeZoneDateTime(timeZoneId);
        }
    }
}