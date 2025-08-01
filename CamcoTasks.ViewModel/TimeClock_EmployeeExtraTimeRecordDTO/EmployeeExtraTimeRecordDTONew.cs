//using ERP.Data.Entities.TimeClock;
//using System.Collections.Generic;
//using static CamcoTasks.Infrastructure.Entities.TimeClock.EmployeeWeeklyHoursReportViewModel;

//namespace CamcoTasks.ViewModels.TimeClock_EmployeeExtraTimeRecordDTO
//{
//    public class TimeClock_EmployeeExtraTimeRecordDTO
//    {
//        public static EmployeeExtraTimeRecord Map(TimeClock_EmployeeExtraTimeRecordViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new EmployeeExtraTimeRecord
//            {
//                Date = viewModel.Date,
//                HrEmployeeId = viewModel.EmployeeId,
//                ExtraTimeCategoryId = viewModel.ExtraTimeCategoryId,
//                EntryId =viewModel.EntryId,
//                ExtraTimeId = viewModel.ExtraTimeId,
//                TotalHours = viewModel.TotalHours
//            };
//        }

//        public static TimeClock_EmployeeExtraTimeRecordViewModel Map(EmployeeExtraTimeRecord dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new TimeClock_EmployeeExtraTimeRecordViewModel
//            {
//                Date = dataEntity.Date,
//                EmployeeId = dataEntity.HrEmployeeId.Value,
//                ExtraTimeCategoryId = dataEntity.ExtraTimeCategoryId,
//                EntryId = dataEntity.EntryId,
//                ExtraTimeId = dataEntity.ExtraTimeId,
//                TotalHours = dataEntity.TotalHours
//            };
//        }

//        public static IEnumerable<TimeClock_EmployeeExtraTimeRecordViewModel> Map(IEnumerable<EmployeeExtraTimeRecord> dataList)
//        {
//            if (dataList == null) { yield break; }
//            foreach (var item in dataList)
//            {
//                yield return Map(item);
//            }
//        }

//        public static IEnumerable<EmployeeExtraTimeRecord> Map(IEnumerable<TimeClock_EmployeeExtraTimeRecordViewModel> dataList)
//        {
//            if (dataList == null) { yield break; }
//            foreach (var item in dataList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
