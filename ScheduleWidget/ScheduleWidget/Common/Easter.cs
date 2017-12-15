using System;
using System.Collections.Generic;

namespace ScheduleWidget.Common
{
    /// <summary>
    /// Calculates Easter for the Western and Orthodox Christian traditions. This class uses code 
    /// written by an anonymous blogger published here:
    /// https://mycodepad.wordpress.com/2013/04/28/c-calculating-orthodox-and-catholic-easter/
    /// </summary>
    public static class Easter
    {
        /// <summary>
        /// Return the date for Easter Sunday on the given year for the calendar.
        /// </summary>
        /// <param name="year">Calendar year</param>
        /// <param name="calendar">Orthodox or Western calendar</param>
        /// <returns>DateTime</returns>
        public static DateTime GetEasterSunday(int year, EasterCalendar calendar = EasterCalendar.Western)
        {
            switch (calendar)
            {
                case EasterCalendar.Orthodox:
                    return GetOrthodoxEaster(year);

                case EasterCalendar.Western:
                    return GetWesternEaster(year);

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Returns the date range for all Easter Sundays between the start 
        /// and end years for the calendar.
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetEasterSundays(int startYear, int endYear, EasterCalendar calendar = EasterCalendar.Western)
        {
            if (startYear > endYear)
            {
                throw new ArgumentException("endYear must be later than startYear.");
            }

            var list = new List<DateTime>();
            for (var i = startYear; i <= endYear; i++)
            {
                var aDate = GetEasterSunday(i, calendar);
                list.Add(aDate);
            }

            return list;
        }

        /// <summary>
        /// Get Western Easter date for the specified year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static DateTime GetWesternEaster(int year)
        {
            var month = 3;
            var g = year % 19 + 1;
            var c = year / 100 + 1;
            var x = (3 * c) / 4 - 12;
            var y = (8 * c + 5) / 25 - 5;
            var z = (5 * year) / 4 - x - 10;
            var e = (11 * g + 20 + y - x) % 30;
            if (e == 24) { e++; }
            if ((e == 25) && (g > 11)) { e++; }
            var n = 44 - e;
            if (n < 21) { n = n + 30; }
            var p = (n + 7) - ((z + n) % 7);
            if (p > 31)
            {
                p = p - 31;
                month = 4;
            }
            return new DateTime(year, month, p);
        }

        /// <summary>
        /// Get Orthodox easter for the specified year
        /// </summary>
        /// <param name="year">Year of easter</param>
        /// <returns>DateTime of Orthodox Easter</returns>
        private static DateTime GetOrthodoxEaster(int year)
        {
            var a = year % 19;
            var b = year % 7;
            var c = year % 4;

            var d = (19 * a + 16) % 30;
            var e = (2 * c + 4 * b + 6 * d) % 7;
            var f = (19 * a + 16) % 30;
            var key = f + e + 3;

            var month = (key > 30) ? 5 : 4;
            var day = (key > 30) ? key - 30 : key;

            return new DateTime(year, month, day);
        }
    }
}
