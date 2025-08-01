//using ERP.Data.Entities.Stockroom;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.StockroomStockPartsPartEntriesDTO
//{
//    public class StockroomStockPartsPartEntriessDTO
//    {
//        public static StockPartPartEntry Map(StockroomStockPartsPartEntriesViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new StockPartPartEntry
//            {
//                PartNumber = viewModel.PartNumber,
//                Cost = viewModel.Cost,
//                DefaultStock = viewModel.DefaultStock,
//                LastAudit = viewModel.LastAudit,
//                Location = viewModel.Location,
//                PackInstId = viewModel.PackInstId,
//                Id = viewModel.PartLocId,
//                PkgQuantity = viewModel.PkgQty,
//                SsmaTimeStamp = viewModel.SsmaTimeStamp,
//                StockQuantity = viewModel.StockQty,
//            };
//        }

//        public static StockroomStockPartsPartEntriesViewModel Map(StockPartPartEntry dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new StockroomStockPartsPartEntriesViewModel
//            {
//                PartNumber = dataEntity.PartNumber,
//                Cost = dataEntity.Cost,
//                DefaultStock = dataEntity.DefaultStock,
//                LastAudit = dataEntity.LastAudit,
//                Location = dataEntity.Location,
//                PackInstId = dataEntity.PackInstId,
//                PartLocId = dataEntity.Id,
//                PkgQty = dataEntity.PkgQuantity,
//                SsmaTimeStamp = dataEntity.SsmaTimeStamp,
//                StockQty = dataEntity.StockQuantity,
//            };
//        }

//        public static IEnumerable<StockroomStockPartsPartEntriesViewModel> Map(IEnumerable<StockPartPartEntry> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
