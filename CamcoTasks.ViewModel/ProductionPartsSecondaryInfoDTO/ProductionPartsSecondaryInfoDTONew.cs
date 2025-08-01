//using ERP.Data.Entities.Production;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.ProductionPartsSecondaryInfoDTO
//{
//    public class HR_PublicEmployeesDTO
//    {
//        public static PartSecondaryInfo Map(ProductionPartsSecondaryInfoViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new PartSecondaryInfo
//            {
//                Id = viewModel.Id,
//                PmPart = viewModel.PmPart,
//                ImageLoc = viewModel.ImageLoc,
//                PmWeight = viewModel.PmWeight
//            };
//        }

//        public static ProductionPartsSecondaryInfoViewModel Map(PartSecondaryInfo dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new ProductionPartsSecondaryInfoViewModel
//            {
//                Id = dataEntity.Id,
//                PmPart = dataEntity.PmPart,
//                ImageLoc = dataEntity.ImageLoc,
//                PmWeight = dataEntity.PmWeight
//            };
//        }

//        public static IEnumerable<ProductionPartsSecondaryInfoViewModel> Map(IEnumerable<PartSecondaryInfo> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
