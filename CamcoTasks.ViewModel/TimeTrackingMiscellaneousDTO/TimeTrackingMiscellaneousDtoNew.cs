//using ERP.Data.Entities.TimeTracking;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.TimeTrackingMiscellaneousDTO
//{
//    public class TimeTrackingMiscellaneousDtoNew
//    {
//        public static Miscellaneous Map(TimeTrackingMiscellaneousViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new Miscellaneous
//            {
//                Id = viewModel.Id,
//                WorkDetails = viewModel.WorkDetails
//            };
//        }

//        public static TimeTrackingMiscellaneousViewModel Map(Miscellaneous dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new TimeTrackingMiscellaneousViewModel
//            {
//                Id = dataEntity.Id,
//                WorkDetails = dataEntity.WorkDetails
//            };
//        }

//        public static IEnumerable<TimeTrackingMiscellaneousViewModel> Map(IEnumerable<Miscellaneous> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
