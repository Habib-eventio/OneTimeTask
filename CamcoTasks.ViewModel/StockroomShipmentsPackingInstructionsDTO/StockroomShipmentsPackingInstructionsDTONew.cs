//using ERP.Data.Entities.Stockroom;
//using System;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.StockroomShipmentsPackingInstructionsDTO
//{
//    public class StockroomShipmentsPackingInstructionsNew
//    {
//        public static ShipmentPackingInstruction Map(StockroomShipmentsPackingInstructionsViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new ShipmentPackingInstruction
//            {
//                Id = viewModel.Id,
//                SsmaTimeStamp = viewModel.SsmaTimeStamp,
//                AdditionalDocs = viewModel.AdditionalDocs,
//                BagType = viewModel.BagType,
//                BoxSize = viewModel.BoxSize,
//                Comment = viewModel.Comment,
//                CreatedBy = viewModel.CreatedBy,
//                CrumpledPaper = viewModel.CrumpledPaper,
//                CustomerId = viewModel.CustId,
//                Customer = viewModel.Customer,
//                DateTimeAdded = viewModel.DateTimeAdded,
//                Instruct = viewModel.Instruct,
//                PartNumber = viewModel.PartNumber,
//                PictureLink = viewModel.PicLink,
//                ProdCreatedBy = viewModel.ProdCreatedBy,
//                Quantity = viewModel.Qty,
//                StackHeight = viewModel.StackHeight,

//            };
//        }

//        public static StockroomShipmentsPackingInstructionsViewModel Map(ShipmentPackingInstruction dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new StockroomShipmentsPackingInstructionsViewModel
//            {
//                Id = dataEntity.Id,
//                SsmaTimeStamp = dataEntity.SsmaTimeStamp,
//                AdditionalDocs = dataEntity.AdditionalDocs,
//                BagType = dataEntity.BagType,
//                BoxSize = dataEntity.BoxSize,
//                Comment = dataEntity.Comment,
//                CreatedBy = dataEntity.CreatedBy,
//                CrumpledPaper = dataEntity.CrumpledPaper,
//                CustId = dataEntity.CustomerId,
//                Customer = dataEntity.Customer,
//                DateTimeAdded = dataEntity.DateTimeAdded,
//                Instruct = dataEntity.Instruct,
//                PartNumber = dataEntity.PartNumber,
//                PicLink = dataEntity.PictureLink,
//                ProdCreatedBy = dataEntity.ProdCreatedBy,
//                Qty = dataEntity.Quantity,
//                StackHeight = dataEntity.StackHeight,
//            };
//        }

//        public static IEnumerable<StockroomShipmentsPackingInstructionsViewModel> Map(IEnumerable<ShipmentPackingInstruction> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
