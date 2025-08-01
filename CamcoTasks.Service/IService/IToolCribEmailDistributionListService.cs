using CamcoTasks.ViewModels.ToolCribEmailDistributionListDTO;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IToolCribEmailDistributionListService
    {
        Task<ToolCribEmailDistributionListViewModel> GetByIdAsync(int id);
    }
}
