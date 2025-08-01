using CamcoTasks.ViewModels.UpdateNotesDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IUpdateNotesService
    {
        Task<IEnumerable<UpdateNotesViewModel>> GetAll();
        Task<IEnumerable<UpdateNotesViewModel>> GetAll(int UpdateId);
        Task<int> Insert(UpdateNotesViewModel data);
        Task<bool> Remove(UpdateNotesViewModel data);
        Task Update(UpdateNotesViewModel data);
    }
}
