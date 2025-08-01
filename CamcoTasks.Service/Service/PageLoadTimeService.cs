using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.Infrastructure;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service
{
    public class PageLoadTimeService : IPageLoadTimeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PageLoadTimeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<int> InsertAsync(PageLoadTimeViewModel pageLoadTime)
        {
            var result = PageLoadTimeDTONew.Map(pageLoadTime);
            await _unitOfWork.LoadTimes.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return result.Id;
        }
    }
}
