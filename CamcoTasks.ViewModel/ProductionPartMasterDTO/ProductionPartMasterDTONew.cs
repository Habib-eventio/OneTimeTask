//using ERP.Data.Entities.Production;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.ProductionPartMasterDTO
//{
//    public class HR_PublicEmployeesDTO
//    {
//        public static PartMaster Map(ProductionPartMasterViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new PartMaster
//            {
//                EngineeringPrintRevision = viewModel.EngPrintRev,
//                Coordinator = viewModel.PmCoordinator,
//                CustomerCode = viewModel.PmCustCode,
//                CustomerCode2 = viewModel.PmCustCode2,
//                CustomerCode3 = viewModel.PmCustCode3,
//                Description = viewModel.PmDesc,
//                Group = viewModel.PmGroup,
//                IsKanban = viewModel.PmKanban,
//                LeadTime = viewModel.PmLeadtime,
//                OilFrequency = viewModel.PmOilfreq,
//                PartNumber = viewModel.PmPart,
//                PartLength = viewModel.PmPartLength,
//                PartPrice = viewModel.PmPartPrice,
//                ShippingMethod = viewModel.PmShippingmethod,
//                Terms = viewModel.PmTerms
//            };
//        }

//        public static ProductionPartMasterViewModel Map(PartMaster dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new ProductionPartMasterViewModel
//            {
//                EngPrintRev = dataEntity.EngineeringPrintRevision,
//                PmCoordinator = dataEntity.CustomerCode,
//                PmCustCode = dataEntity.Coordinator,
//                PmCustCode2 = dataEntity.CustomerCode2,
//                PmCustCode3 = dataEntity.CustomerCode3,
//                PmDesc = dataEntity.Description,
//                PmGroup = dataEntity.Group,
//                PmKanban = dataEntity.IsKanban,
//                PmLeadtime = dataEntity.LeadTime,
//                PmOilfreq = dataEntity.OilFrequency,
//                PmPart = dataEntity.PartNumber,
//                PmPartLength = dataEntity.PartLength,
//                PmPartPrice = dataEntity.PartPrice,
//                PmShippingmethod = dataEntity.ShippingMethod,
//                PmTerms = dataEntity.Terms
//            };
//        }

//        public static IEnumerable<ProductionPartMasterViewModel> Map(IEnumerable<PartMaster> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
