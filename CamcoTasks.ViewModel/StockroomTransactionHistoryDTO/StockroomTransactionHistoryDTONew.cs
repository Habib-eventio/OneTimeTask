//using ERP.Data.Entities.Stockroom;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.StockroomTransactionHistoryDTO
//{
//    public class StockroomTransactionHistoryDTONew
//    {
//        public static TransactionHistory Map(StockroomTransactionHistoryViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new TransactionHistory
//            {
//                SsmaTimeStamp = viewModel.SsmaTimeStamp,
//                CompletedBy = viewModel.CompletedBy,
//                CustomerOrderNumber = viewModel.Conumber,
//                IsPartial = viewModel.IsPartial,
//                ItemId = viewModel.ItemId,
//                Location = viewModel.Location,
//                NewStockQuantity = viewModel.NewStockQty,
//                Note = viewModel.Note,
//                OldStockQuantity = viewModel.OldStockQty,
//                PackType = viewModel.PackType,
//                PalId = viewModel.PalId,
//                PartNumber = viewModel.PartNumber,
//                PurchaseOrderNumber = viewModel.Ponum,
//                PurchaseOrderNumber1 = viewModel.Ponum1,
//                QuantityMoved = viewModel.QuantityMoved,
//                ShopOrderNumber = viewModel.Sonumber,
//                TransactionDate = viewModel.TransactionDate,
//                TransactionType = viewModel.TransactionType,
//                Id = viewModel.TransId
//            };
//        }

//        public static StockroomTransactionHistoryViewModel Map(TransactionHistory dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new StockroomTransactionHistoryViewModel
//            {
//                SsmaTimeStamp = dataEntity.SsmaTimeStamp,
//                CompletedBy = dataEntity.CompletedBy,
//                Conumber = dataEntity.CustomerOrderNumber,
//                IsPartial = dataEntity.IsPartial,
//                ItemId = dataEntity.ItemId,
//                Location = dataEntity.Location,
//                NewStockQty = dataEntity.NewStockQuantity,
//                Note = dataEntity.Note,
//                OldStockQty = dataEntity.OldStockQuantity,
//                PackType = dataEntity.PackType,
//                PalId = dataEntity.PalId,
//                PartNumber = dataEntity.PartNumber,
//                Ponum = dataEntity.PurchaseOrderNumber,
//                Ponum1 = dataEntity.PurchaseOrderNumber1,
//                QuantityMoved = dataEntity.QuantityMoved,
//                Sonumber = dataEntity.ShopOrderNumber,
//                TransactionDate = dataEntity.TransactionDate,
//                TransactionType = dataEntity.TransactionType,
//                TransId = dataEntity.Id
//            };
//        }

//        public static IEnumerable<StockroomTransactionHistoryViewModel> Map(IEnumerable<TransactionHistory> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
