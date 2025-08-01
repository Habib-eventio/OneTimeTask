//using ERP.Data.CustomModels.Other;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.MaintenanceWorkOrderDataDTO;
//using CamcoTasks.ViewModels.MetricsFieldsDTO;
//using CamcoTasks.ViewModels.MetricsGagingDataDTO;
//using CamcoTasks.ViewModels.MetricsGoalsDTO;
//using CamcoTasks.ViewModels.PlanningOverheadDTO;
//using CamcoTasks.ViewModels.PurchasingPurchaseRequestsDTO;
//using CamcoTasks.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class MetricService : IMetricService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public MetricService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<bool> AddMetricData(MetricsGagingDataViewModel data)
//        {
//            var result = MetricsGagingDataDTONew.Map(data);
//            await _unitOfWork.GagingsData.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return true;
//        }

//        public async Task<bool> AddOverheadData(PlanningOverheadViewModel data)
//        {
//            var result = PlanningOverheadDTONew.Map(data);
//            await _unitOfWork.Overheads.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return true;
//        }

//        public async Task<List<MetricsDataCustom>> GetAllCustomGageData()
//        {
//            return await _unitOfWork.MetricTypes.GetAllCustomGageData();
//        }

//        public async Task<List<CustomOverHeads>> GetAllCustomOverheadData()
//        {
//            return await _unitOfWork.MetricTypes.GetAllCustomOverheadData();
//        }

//        public async Task<IEnumerable<MetricsGagingDataViewModel>> GetAllGageData()
//        {
//            return MetricsGagingDataDTONew.Map(await _unitOfWork.GagingsData.FindAllAsync(x => x.IsDeleted == null || x.IsDeleted == false));
//        }

//        public async Task<IEnumerable<PlanningOverheadViewModel>> GetAllOverheadData()
//        {
//            var result = await _unitOfWork.Overheads.FindAllAsync(x => (x.IsDeleted == null ||
//                                                                        x.IsDeleted == false) &&
//                                                                       x.OverheadDate.HasValue);
//            return PlanningOverheadDTONew.Map(result);
//        }

//        public async Task<List<MaintenanceOpenTicketDates>> GetCustomMaintenanceOpenTickets()
//        {
//            return await _unitOfWork.MetricTypes.GetCustomMaintenanceOpenTickets();
//        }

//        public async Task<IEnumerable<MaintenanceWorkOrderDataViewModel>> GetCustomMaintenanceOpenTicketsByDate(DateTime actualdate)
//        {
//            return MaintenanceWorkOrderDataDTONew.Map(await _unitOfWork.WorkOrdersData.FindAllAsync(
//            x => x.DateTimeOpen <= actualdate &&
//                (x.DateTimeComplete == null || x.DateTimeComplete > actualdate.AddDays(1)) &&
//                x.IsActive == true));
//        }

//        public async Task<List<TempShopDailyOutput>> GetFinalDailyShopOutput()
//        {
//            return await _unitOfWork.MetricTypes.GetFinalDailyShopOutput();
//        }

//        public async Task<IEnumerable<MaintenanceWorkOrderDataViewModel>> GetMaintenanceOpenTickets()
//        {
//            return MaintenanceWorkOrderDataDTONew.Map(await _unitOfWork.WorkOrdersData.GetListAsync());
//        }

//        public async Task<IEnumerable<MaintenanceWorkOrderDataViewModel>> GetMaintenancePriorityOneByDate(DateTime actualdate)
//        {
//            return MaintenanceWorkOrderDataDTONew.Map(await _unitOfWork.WorkOrdersData.FindAllAsync(x => x.DateTimeOpen <= actualdate &&
//                        (x.DateTimeComplete == null || x.DateTimeComplete > actualdate.AddDays(1))
//                        && x.Priority == 1 && x.IsActive == true));
//        }

//        public async Task<List<MetricsHistoryCustomData>> GetMetricsHistory()
//        {
//            return await _unitOfWork.MetricTypes.GetMetricsHistory();
//        }

//        public async Task<List<MetricsHistoryCustomData>> GetMetricsHistoryDaysPackedAhead()
//        {
//            return await _unitOfWork.MetricTypes.GetMetricsHistoryDaysPackedAhead();

//        }

//        public async Task<int> GetOpenPOCount()
//        {
//            return await _unitOfWork.MetricTypes.GetOpenPOCount();
//        }

