using CamcoTasks.ViewModels.ProductionTimeSheetDataDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IProductionService
    {
        Task<IEnumerable<ProductionTimeSheetDataViewModel>> GetData(DateTime from, DateTime to);

        Task<IEnumerable<ProductionTimeSheetDataViewModel>> GetData();
    }
}
