using CamcoTasks.Infrastructure.Entities.CAMCO;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.CamcoProjectsDTO
{
    public class CamcoProjectsDtoNew
    {
        public static Project Map(CamcoProjectsViewModel viewModel)
        {
            if (viewModel == null) return null;

            return new Project
            {
                Id = viewModel.Id,
                IsActive = viewModel.IsActive,
                IsPostponed = viewModel.IsPostponed,
                Title = viewModel.Title,
                EnteredByEmployeeId = viewModel.EnteredByEmployeeId,
                ChampionEmployeeId = viewModel.ChampionEmployeeId,
                Description = viewModel.Description,
                DateCreated = viewModel.DateCreated,
                ProjectType = viewModel.ProjectType,
                Notes = viewModel.Notes,
                DateUpdated = viewModel.DateUpdated,
                Status = viewModel.Status,
                PostponedReason = viewModel.PostponedReason,
                UpdatedById = viewModel.UpdatedById,
            };
        }

        public static CamcoProjectsViewModel Map(Project entity)
        {
            if (entity == null) return null;

            return new CamcoProjectsViewModel
            {
                Id = (int)entity.Id,
                IsActive = entity.IsActive.Value,
                IsPostponed = entity.IsPostponed,
                Title = entity.Title,
                EnteredByEmployeeId = entity.EnteredByEmployeeId,
                ChampionEmployeeId = entity.ChampionEmployeeId,
                Description = entity.Description,
                DateCreated = entity.DateCreated.Value,
                ProjectType = entity.ProjectType,
                Notes = entity.Notes,
                DateUpdated = entity.DateUpdated,
                Status = entity.Status,
                PostponedReason= entity.PostponedReason,
                UpdatedById = entity.UpdatedById,
            };
        }

        public static IEnumerable<CamcoProjectsViewModel> Map(IEnumerable<Project> dataEntityList)
        {
            if (dataEntityList == null) yield break;

            foreach (Project item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
