//using ERP.Data.Entities.HR;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.JobDescriptions
//{
//    public class JobDescriptionsDtoNew
//    {
//        public static JobDescription Map(JobDescriptionsViewModal viewModal)
//        {
//            if (viewModal == null) { return null; }

//            return new JobDescription()
//            {
//                Id = viewModal.Id,
//                Name = viewModal.Name,
//                RecommendedTierLevelId = viewModal.RecommendedTierLevelId,
//                Details = viewModal.Details,
//                IsDeleted = viewModal.IsDeleted,
//                CreatedByEmployeeId = viewModal.CreatedById,
//                DateCreated = viewModal.DateCreated,
//                UpdatedByEmployeeId = viewModal.UpdatedById,
//                DateUpdated = viewModal.DateUpdated,
//            };
//        }

//        public static JobDescriptionsViewModal Map(JobDescription entity)
//        {
//            if(entity == null) { return null; }

//            return new JobDescriptionsViewModal()
//            {
//                Id = entity.Id,
//                Name = entity.Name,
//                RecommendedTierLevelId = entity.RecommendedTierLevelId,
//                Details = entity.Details,
//                IsDeleted = entity.IsDeleted,
//                CreatedById = entity.CreatedByEmployeeId,
//                DateCreated = entity.DateCreated,
//                UpdatedById = entity.UpdatedByEmployeeId,
//                DateUpdated = entity.DateUpdated,
//            };
//        }

//        public static IEnumerable<JobDescriptionsViewModal> Map(IEnumerable<JobDescription> entityList)
//        {
//            if (entityList == null) { yield break; }

//            foreach (var entity in entityList)
//            {
//                yield return Map(entity);
//            }
//        }
//    }
//}
