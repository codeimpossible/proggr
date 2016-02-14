using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Tests
{
    /// <summary>
    /// Timespan extension methods
    /// </summary>
    public static class DateAndTimeExtensions
    {
        /// <summary>
        /// Generates a TimeSpan representing a set number of Minutes
        /// </summary>
        /// <param name="i">the number of minutes to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Minutes(this int i)
        {
            return new TimeSpan(0, i, 0);
        }

        /// <summary>
        /// Generates a TimeSpan representing a set number of Hours
        /// </summary>
        /// <param name="i">the number of hours to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Hours(this int i)
        {
            return new TimeSpan(i, 0, 0);
        }

        /// <summary>
        /// Generates a TimeSpan representing a set number of Seconds
        /// </summary>
        /// <param name="i">the number of seconds to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Seconds(this int i)
        {
            return new TimeSpan(0, 0, i);
        }

        /// <summary>
        /// Generates a TimeSpan representing a set number of Days
        /// </summary>
        /// <param name="i">the number of days to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Days(this int i)
        {
            return new TimeSpan(i, 0, 0, 0);
        }

        /// <summary>
        /// Determines a date of time from a timespan
        /// </summary>
        /// <param name="val">the target timespan</param>
        /// <example>
        /// TimeSpan.FromHours(2.0).Ago(); //two hours in the past
        /// </example>
        /// <returns>a datetime object</returns>
        public static DateTime Ago(this TimeSpan val)
        {
            return DateTime.Now.Subtract(val);
        }

        /// <summary>
        /// Determines a date of time from a timespan
        /// </summary>
        /// <param name="val">the target timespan</param>
        /// <example>
        /// TimeSpan.FromHours(2.0).FromNow(); //two hours in the future
        /// </example>
        /// <returns>a datetime object</returns>
        public static DateTime FromNow(this TimeSpan val)
        {
            return DateTime.Now.Add(val);
        }
    }
}
