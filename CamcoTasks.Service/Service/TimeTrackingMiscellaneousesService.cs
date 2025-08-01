//using System.Linq;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.TimeTrackingMiscellaneousDTO;
//using ERP.Repository.IRepository.TimeTracking;
//using CamcoTasks.Infrastructure;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class TimeTrackingMiscellaneousesService : ITimeTrackingMiscellaneousesService
//    {
//        private readonly IMiscellaneousRepository _timeTrackingMescellaneousesRepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public TimeTrackingMiscellaneousesService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<int> InsertAsync(TimeTrackingMiscellaneousViewModel entity)
//        {
//            var result = TimeTrackingMiscellaneousDtoNew.Map(entity);
//            await _unitOfWork.Miscellaneouss.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return result.Id;
//        }

//        public async Task<TimeTrackingMiscellaneousViewModel> GetLastEntityAsync()
//        {
//            var result = await _unitOfWork.Miscellaneouss.GetListAsync();
//            return TimeTrackingMiscellaneousDtoNew.Map(result.MaxBy(a=>a.Id));
//        }
//    }
//}
