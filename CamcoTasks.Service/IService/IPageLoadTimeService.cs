using CamcoTasks.ViewModels.PageLoadTimeDTO;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IPageLoadTimeService
    {
        Task<int> InsertAsync(PageLoadTimeViewModel pageLoadTime);
    }
}
