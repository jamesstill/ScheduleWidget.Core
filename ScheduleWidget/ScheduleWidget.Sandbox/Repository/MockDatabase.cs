using System.Collections.Generic;
using ScheduleWidget.Sandbox.Models;

namespace ScheduleWidget.Sandbox.Repository
{
    public static class MockDatabase
    {
        public static int UniqueID = 1;
        public static List<ScheduleViewModel> Schedules = new List<ScheduleViewModel>();
    }
}
