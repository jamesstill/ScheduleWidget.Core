using System;
using System.Diagnostics;
using System.Globalization;

namespace ScheduleWidget.Common
{
    /// <summary>
    /// Extensions methods for DateTime that help with date arithmetic.
    /// </summary>
    internal static partial class DateExtensions
    {
        /// <summary>
        /// Returns the number of weeks (1 thru 5) in the month of the 
        /// date argument based on the current culture.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static int WeeksInMonth(this DateTime aDate)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            var totalNumberOfDaysInMonth = DateTime.DaysInMonth(aDate.Year, aDate.Month);
            var firstDayOfMonth = ToFirstDayOfMonth(aDate);
            while (firstDayOfMonth.DayOfWeek != firstDayOfWeek)
            {
                firstDayOfMonth = firstDayOfMonth.AddDays(1);
            }

            var firstDayOfMonthNumber = (int) firstDayOfMonth.DayOfWeek;
            return (int)Math.Ceiling((firstDayOfMonthNumber + totalNumberOfDaysInMonth) / 7f);
        }

        /// <summary>
        /// Returns the week number in the month of the date argument 
        /// (1 thru 5).
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static int ToWeekOfMonth(this DateTime aDate)
        {
            var firstDayOfMonth = ToFirstDayOfMonth(aDate);
            var value = aDate.ToWeekOfYear() - firstDayOfMonth.ToWeekOfYear() + 1;
            return value;
        }

