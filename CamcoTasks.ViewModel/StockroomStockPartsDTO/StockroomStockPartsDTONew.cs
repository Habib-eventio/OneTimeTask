//using ERP.Data.Entities.Stockroom;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.StockroomStockPartsDTO
//{
//    public class StockroomStockPartsDTONew
//    {
//        public static StockPart Map(StockroomStockPartsViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new StockPart
//            {
//                PartNumber = viewModel.PartNumber,
//                Customer = viewModel.Customer,
//                CustomerId = viewModel.CustId,
//                Status = viewModel.Status,
//            };
//        }

//        public static StockroomStockPartsViewModel Map(StockPart dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new StockroomStockPartsViewModel
//            {
//                PartNumber = dataEntity.PartNumber,
//                Customer = dataEntity.Customer,
//                CustId = dataEntity.CustomerId,
//                Status = dataEntity.Status,
//            };
//        }

//        public static IEnumerable<StockroomStockPartsViewModel> Map(IEnumerable<StockPart> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
