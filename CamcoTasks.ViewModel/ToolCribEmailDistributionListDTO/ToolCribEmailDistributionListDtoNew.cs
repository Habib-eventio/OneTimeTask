//using ERP.Data.Entities.ToolCrib;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.ToolCribEmailDistributionListDTO
//{
//    public class ToolCribEmailDistributionListDtoNew
//    {
//        public static EmailDistributionList Map(ToolCribEmailDistributionListViewModel viewModel)
//        {
//            if (viewModel == null) return null;

//            return new EmailDistributionList
//            {
//                Id = viewModel.Id,
//                EmployeeId = viewModel.EmpId,
//                IsActive = viewModel.Active,
//                EmailOptionId = viewModel.EmailOptionId,
//            };
//        }

//        public static ToolCribEmailDistributionListViewModel Map(EmailDistributionList entity)
//        {
//            if (entity == null) return null;

//            return new  ToolCribEmailDistributionListViewModel
//            {
//                Id = entity.Id,
//                EmpId = entity.EmployeeId,
//                Active = entity.IsActive,
//                EmailOptionId = entity.EmailOptionId,
//            };
//        }

//        public static IEnumerable<ToolCribEmailDistributionListViewModel> Map(IEnumerable<EmailDistributionList> dataEntityList)
//        {
//            if (dataEntityList == null) yield break;

//            foreach (EmailDistributionList item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
