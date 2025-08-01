//using ERP.Data.Entities.Metric;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.MetricsHistoryDTO
//{
//    public class MetricsHistoryDTONew
//    {
//        public static MetricHistory Map(MetricsHistoryViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new MetricHistory
//            {
//                Id = viewModel.Id,
//                Date = viewModel.Date,
//                DaysPackedAhead = viewModel.DaysPackedAhead,
//                InventoryTotal = viewModel.InventoryTotal,
//                PackagedInventoryTotal = viewModel.PkgInventoryTotal,
//                ShopOutputExpenses = viewModel.ShopOutputExpenses,
//                ShopOutputProfitLoss = viewModel.ShopOutputProfitLoss,
//                ShopOutputTotal = viewModel.ShopOutputTotal,
//                WorkInProgressTotal = viewModel.WIPTotal
//            };
//        }

//        public static MetricsHistoryViewModel Map(MetricHistory dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new MetricsHistoryViewModel
//            {
//                Id = dataEntity.Id,
//                Date = dataEntity.Date,
//                DaysPackedAhead = dataEntity.DaysPackedAhead,
//                InventoryTotal = dataEntity.InventoryTotal,
//                PkgInventoryTotal = dataEntity.PackagedInventoryTotal,
//                ShopOutputExpenses = dataEntity.ShopOutputExpenses,
//                ShopOutputProfetLoss = dataEntity.ShopOutputProfitLoss,
//                ShopOutputTotal = dataEntity.ShopOutputTotal,
//                WIPTotal = dataEntity.WorkInProgressTotal
//            };
//        }

//        public static IEnumerable<MetricsHistoryViewModel> Map(IEnumerable<MetricHistory> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
