using CamcoTasks.Infrastructure.Entities.HR;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.Department
{
    public static class DepartmentDTO
    {
        public static DepartmentAndManager Map(DepartmentViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new DepartmentAndManager
            {
                Id = viewModel.Id,
                DepartmentName = viewModel.Name,
                DepartmentAbbreviation = viewModel.DepartmentAbbreviation,
                DepartmentImage = viewModel.DepartmentImage,
                PrimaryManagerEmployeeId = viewModel.PrimaryManagerId,
                PrimaryManagerCanApproveTimeSheet = viewModel.PrimaryManagerCanApproveTimeSheet,
                LeaderEmployeeId = viewModel.LeaderId,
                LeaderCanApproveTimeSheet = viewModel.LeaderCanApproveTimeSheet,
                IsDeleted = viewModel.IsDeleted,
                CreatedByEmployeeId = viewModel.CreatedById,
                DateCreated = viewModel.DateCreated.Value,
                UpdatedByEmployeeId = viewModel.UpdatedById,
                DateUpdated = viewModel.DateUpdated,
            };
        }

        public static DepartmentViewModel Map(DepartmentAndManager entity)
        {
            if (entity == null) { return null; }

            return new DepartmentViewModel
            {
                Id = entity.Id,
                Name = entity.DepartmentName,
                DepartmentAbbreviation = entity.DepartmentAbbreviation,
                DepartmentImage = entity.DepartmentImage,
                PrimaryManagerId = entity.PrimaryManagerEmployeeId,
                PrimaryManagerCanApproveTimeSheet = entity.PrimaryManagerCanApproveTimeSheet,
                LeaderId = entity.LeaderEmployeeId,
                LeaderCanApproveTimeSheet = entity.LeaderCanApproveTimeSheet,
                IsDeleted = entity.IsDeleted,
                CreatedById = entity.CreatedByEmployeeId,
                DateCreated = entity.DateCreated,
                UpdatedById = entity.UpdatedByEmployeeId,
                DateUpdated = entity.DateUpdated,
            };
        }

        public static IEnumerable<DepartmentViewModel> Map(IEnumerable<DepartmentAndManager> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
