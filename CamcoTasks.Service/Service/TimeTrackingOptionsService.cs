//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.TimeTrackingOptionsDTO;
//using ERP.Repository.IRepository.TimeTracking;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class TimeTrackingOptionsService : ITimeTrackingOptionsService
//    {
//        private readonly IOptionRepository _timeTrackingOptionsRepository;

//        public TimeTrackingOptionsService(IOptionRepository timeTrackingOptionsRepository)
//        {
//            _timeTrackingOptionsRepository = timeTrackingOptionsRepository;
//        }

//        public async Task<TimeTrackingOptionsViewModel> GetAsync(string appName, string optionName)
//        {
//            return TimeTrackingOptionsDtoNew.Map(await _timeTrackingOptionsRepository.FindAsync(x => x.AppName == appName && x.OptionName == optionName));
//        }
//    }
//}
