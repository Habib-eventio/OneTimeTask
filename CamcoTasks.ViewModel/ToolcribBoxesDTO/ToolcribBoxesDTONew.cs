//using ERP.Data.Entities.ToolCrib;
//using System;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.ToolcribBoxesDTO
//{
//    public class ToolcribBoxesDTONew
//    {
//        public static Box Map(ToolcribBoxesViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new Box
//            {
//                Id = viewModel.BoxId,
//                Description = viewModel.Description,
//                Height = viewModel.Height,
//                Length = viewModel.Length,
//                Price = viewModel.Price,
//                QRN = viewModel.Qrn,
//                SSMA_TimeStamp = viewModel.SsmaTimeStamp,
//                Vendor = viewModel.Vendor,
//                VenNum = viewModel.VenNum.ToString(),
//                Weight = viewModel.Weight,
//                Width = viewModel.Width
//            };
//        }

//        public static ToolcribBoxesViewModel Map(Box dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new ToolcribBoxesViewModel
//            {
//                BoxId = dataEntity.Id,
//                Description = dataEntity.Description,
//                Height = dataEntity.Height,
//                Length = dataEntity.Length,
//                Price = dataEntity.Price,
//                Qrn = dataEntity.QRN,
//                SsmaTimeStamp = dataEntity.SSMA_TimeStamp,
//                Vendor = dataEntity.Vendor,
//                VenNum = Convert.ToInt32(dataEntity.VenNum),
//                Weight = dataEntity.Weight,
//                Width = dataEntity.Width
//            };
//        }

//        public static IEnumerable<ToolcribBoxesViewModel> Map(IEnumerable<Box> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
