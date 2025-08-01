using CamcoTasks.Infrastructure.Entities.Page;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.PageLoadTimeDTO
{
    public class PageLoadTimeDTONew
    {
        public static LoadTime Map(PageLoadTimeViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new LoadTime
            {
                Id = viewModel.Id,
                PageName = viewModel.PageName,
                SectionName = viewModel.SectionName,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                DateCreated = viewModel.DateCreated,
            };
        }

        public static PageLoadTimeViewModel Map(LoadTime dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new PageLoadTimeViewModel
            {
                Id = dataEntity.Id,
                PageName = dataEntity.PageName,
                SectionName = dataEntity.SectionName,
                StartTime = dataEntity.StartTime,
                EndTime = dataEntity.EndTime,
                DateCreated = dataEntity.DateCreated,
            };
        }

        public static IEnumerable<PageLoadTimeViewModel> Map(IEnumerable<LoadTime> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
