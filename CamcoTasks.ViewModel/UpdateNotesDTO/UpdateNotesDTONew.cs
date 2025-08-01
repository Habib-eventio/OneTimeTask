using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.UpdateNotesDTO
{
    public static class UpdateNotesDTONew
    {
        public static UpdateNote Map(UpdateNotesViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new UpdateNote
            {
                Id = viewModel.ID,
                UpdateId = viewModel.UpdateID,
                Notes = viewModel.Notes,
                NoteDate = viewModel.NoteDate,
                IsDeleted = viewModel.IsDeleted
            };
        }

        public static UpdateNotesViewModel Map(UpdateNote dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new UpdateNotesViewModel
            {
                ID = dataEntity.Id,
                UpdateID = dataEntity.UpdateId,
                Notes = dataEntity.Notes,
                NoteDate = dataEntity.NoteDate,
                IsDeleted = dataEntity.IsDeleted
            };
        }

        public static IEnumerable<UpdateNotesViewModel> Map(IEnumerable<UpdateNote> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
