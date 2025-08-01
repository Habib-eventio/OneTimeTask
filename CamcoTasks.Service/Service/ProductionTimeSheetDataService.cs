using CamcoTasks.Infrastructure.IRepository.Production;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.ProductionTimeSheetDataDTO;
using ERP.Repository.IRepository.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service
{
    public class ProductionTimeSheetDataService : IProductionTimeSheetDataServices
    {
        private readonly ITimeSheetDatumRepository _productionTimeSheetDataRepository;

        public ProductionTimeSheetDataService(ITimeSheetDatumRepository productionTimeSheetDataRepository)
        {
            _productionTimeSheetDataRepository = productionTimeSheetDataRepository;
        }

        //public async Task<IEnumerable<ProductionTimeSheetDataViewModel>> GetListByProjectId(int projectId)
        //{
        //    return ProductionTimeSheetDataDTONew.Map(await _productionTimeSheetDataRepository
        //        .FindAllAsync(p => p.ProjectId == projectId));
        //}

        public async Task<double> GetSumOfBurdenTimeAsync(int projectId)
        {
            return await _productionTimeSheetDataRepository
                .GetSumOfBurdenTimeAsync(projectId);
        }
    }
}
