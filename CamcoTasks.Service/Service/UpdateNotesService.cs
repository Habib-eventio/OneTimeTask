using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.UpdateNotesDTO;
using CamcoTasks.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service
{
    public class UpdateNotesService : IUpdateNotesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateNotesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UpdateNotesViewModel>> GetAll()
        {
            var data = UpdateNotesDTONew.Map(await _unitOfWork.UpdateNotes.GetListAsync());
            return data;
        }

        public async Task<IEnumerable<UpdateNotesViewModel>> GetAll(int UpdateId)
        {
            var data = UpdateNotesDTONew.Map(await _unitOfWork.UpdateNotes.FindAllAsync(x => x.UpdateId == UpdateId && !x.IsDeleted));
            return data;
        }

        public async Task<int> Insert(UpdateNotesViewModel data)
        {
            var result = UpdateNotesDTONew.Map(data);
            await _unitOfWork.UpdateNotes.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return result.Id;
        }

        public async Task<bool> Remove(UpdateNotesViewModel data)
        {
            await _unitOfWork.UpdateNotes.RemoveAsync(UpdateNotesDTONew.Map(data));
            return await _unitOfWork.CompleteAsync();
        }

        public async Task Update(UpdateNotesViewModel data)
        {
            var updateNote = await _unitOfWork.UpdateNotes.GetAsync(data.ID);

            updateNote.Id = data.ID;
            updateNote.UpdateId = data.UpdateID;
            updateNote.Notes = data.Notes;
            updateNote.NoteDate = data.NoteDate;
            updateNote.IsDeleted = data.IsDeleted;
            await _unitOfWork.CompleteAsync();

            //await _unitOfWork.UpdateNotes.UpdateAsync(UpdateNotesDTONew.Map(data));
        }
    }
}
