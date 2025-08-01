using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TasksImagesDTO
{
    public class TasksImagesDTONew
    {
        public static TaskImage Map(TasksImagesViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new TaskImage
            {
                Id = viewModel.Id,
                RecurringId = viewModel.RecurringId,
                OneTimeId = viewModel.OneTimeId,
                PictureLink = viewModel.PictureLink,
                IsDeleted = viewModel.IsDeleted,
                ImageNote = viewModel.ImageNote,
                UpdateId = viewModel.UpdateId,
                FileName = viewModel.FileName,
            };
        }

        public static TasksImagesViewModel Map(TaskImage dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new TasksImagesViewModel
            {
                Id = dataEntity.Id,
                RecurringId = dataEntity.RecurringId.Value,
                OneTimeId = dataEntity.OneTimeId.Value,
                PictureLink = dataEntity.PictureLink,
                IsDeleted = dataEntity.IsDeleted.Value,
                ImageNote = dataEntity.ImageNote,
                UpdateId = dataEntity.UpdateId,
                FileName = dataEntity.FileName
            };
        }

        public static IEnumerable<TasksImagesViewModel> Map(IEnumerable<TaskImage> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