//        public async Task<IEnumerable<PurchasingPurchaseRequestsViewModel>> GetOpenPO()
//        {
//            return PurchasingPurchaseRequestsDTONew.Map(await _unitOfWork.PurchaseRequests.FindAllAsync(a => a.Status == "1. Open" || a.Status == "3. Waiting For Quote" || a.Status == "7. Waiting On Approval"));
//        }

//        public async Task<int> GetOpenShopOrderCount()
//        {
//            return await _unitOfWork.MetricTypes.GetOpenShopOrderCount();
//        }

//        public async Task<List<TempOverweightBoxModel>> GetOverweightBoxes()
//        {
//            return await _unitOfWork.MetricTypes.GetOverweightBoxes();
//        }

//        public async Task<IEnumerable<PurchasingPurchaseRequestsViewModel>> GetPOReqData()
//        {
//            return PurchasingPurchaseRequestsDTONew.Map(await _unitOfWork.MetricTypes.GetPOReqData());
//        }

//        public async Task<List<POsTotals>> GetPORequestsTotals()
//        {
//            return await _unitOfWork.MetricTypes.GetPORequestsTotals();
//        }

//        public async Task<decimal> GetPrevWeekInvoiceTotal()
//        {
//            return await _unitOfWork.MetricTypes.GetPrevWeekInvoiceTotal();
//        }

//        public async Task<decimal> GetPrevWeekShipmentsTotal()
//        {
//            return await _unitOfWork.MetricTypes.GetPrevWeekShipmentsTotal();
//        }

//        public async Task<List<TempCOModded>> GetTopFiftyUnpackedCOs()
//        {
//            return await _unitOfWork.MetricTypes.GetTopFiftyUnpackedCOs();
//        }

//        public async Task<List<TempCOModded>> GetTopTwentyUnpackedCOs()
//        {
//            return await _unitOfWork.MetricTypes.GetTopTwentyUnpackedCOs();
//        }

//        public async Task<double> GetTotalInventoryCost()
//        {
//            return await _unitOfWork.MetricTypes.GetTotalInventoryCost();
//        }

//        public async Task<decimal> GetTotalOpenWorkOrders()
//        {
//            return await _unitOfWork.MetricTypes.GetTotalOpenWorkOrders();
//        }

//        public async Task<double> GetTotalPkgInventoryCost()
//        {
//            return await _unitOfWork.MetricTypes.GetTotalPkgInventoryCost();
//        }

//        public async Task<List<RmaCustom>> GetTotalRMA()
//        {
//            return await _unitOfWork.MetricTypes.GetTotalRMA();
//        }

//        public async Task<List<RmaCustom>> GetTotalRMA2()
//        {
//            return await _unitOfWork.MetricTypes.GetTotalRMA2();
//        }

//        public async Task<decimal> GetTotalTopPriorityWorkOrders()
//        {
//            return await _unitOfWork.MetricTypes.GetTotalTopPriorityWorkOrders();
//        }

//        public async Task<List<WeeklyInvoice>> GetWeeklyInvoiceAsync()
//        {
//            return await _unitOfWork.MetricTypes.GetWeeklyInvoiceAsync();
//        }

//        public async Task<double> GetWIPTotalCost()
//        {
//            return await _unitOfWork.MetricTypes.GetWIPTotalCost();
//        }

//        public async Task<int> RMAPrevYear()
//        {
//            return await _unitOfWork.MetricTypes.RMAPrevYear();
//        }

//        public async Task<int> RMAYearToDate()
//        {
//            return await _unitOfWork.RmaMasters.CountAsync(a => a.Rmadate.Year == DateTime.Now.Year);
//            //return await _unitOfWork.MetricTypes.RMAYearToDate();
//        }

//        public async Task<decimal> ScrapCostMonthToDate()
//        {
//            return await _unitOfWork.MetricTypes.ScrapCostMonthToDate();
//        }

//        public async Task<decimal> ScrapCostPrevMonth()
//        {
//            return await _unitOfWork.MetricTypes.ScrapCostPrevMonth();
//        }

//        public async Task<decimal> ScrapCostYearToDate()
//        {
//            return await _unitOfWork.MetricTypes.ScrapCostYearToDate();
//        }

//        public async Task<int> ShippingDaysPackedAhead(List<TempCOModded> tempCOs)
//        {
//            return await _unitOfWork.MetricTypes.ShippingDaysPackedAhead(tempCOs);
//        }

