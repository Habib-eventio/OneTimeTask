using CamcoTasks.Infrastructure.Entities.Automated;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.AutomatedAutomationsDTO
{
    public class AutomatedAutomationsDTONew
    {
        public static Automation Map(AutomatedAutomationsViewModel viewModel)
        {
            if (viewModel == null) return null;

            return new Automation
            {
                Id = viewModel.ID,
                Type = viewModel.AutomationType,
                Name = viewModel.Name,
                TimerTicks = viewModel.TimerTick,
                EarliestTimeToRun = viewModel.EarliestTime,
                LatestTimeToRun = viewModel.LatestTime,
                LastRunDate = viewModel.LastRun,
                IsEnabled = viewModel.IsEnabled,
                WeekdayToRun = viewModel.WeekdayToRun,
                IsRestartNeeded = viewModel.NeedsRestart,
                IsMondaySelected = viewModel.Monday,
                IsTuesdaySelected = viewModel.Tuesday,
                IsWednesdaySelected = viewModel.Wednesday,
                IsThursdaySelected = viewModel.Thursday,
                IsFridaySelected = viewModel.Friday,
                IsSaturdaySelected = viewModel.Saturday,
                IsSundaySelected = viewModel.Sunday,
                IsRunningNow = viewModel.IsRunningNow,
            };
        }

        public static AutomatedAutomationsViewModel Map(Automation entity)
        {
            if (entity == null) return null;

            return new AutomatedAutomationsViewModel
            {
                ID = entity.Id,
                AutomationType = entity.Type,
                Name = entity.Name,
                TimerTick = entity.TimerTicks,
                EarliestTime = entity.EarliestTimeToRun,
                LatestTime = entity.LatestTimeToRun,
                LastRun = entity.LastRunDate,
                IsEnabled = entity.IsEnabled,
                WeekdayToRun = entity.WeekdayToRun,
                NeedsRestart = entity.IsRestartNeeded,
                Monday = entity.IsMondaySelected,
                Tuesday = entity.IsTuesdaySelected,
                Wednesday = entity.IsWednesdaySelected,
                Thursday = entity.IsThursdaySelected,
                Friday = entity.IsFridaySelected,
                Saturday = entity.IsSaturdaySelected,
                Sunday = entity.IsSundaySelected,
                IsRunningNow = entity.IsRunningNow,
            };
        }

        public static IEnumerable<AutomatedAutomationsViewModel> Map(IEnumerable<Automation> dataEntityList)
        {
            if (dataEntityList == null) yield break;

            foreach (Automation item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
