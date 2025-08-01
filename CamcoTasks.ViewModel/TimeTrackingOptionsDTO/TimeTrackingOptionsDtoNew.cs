//using ERP.Data.Entities.TimeTracking;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.TimeTrackingOptionsDTO
//{
//    public class TimeTrackingOptionsDtoNew
//    {
//        public static Option Map(TimeTrackingOptionsViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new Option
//            {
//                Id = viewModel.Id,
//                OptionName = viewModel.OptionName,
//                IsActive = viewModel.Active,
//                AppName = viewModel.AppName
//            };
//        }

//        public static TimeTrackingOptionsViewModel Map(Option dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new TimeTrackingOptionsViewModel
//            {
//                Id = dataEntity.Id,
//                OptionName = dataEntity.OptionName,
//                Active = dataEntity.IsActive.Value,
//                AppName = dataEntity.AppName
//            };
//        }

//        public static IEnumerable<TimeTrackingOptionsViewModel> Map(IEnumerable<Option> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
