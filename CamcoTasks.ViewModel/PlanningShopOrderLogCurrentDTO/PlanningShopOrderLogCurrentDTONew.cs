//using ERP.Data.Entities.Planning;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.PlanningShopOrderLogCurrentDTO
//{
//    public class PlanningShopOrderLogCurrentDTONew
//    {
//        public static ShopOrderNumberLogCurrent Map(PlanningShopOrderLogCurrentViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new ShopOrderNumberLogCurrent
//            {
//                Customer = viewModel.Customer,
//                Date = viewModel.Date,
//                DateClosed = viewModel.DateClosed,
//                EnteredBy = viewModel.Notes,
//                Id = viewModel.Id,
//                Notes = viewModel.Notes,
//                QuantityToStock = viewModel.QuantityToStock,
//                ToStockDate = viewModel.ToStockDate
//            };
//        }

//        public static PlanningShopOrderLogCurrentViewModel Map(ShopOrderNumberLogCurrent dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new PlanningShopOrderLogCurrentViewModel
//            {
//                Customer = dataEntity.Customer,
//                Date = dataEntity.Date,
//                DateClosed = dataEntity.DateClosed,
//                EnteredBy = dataEntity.Notes,
//                Id = dataEntity.Id,
//                Notes = dataEntity.Notes,
//                QuantityToStock = dataEntity.QuantityToStock,
//                ToStockDate = dataEntity.ToStockDate
//            };
//        }

//        public static IEnumerable<PlanningShopOrderLogCurrentViewModel> Map(IEnumerable<ShopOrderNumberLogCurrent> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
