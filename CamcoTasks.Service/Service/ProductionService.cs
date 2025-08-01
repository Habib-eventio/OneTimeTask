//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.ProductionTimeSheetDataDTO;
//using ERP.Repository.IRepository.Production;
//using CamcoTasks.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class ProductionService : IProductionService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public ProductionService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<IEnumerable<ProductionTimeSheetDataViewModel>> GetData(DateTime from, DateTime to)
//        {
//            return ProductionTimeSheetDataDTONew.Map(await _unitOfWork.TimeSheetsData.FindAllAsync(x => x.Date.Value.Date >= from && x.Date.Value.Date <= to));
//        }

//        public async Task<IEnumerable<ProductionTimeSheetDataViewModel>> GetData()
//        {
//            return ProductionTimeSheetDataDTONew.Map(await _unitOfWork.TimeSheetsData.GetListAsync());
//        }
//    }
//}
