using System;
using ScheduleWidget.TemporalExpressions.Base;

namespace ScheduleWidget.TemporalExpressions
{
    /// <summary>
    /// Compares a range of time within a year. For example, any date that
    /// falls between April and October:
    /// var nonWinterMonths = new ScheduleRangeEachYear(4, 10);
    /// </summary>
    public class ScheduleRangeInYear : TemporalExpression
    {
        private readonly int _startMonth;
        private readonly int _endMonth;
        private readonly int _startDay;
        private readonly int _endDay;

        public ScheduleRangeInYear(int startMonth, int endMonth, int startDay, int endDay)
        {
            _startMonth = startMonth;
            _endMonth = endMonth;
            _startDay = startDay;
            _endDay = endDay;
        }

        public ScheduleRangeInYear(int startMonth, int endMonth)
        {
            _startMonth = startMonth;
            _endMonth = endMonth;
            _startDay = 0;
            _endDay = 0;
        }

        /// <summary>
        /// Returns true if the date is included in the expression
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            return MonthsInclude(aDate) || StartMonthIncludes(aDate) || EndMonthIncludes(aDate);
        }

        private bool MonthsInclude(DateTime aDate)
        {
            return (aDate.Month > _startMonth && aDate.Month < _endMonth);
        }

        private bool StartMonthIncludes(DateTime aDate)
        {
            if (aDate.Month != _startMonth)
                return false;

            if (_startDay == 0)
                return true;

            return (aDate.Day >= _startDay);
        }

        private bool EndMonthIncludes(DateTime aDate)
        {
            if (aDate.Month != _endMonth)
                return false;

            if (_endDay == 0)
                return true;

            return (aDate.Day <= _endDay);
        }
    }
}
