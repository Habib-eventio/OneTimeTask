//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.QualityLineInspectionsDTO;
//using ERP.Data.Entities.Quality;
//using ERP.Repository.IRepository.Quality;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class QualityLineInspectionsService : IQualityLineInspectionsService
//    {
//        private readonly IQualityLineInspectionRepository _qualityLineInspectionsRepository;

//        public QualityLineInspectionsService(IQualityLineInspectionRepository qualityLineInspectionsRepository)
//        {
//            _qualityLineInspectionsRepository = qualityLineInspectionsRepository;
//        }

//        public async Task<IEnumerable<QualityLineInspectionsViewModel>> GetList(DateTime firstDay, DateTime lastDay)
//        {
//            return QualityLineInspectionsDTONew.Map(await _qualityLineInspectionsRepository.FindAllAsync(l => l.DateAdded >= firstDay && l.DateAdded <= lastDay));
//        }
//    }
//}
