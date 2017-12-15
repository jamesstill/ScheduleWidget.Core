using System.Collections.Generic;
using System.Linq;
using ScheduleWidget.Sandbox.Models;

namespace ScheduleWidget.Sandbox.Repository
{
    public class ScheduleRepository
    {
        public ScheduleViewModel GetSchedule(int id)
        {
            return MockDatabase.Schedules.FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<ScheduleViewModel> GetAllSchedules(string userId)
        {
            return MockDatabase.Schedules.Where(s => s.UserId == userId);
        }

        public bool Save(ScheduleViewModel scheduleViewModel)
        {
            if (scheduleViewModel.ID == 0)
            {
                scheduleViewModel.ID = MockDatabase.UniqueID;
                MockDatabase.UniqueID++;
                MockDatabase.Schedules.Add(scheduleViewModel);
            }
            else
            {
                var schedule = MockDatabase.Schedules.FirstOrDefault(s => s.ID == scheduleViewModel.ID);

                if (schedule == null)
                    return false;

                schedule.Title = scheduleViewModel.Title;
                schedule.Frequency = scheduleViewModel.Frequency;
                schedule.Days = scheduleViewModel.Days;
                schedule.Weeks = scheduleViewModel.Weeks;
                schedule.StartDate = scheduleViewModel.StartDate;
                schedule.EndDate = scheduleViewModel.EndDate;
                schedule.StartTime = scheduleViewModel.StartTime;
                schedule.EndTime = scheduleViewModel.EndTime;
            }

            return true;
        }
    }
}
