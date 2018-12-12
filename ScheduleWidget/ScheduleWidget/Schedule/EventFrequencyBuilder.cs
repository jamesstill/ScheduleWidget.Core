using System;
using ScheduleWidget.Common;
using ScheduleWidget.Schedule.ConcreteFrequencyBuilders;

namespace ScheduleWidget.Schedule
{
    public static class EventFrequencyBuilder
    {
        public static IEventFrequencyBuilder Create(FrequencyType type)
        {
            IEventFrequencyBuilder frequencyBuilder;
            
            switch (type)
            {
                case FrequencyType.Daily:
                    frequencyBuilder = new DailyEventFrequencyBuilder();
                    break;

                case FrequencyType.Weekly:
                    frequencyBuilder = new WeeklyEventFrequencyBuilder();
                    break;

                case FrequencyType.DayOfMonth:
                    frequencyBuilder = new DayOfMonthEventFrequencyBuilder();
                    break;

                case FrequencyType.MonthlyByDayOfMonth:
                    frequencyBuilder = new MonthlyDayOfMonthEventFrequencyBuilder();
                    break;

                case FrequencyType.MonthlyByDayInMonth:
                    frequencyBuilder = new MonthlyDayInMonthEventFrequencyBuilder();
                    break;

                case FrequencyType.MonthlyByDayInWeekOfMonth:
                    frequencyBuilder = new MonthlyDayInWeekOfMonthEventFrequencyBuilder();
                    break;

                case FrequencyType.Quarterly:
                    frequencyBuilder = new QuarterlyEventFrequencyBuilder();
                    break;

                case FrequencyType.Yearly:
                    frequencyBuilder = new AnnualEventFrequencyBuilder();
                    break;

                case FrequencyType.None:
                    frequencyBuilder = new NullEventFrequencyBuilder();
                    break;

                default:
                    throw new Exception("Unknown frequency type passed into frequency builder!");
            }

            return frequencyBuilder;
        }
    }
}
