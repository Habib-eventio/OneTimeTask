//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.LoginLogs;
//using ERP.Repository.IRepository.HR;
//using System.Threading.Tasks;
//using CamcoTasks.Infrastructure;
//using ERP.Data.Entities.HR;

//namespace CamcoTasks.Service.Service
//{
//    public class LoginLogsService : ILoginLogsService
//    {
//        private readonly ILoginLogRepository _loginLogsRepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public LoginLogsService(ILoginLogRepository loginLogsRepository, IUnitOfWork unitOfWork)
//        {
//            _loginLogsRepository = loginLogsRepository;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task UpdateAsync(LoginLogsViewModel entity)
//        {
//            var loginLog = await _unitOfWork.LoginLogs.GetAsync(entity.Id);

//            loginLog.Id = entity.Id;
//            loginLog.EmployeeId = entity.EmployeeId;
//            loginLog.SignedInTime = entity.SignedInTime;
//            loginLog.SignedOutTime = entity.SignedInTime;
//            loginLog.TotalChanges = entity.TotalChanges;
//            loginLog.ApplicationId = entity.ApplicationId;
//            await _unitOfWork.CompleteAsync();

//            //await _loginLogsRepository.UpdateAsync(LoginLogsDto.Map(entity));
//        }

//        public async Task<LoginLogsViewModel> GetAsync(short appId, long employeeId)
//        {
//            return LoginLogsDto.Map(await _loginLogsRepository.FindAsync(x => x.ApplicationId == appId
//                        && x.EmployeeId == employeeId
//                        && x.SignedOutTime == null));
//        }
//    }
//}
