//using ERP.Data.Entities.TimeTracking;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.TimeTrackingTransactionTimelogDTO
//{
//    public class TimeTrackingTransactionTimelogDtoNew
//    {
//        public static TransactionTimelog Map(TimeTrackingTransactionTimelogViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new TransactionTimelog
//            {
//                Id = viewModel.Id,
//                GenericId = viewModel.GenericId,
//                TransactionId = viewModel.TransactionId,
//                CategoryId = viewModel.CategoryId,
//                TimeStart = viewModel.TimeStart,
//                TimeEnd = viewModel.TimeEnd,
//                Difference = viewModel.Difference,
//                TimeTaken = viewModel.TimeTaken,
//                PageName = viewModel.PageName,
//                IsPaused = viewModel.IsPaused,
//                IsComplete = viewModel.IsComplete,
//                TransactionBy = viewModel.TransactionBy,
//                Description = viewModel.Description,
//                Value = viewModel.Value,
//                Description2 = viewModel.Description2,
//                Value2 = viewModel.Value2,
//                IsCancel = viewModel.IsCancel,
//                IsFlagged = viewModel.IsFlagged,
//                FlaggedNote = viewModel.FlaggedNote,
//                EnteredDate = viewModel.EnteredDate,
//            };
//        }

//        public static TimeTrackingTransactionTimelogViewModel Map(TransactionTimelog dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new TimeTrackingTransactionTimelogViewModel
//            {
//                Id = dataEntity.Id,
//                GenericId = dataEntity.GenericId,
//                TransactionId = dataEntity.TransactionId,
//                CategoryId = dataEntity.CategoryId.Value,
//                TimeStart = dataEntity.TimeStart,
//                TimeEnd = dataEntity.TimeEnd,
//                Difference = dataEntity.Difference,
//                TimeTaken = dataEntity.TimeTaken,
//                PageName = dataEntity.PageName,
//                IsPaused = dataEntity.IsPaused,
//                IsComplete = dataEntity.IsComplete,
//                TransactionBy = dataEntity.TransactionBy,
//                Description = dataEntity.Description,
//                Value = dataEntity.Value,
//                Description2 = dataEntity.Description2,
//                Value2 = dataEntity.Value2,
//                IsCancel = dataEntity.IsCancel,
//                IsFlagged = dataEntity.IsFlagged,
//                FlaggedNote = dataEntity.FlaggedNote,
//                EnteredDate = dataEntity.EnteredDate,
//            };
//        }

//        public static IEnumerable<TimeTrackingTransactionTimelogViewModel> Map(IEnumerable<TransactionTimelog> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
