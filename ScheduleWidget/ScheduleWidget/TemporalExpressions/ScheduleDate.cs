using System;
using System.Globalization;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.TemporalExpressions
{
    /// <summary>
    /// Compares two dates for an exact match
    /// </summary>
    public class ScheduleDate : TemporalExpression
    {
        private DateTime _date;

        /// <summary>
        /// The date value
        /// </summary>
        /// <param name="aDate"></param>
        public ScheduleDate(DateTime aDate) 
        {
            _date = aDate;
        }

        /// <summary>
        /// Returns true if the date matches this date value
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            return (_date.Date == aDate.Date);
        }
    }
}
