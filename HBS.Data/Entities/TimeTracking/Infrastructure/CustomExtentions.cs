using System;
using System.Collections.Generic;

namespace HBS.Data.Entities.TimeTracking.Infrastructure
{
    public static class DictionaryExtensions
    {
        public static TKey FindKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                if (value.Equals(pair.Value)) return pair.Key;

            throw new Exception("the value is not found in the dictionary");
        }

        public static DateTime ConvertToGivenTimeZoneDateTime(this DateTime sourceDateTime,string destinationTimeZoneId)
        {
           return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(sourceDateTime, TimeZoneInfo.Local.Id, destinationTimeZoneId);
        }
    }
}
