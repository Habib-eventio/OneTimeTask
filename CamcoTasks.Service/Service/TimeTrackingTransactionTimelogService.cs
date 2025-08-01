//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.TimeTrackingTransactionTimelogDTO;
//using ERP.Repository.IRepository.TimeTracking;
//using CamcoTasks.Infrastructure;
//using System;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class TimeTrackingTransactionTimelogService : ITimeTrackingTransactionTimelogService
//    {
//        private readonly ITransactionTimeLogRepository _timeTrackingTransctionTimeLoagRepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public TimeTrackingTransactionTimelogService(ITransactionTimeLogRepository timeTrackingTransactionTimelogRepository,
//            IUnitOfWork unitOfWork)
//        {
//            _timeTrackingTransctionTimeLoagRepository = timeTrackingTransactionTimelogRepository;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<int> InsertAsync(TimeTrackingTransactionTimelogViewModel entity)
//        {
//            var result = TimeTrackingTransactionTimelogDtoNew.Map(entity);
//            await _unitOfWork.TransactionTimelogs.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return result.Id;
//        }

//        public async Task UpdateAsync(TimeTrackingTransactionTimelogViewModel entity)
//        {
//            var timeTrackingTransactionTimelog = await _unitOfWork.TransactionTimelogs.GetAsync(entity.Id);
//            if (entity.TimeStart != null && entity.TimeEnd != null)
//            {
//                int TolSec = Convert.ToInt32((entity.TimeEnd.Value - entity.TimeStart.Value).TotalSeconds);
//                entity.Difference = TolSec;
//                entity.TimeTaken = TolSec;
//            }

//            timeTrackingTransactionTimelog.Id = entity.Id;
//            timeTrackingTransactionTimelog.GenericId = entity.GenericId;
//            timeTrackingTransactionTimelog.TransactionId = entity.TransactionId;
//            timeTrackingTransactionTimelog.CategoryId = entity.CategoryId;
//            timeTrackingTransactionTimelog.TimeStart = entity.TimeStart;
//            timeTrackingTransactionTimelog.TimeEnd = entity.TimeEnd;
//            timeTrackingTransactionTimelog.Difference = entity.Difference;
//            timeTrackingTransactionTimelog.TimeTaken = entity.TimeTaken;
//            timeTrackingTransactionTimelog.PageName = entity.PageName;
//            timeTrackingTransactionTimelog.IsPaused = entity.IsPaused;
//            timeTrackingTransactionTimelog.IsComplete = entity.IsComplete;
//            timeTrackingTransactionTimelog.TransactionBy = entity.TransactionBy;
//            timeTrackingTransactionTimelog.Description = entity.Description;
//            timeTrackingTransactionTimelog.Value = entity.Value;
//            timeTrackingTransactionTimelog.Description2 = entity.Description2;
//            timeTrackingTransactionTimelog.Value2 = entity.Value2;
//            timeTrackingTransactionTimelog.IsCancel = entity.IsCancel;
//            timeTrackingTransactionTimelog.IsFlagged = entity.IsFlagged;
//            timeTrackingTransactionTimelog.FlaggedNote = entity.FlaggedNote;
//            timeTrackingTransactionTimelog.EnteredDate = entity.EnteredDate;

//            await _unitOfWork.CompleteAsync();
//            //await _timeTrackingTransctionTimeLoagRepository.UpdateAsync(TimeTrackingTransactionTimelogDtoNew.Map(entity));
//        }

//        public async Task<int> InsertAsync(int categoryId, string transactionBy, DateTime startDate,
//            int transactionId, string value)
//        {
//            TimeTrackingTransactionTimelogViewModel timeLog = new TimeTrackingTransactionTimelogViewModel()
//            {
//                CategoryId = categoryId,
//                TransactionBy = transactionBy,
//                TimeStart = startDate,
//                TransactionId = transactionId,
//                Value = value.ToUpper()
//            };
//            var result = TimeTrackingTransactionTimelogDtoNew.Map(timeLog);
//            await _unitOfWork.TransactionTimelogs.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return result.Id;
//        }

//        public async Task<TimeTrackingTransactionTimelogViewModel> GetAsync(string transaction, int categoryId, DateTime todate,
//            bool isCompleted, bool isCancel)
//        {
//            return TimeTrackingTransactionTimelogDtoNew.Map(await _timeTrackingTransctionTimeLoagRepository.FindAsync(x => x.TransactionBy == transaction
//                && (x.TimeStart.HasValue
//                && x.TimeStart.Value.Date == todate.Date)
//                && x.CategoryId == categoryId
//                && (x.IsComplete == null || (x.IsComplete.HasValue && x.IsComplete.Value == isCompleted))
//                && (x.IsCancel == null || (x.IsCancel.HasValue && x.IsCancel == isCancel))));
//        }
//    }
//}