        /// <summary>
        /// Returns the week number in the year of the date argument (typically 1 
        /// thru 52) based on the current culture.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static int ToWeekOfYear(this DateTime aDate)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            var calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            return cultureInfo.Calendar.GetWeekOfYear(aDate, calendarWeekRule, firstDayOfWeek);
        }

        /// <summary>
        /// Returns the first day of the month for the date argument. For example,
        /// if the date is 20 Jan 2040 then the method will return 1 Jan 2040.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static DateTime ToFirstDayOfMonth(this DateTime aDate)
        {
            return new DateTime(aDate.Year, aDate.Month, 1);
        }

        /// <summary>
        /// Returns the Nth occurrence of the date argument's DayOfWeek (e.g, Friday).
        /// For example, if the date is a Friday in Jan 2040 and n == 3 the method 
        /// will return the 3rd Friday in Jan 2040 or 20 Jan 2040.
        /// </summary>
        /// <param name="aDate">date arg with desired DayOfWeek (e.g., Friday)</param>
        /// <param name="n">occurrence number (e.g., 3 for 3rd Friday of month)</param>
        /// <returns>matching date</returns>
        internal static DateTime NthOccurrenceInMonth(this DateTime aDate, int n)
        {
            var firstDayOfMonth = ToFirstDayOfMonth(aDate);
            var delta = (int) aDate.DayOfWeek - (int) firstDayOfMonth.DayOfWeek;
            if (delta < 0)
            {
                delta = delta + 7;
            }

            var dayResult = (delta + 1) + (7 * (n - 1));

            return (dayResult > DateTime.DaysInMonth(aDate.Year, aDate.Month)) ? 
                LastOccurrenceInMonth(aDate) : 
                new DateTime(aDate.Year, aDate.Month, dayResult);
        }

        /// <summary>
        /// Returns the last occurrence of the date argument's DayOfWeek (e.g., Friday).
        /// For example, if the date is a Friday in Jan 2040 and we want the last Friday
        /// for that month the method will return 27 Jan 2040.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static DateTime LastOccurrenceInMonth(this DateTime aDate)
        {
            var lastDayOfMonth = aDate.ToLastDayOfMonth();
            var delta = (aDate.DayOfWeek - lastDayOfMonth.DayOfWeek);
            if (delta > 0)
            {
                delta -= 7;
            }

            Debug.Assert(delta <= 0);
            return lastDayOfMonth.AddDays(delta);
        }
        
        /// <summary>
        /// Returns the last day of the date argument's month. For example,
        /// if the date is 10 Jan 2040 then the method will return 31 Jan 2040.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static DateTime ToLastDayOfMonth(this DateTime aDate)
        {
            return aDate.ToFirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Returns the number of days left in the month from the date argument.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static int ToDaysLeftInMonth(this DateTime aDate)
        {
            var actualMaximum = DateTime.DaysInMonth(aDate.Year, aDate.Month);
            return actualMaximum - aDate.Day;
        }

        /// <summary>
        /// Returns a DayInterval enum constant matching the DayOfWeek of the
        /// date argument. For example, if the date is on a Friday the method
        /// will return DayInterval.Fri
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static DayInterval ToDayInterval(this DateTime aDate)
        {
            switch (aDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return DayInterval.Sun;

                case DayOfWeek.Monday:
                    return DayInterval.Mon;

                case DayOfWeek.Tuesday:
                    return DayInterval.Tue;

                case DayOfWeek.Wednesday:
                    return DayInterval.Wed;

                case DayOfWeek.Thursday:
                    return DayInterval.Thu;

                case DayOfWeek.Friday:
                    return DayInterval.Fri;

                case DayOfWeek.Saturday:
                    return DayInterval.Sat;

                default:
                    return DayInterval.None;
            }
        }

        /// <summary>
        /// Returns the Nth week number (1 thru 5) for the date argument and the
        /// WeekInterval argument. For example, if the date is 27 Jan 2040 and
        /// interval is WeekInterval.Last then the method returns 4 (4th week). 
        /// </summary>
        /// <param name="aDate"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        internal static int ToNthFromWeekInterval(this DateTime aDate, WeekInterval interval)
        {
            var numberOfWeeksInMonth = aDate.WeeksInMonth();
            switch (interval)
            {
                case WeekInterval.First:
                    return 1;

                case WeekInterval.Second:
                    return 2;

                case WeekInterval.Third:
                    return 3;

                case WeekInterval.Fourth:
                    return 4;

                case WeekInterval.Last:
                    return (numberOfWeeksInMonth > 4) ? 5 : 4;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Returns a WeekInterval enum constant for the date argument. For example,
        /// if the date is 20 Jan 2040 then the method will return WeekInterval.Third
        /// since 20 Jan 2040 is in the third week of the month.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static WeekInterval ToWeekInterval(this DateTime aDate)
        {
            var numberOfWeeksInMonth = aDate.WeeksInMonth();
            var weekNumber = aDate.ToWeekOfMonth();
            switch (weekNumber)
            {
                case 1:
                    return WeekInterval.First;

                case 2:
                    return WeekInterval.Second;

                case 3:
                    return WeekInterval.Third;

                case 4:
                    return (numberOfWeeksInMonth > 4) ? WeekInterval.Fourth : WeekInterval.Last;

                case 5:
                    return WeekInterval.Last;

                default:
                    return WeekInterval.None;
            }
        }

        /// <summary>
        /// Returns a QuarterInterval enum constant for the date argument. For example,
        /// if the date is 20 Jan 2040 then the method will return QuarterInterval.First
        /// since Jan is in the first quarter of the year.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static QuarterInterval ToQuarterInterval(this DateTime aDate)
        {
            switch (aDate.Month)
            {
                case 1:
                case 2:
                case 3:
                    return QuarterInterval.First;

                case 4:
                case 5:
                case 6:
                    return QuarterInterval.Second;

                case 7:
                case 8:
                case 9:
                    return QuarterInterval.Third;

                case 10:
                case 11:
                case 12:
                    return QuarterInterval.Fourth;

                default:
                    return QuarterInterval.None;
            }
        }

        /// <summary>
        /// Returns the MonthOfQuarterInterval enum constant for the date argument. For example,
        /// if the date is 20 Jan 2040 then the method will return MonthOfQuarterInterval.First
        /// since Jan is the first month in the first quarter. If the date were in Mar, Jun, Sep,
        /// or Dec then MonthOfQuarterInterval.Third would be returned since all four months are
        /// the third month within their quarters.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        internal static MonthOfQuarterInterval ToMonthOfQuarterInterval(this DateTime aDate)
        {
            switch (aDate.Month)
            {
                case 1:
                case 4:
                case 7:
                case 10:
                    return MonthOfQuarterInterval.First;

                case 2:
                case 5:
                case 8:
                case 11:
                    return MonthOfQuarterInterval.Second;

                case 3:
                case 6:
                case 9:
                case 12:
                    return MonthOfQuarterInterval.Third;

                default:
                    return MonthOfQuarterInterval.None;
            }
        }
    }
}
