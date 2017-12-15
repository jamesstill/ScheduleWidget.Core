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

                case FrequencyType.Monthly:
                    frequencyBuilder = new MonthlyEventFrequencyBuilder();
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
