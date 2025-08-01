using System.Threading.Tasks;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;

namespace CamcoTasks.Service.Service
{
    /// <summary>
    /// Fallback service used when PageLoadTime functionality is disabled.
    /// </summary>
    public class NullPageLoadTimeService : IPageLoadTimeService
    {
        public Task<int> InsertAsync(PageLoadTimeViewModel pageLoadTime)
            => Task.FromResult(0);
    }
}
