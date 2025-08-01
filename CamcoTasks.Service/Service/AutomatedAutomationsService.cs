using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.AutomatedAutomationsDTO;
using CamcoTasks.Infrastructure;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service
{
    public class AutomatedAutomationsService : IAutomatedAutomationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AutomatedAutomationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AutomatedAutomationsViewModel> GetById(int id)
        {
            return AutomatedAutomationsDTONew.Map(await _unitOfWork.Automations.GetAsync(id));
        }

        public async Task<AutomatedAutomationsViewModel> GetAutomationAsync(string automationType)
        {
            return AutomatedAutomationsDTONew.Map(await _unitOfWork.Automations.FindAsync(a => a.Type == automationType));
        }

        public async Task UpdateAsync(AutomatedAutomationsViewModel entity)
        {
            var autoamtedAutomation = await _unitOfWork.Automations.GetAsync(entity.ID);

            autoamtedAutomation.Id = entity.ID;
            autoamtedAutomation.Type = entity.AutomationType;
            autoamtedAutomation.Name = entity.Name;
            autoamtedAutomation.TimerTicks = entity.TimerTick;
            autoamtedAutomation.EarliestTimeToRun = entity.EarliestTime;
            autoamtedAutomation.LatestTimeToRun = entity.LatestTime;
            autoamtedAutomation.LastRunDate = entity.LastRun;
            autoamtedAutomation.IsEnabled = entity.IsEnabled;
            autoamtedAutomation.WeekdayToRun = entity.WeekdayToRun;
            autoamtedAutomation.IsRestartNeeded = entity.NeedsRestart;
            autoamtedAutomation.IsMondaySelected = entity.Monday;
            autoamtedAutomation.IsTuesdaySelected = entity.Tuesday;
            autoamtedAutomation.IsWednesdaySelected = entity.Wednesday;
            autoamtedAutomation.IsThursdaySelected = entity.Thursday;
            autoamtedAutomation.IsFridaySelected = entity.Friday;
            autoamtedAutomation.IsSaturdaySelected = entity.Saturday;
            autoamtedAutomation.IsSundaySelected = entity.Sunday;
            autoamtedAutomation.IsRunningNow = entity.IsRunningNow;
            await _unitOfWork.CompleteAsync();

            //await _unitOfWork.Automations.UpdateAsync(AutomatedAutomationsDTONew.Map(entity));
        }
    }
}