//        public async Task<List<TransactionModel>> ShippingDaysPackedAheadList()
//        {
//            return await _unitOfWork.MetricTypes.ShippingDaysPackedAheadList();
//        }

//        public async Task<bool> UpdateMetricData(MetricsGagingDataViewModel data)
//        {
//            return await _unitOfWork.MetricTypes.UpdateMetricData(MetricsGagingDataDTONew.Map(data));
//        }

//        public async Task<bool> UpdateOverheadData(PlanningOverheadViewModel data)
//        {
//            return await _unitOfWork.MetricTypes.UpdateOverheadData(PlanningOverheadDTONew.Map(data));
//        }

//        public async Task<MetricsGoalsViewModel> InsertMetricsGoalAsync(MetricsGoalsViewModel metricsGoal)
//        {
//            var result = MetricsGoalsDTONew.Map(metricsGoal);
//            await _unitOfWork.Goals.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            metricsGoal.Id = result.Id;
//            return metricsGoal;
//        }

//        public async Task<bool> UpdateMetricsGoalAsync(MetricsGoalsViewModel metricsGoal)
//        {
//            var metricGoal = await _unitOfWork.Goals.GetAsync(metricsGoal.Id);

//            metricGoal.Id = metricsGoal.Id;
//            metricGoal.GoalDescription = metricsGoal.GoalDescription;
//            metricGoal.GoalItem = metricsGoal.GoalItem;
//            metricGoal.GoalAlert = metricsGoal.GoalAlert;
//            metricGoal.GoalCaution = metricsGoal.GoalCaution;
//            metricGoal.IsBelowAlert = metricsGoal.IsBelowAlert;
//            metricGoal.IsBelowCaution = metricsGoal.IsBelowCaution;
//            metricGoal.IsBelowGoal = metricsGoal.IsBelowGoal;

//            await _unitOfWork.CompleteAsync();

//            //await _unitOfWork.Goals.UpdateAsync(MetricsGoalsDTONew.Map(metricsGoal));
//            return true;
//        }

//        public async Task<IEnumerable<MetricsGoalsViewModel>> GetMetricGoalsAsync()
//        {
//            return MetricsGoalsDTONew.Map(await _unitOfWork.Goals.GetListAsync());
//        }

//        public async Task<bool> DeleteMetricsGoalAsync(MetricsGoalsViewModel metricsGoal)
//        {
//            var result = MetricsGoalsDTONew.Map(metricsGoal);
//            await _unitOfWork.Goals.RemoveAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return true;
//        }

//        public async Task<MetricsGoalsViewModel> GetMetricsGoalAsync(string GoalItem)
//        {
//            return MetricsGoalsDTONew.Map(await _unitOfWork.Goals.FindAsync(x => x.GoalItem == GoalItem.ToLower()));
//        }

//        public async Task<int> InsertMetricsFieldAsync(MetricsFieldsViewModel FieldModel)
//        {
//            var result = MetricsFieldsDTONew.Map(FieldModel);
//            await _unitOfWork.Fields.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return result.Id;
//        }

//        public async Task<MetricsFieldsViewModel> GetMetricsFieldAsync(int Id)
//        {
//            return MetricsFieldsDTONew.Map(await _unitOfWork.Fields.GetAsync(Id));
//        }

//        public async Task<IEnumerable<MetricsFieldsViewModel>> GetAllMetricsFieldAsync()
//        {
//            return MetricsFieldsDTONew.Map(await _unitOfWork.Fields.GetListAsync());
//        }

//        public async Task<MetricsFieldsViewModel> UpdateMetricsFieldAsync(MetricsFieldsViewModel FieldModel)
//        {
//            var field = await _unitOfWork.Fields.GetAsync(FieldModel.Id);

//            field.Id = FieldModel.Id;
//            field.DataName = FieldModel.DataName;
//            field.DataValue = FieldModel.DataValue;
//            field.GoalId = FieldModel.GoalId;
//            field.IsGoalRequired = FieldModel.IsGoalRequired;
//            field.IsMetrics = FieldModel.IsMetrics;
//            await _unitOfWork.CompleteAsync();

//            //await _unitOfWork.Fields.UpdateAsync(MetricsFieldsDTONew.Map(FieldModel));
//            return FieldModel;
//        }
//    }
//}
