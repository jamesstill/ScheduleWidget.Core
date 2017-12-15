using System;

namespace ScheduleWidget.Common
{
    /// <summary>
    /// Describes a simple date range from start to end. If the two dates
    /// are identical then this describes a one-time (one day) occurrence.
    /// </summary>
    public class DateRange
    {
        public DateRange(DateTime startDateTime, DateTime endDateTime)
        {
            if (endDateTime < startDateTime)
            {
                throw new ArgumentException("End date cannot be less than the start date.");
            }

            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }

        public bool IsOneTime
        {
            get
            {
                var ts = (EndDateTime - StartDateTime);
                return (ts.Days == 0);
            }
        }
    }
}
