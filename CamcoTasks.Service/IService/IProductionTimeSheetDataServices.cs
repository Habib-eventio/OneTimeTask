using CamcoTasks.ViewModels.ProductionTimeSheetDataDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IProductionTimeSheetDataServices
    {
        //Task<IEnumerable<ProductionTimeSheetDataViewModel>> GetListByProjectId(int projectId);
        Task<double> GetSumOfBurdenTimeAsync(int projectId);
    }
}
