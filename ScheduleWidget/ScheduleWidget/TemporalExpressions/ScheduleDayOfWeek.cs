using ScheduleWidget.Common;
using ScheduleWidget.TemporalExpressions.Base;
using System;

namespace ScheduleWidget.TemporalExpressions
{
    /// <summary>
    /// Compares two specific days of week exactly
    /// </summary>
    public class ScheduleDayOfWeek : TemporalExpression
    {
        private readonly DayInterval _day;

        /// <summary>
        /// The day of week value
        /// </summary>
        /// <param name="aDay"></param>
        public ScheduleDayOfWeek(DayInterval aDay)
        {
            _day = aDay;
        }

        /// <summary>
        /// Returns true if the date day of week matches the flag
        /// attribute value:
        /// 
        ///     Sun = 1,
        ///     Mon = 2,
        ///     Tue = 4,
        ///     Wed = 8,
        ///     Thu = 16,
        ///     Fri = 32,
        ///     Sat = 64
        /// 
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            switch (aDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return ((int)_day == 1);

                case DayOfWeek.Monday:
                    return ((int)_day == 2);

                case DayOfWeek.Tuesday:
                    return ((int)_day == 4);

                case DayOfWeek.Wednesday:
                    return ((int)_day == 8);

                case DayOfWeek.Thursday:
                    return ((int)_day == 16);

                case DayOfWeek.Friday:
                    return ((int)_day == 32);

                case DayOfWeek.Saturday:
                    return ((int)_day == 64);

                default:
                    return false;
            }
        }
    }
}
