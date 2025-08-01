//using ERP.Data.Entities.Metric;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.MetricsGagingDataDTO
//{
//    public class MetricsGagingDataDTONew
//    {
//        public static GagingData Map(MetricsGagingDataViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new GagingData
//            {
//                DateReported = viewModel.DateReported,
//                Id = viewModel.Id,
//                IsDeleted = viewModel.IsDeleted.Value,
//                NumberOfGagesBehindCalibration = viewModel.NumberOfGagesBehindCalibration,
//                NumberOfLostGages = viewModel.NumberOfLostGages
//            };
//        }

//        public static MetricsGagingDataViewModel Map(GagingData dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new MetricsGagingDataViewModel
//            {
//                DateReported = dataEntity.DateReported,
//                Id = dataEntity.Id,
//                IsDeleted = dataEntity.IsDeleted,
//                NumberOfGagesBehindCalibration = dataEntity.NumberOfGagesBehindCalibration,
//                NumberOfLostGages = dataEntity.NumberOfLostGages
//            };
//        }

//        public static IEnumerable<MetricsGagingDataViewModel> Map(IEnumerable<GagingData> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
