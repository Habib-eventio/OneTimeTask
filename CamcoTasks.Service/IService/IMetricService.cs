//using ERP.Data.CustomModels.Other;
//using CamcoTasks.ViewModels.MaintenanceWorkOrderDataDTO;
//using CamcoTasks.ViewModels.MetricsFieldsDTO;
//using CamcoTasks.ViewModels.MetricsGagingDataDTO;
//using CamcoTasks.ViewModels.MetricsGoalsDTO;
//using CamcoTasks.ViewModels.PlanningOverheadDTO;
//using CamcoTasks.ViewModels.PurchasingPurchaseRequestsDTO;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.IService
//{
//    public interface IMetricService
//    {
//        Task<List<TempCOModded>> GetTopTwentyUnpackedCOs();
//        Task<List<TempCOModded>> GetTopFiftyUnpackedCOs();
//        Task<int> ShippingDaysPackedAhead(List<TempCOModded> tempCOs);
//        Task<List<TransactionModel>> ShippingDaysPackedAheadList();
//        Task<int> RMAYearToDate();
//        Task<List<RmaCustom>> GetTotalRMA();
//        Task<List<RmaCustom>> GetTotalRMA2();
//        Task<decimal> ScrapCostMonthToDate();
//        Task<decimal> ScrapCostPrevMonth();
//        Task<decimal> ScrapCostYearToDate();
//        Task<int> RMAPrevYear();
//        Task<int> GetOpenPOCount();
//        Task<IEnumerable<PurchasingPurchaseRequestsViewModel>> GetOpenPO();
//        Task<List<POsTotals>> GetPORequestsTotals();
//        Task<List<TempShopDailyOutput>> GetFinalDailyShopOutput();
//        Task<List<WeeklyInvoice>> GetWeeklyInvoiceAsync();
//        Task<double> GetTotalInventoryCost();
//        Task<double> GetTotalPkgInventoryCost();
//        Task<double> GetWIPTotalCost();
//        Task<int> GetOpenShopOrderCount();
//        Task<decimal> GetPrevWeekInvoiceTotal();
//        Task<decimal> GetPrevWeekShipmentsTotal();
//        Task<decimal> GetTotalOpenWorkOrders();
//        Task<IEnumerable<MaintenanceWorkOrderDataViewModel>> GetMaintenanceOpenTickets();
//        Task<List<MaintenanceOpenTicketDates>> GetCustomMaintenanceOpenTickets();
//        Task<IEnumerable<MaintenanceWorkOrderDataViewModel>> GetCustomMaintenanceOpenTicketsByDate(DateTime actualdate);
//        Task<IEnumerable<MaintenanceWorkOrderDataViewModel>> GetMaintenancePriorityOneByDate(DateTime actualdate);
//        Task<decimal> GetTotalTopPriorityWorkOrders();
//        Task<IEnumerable<MetricsGagingDataViewModel>> GetAllGageData();
//        Task<List<MetricsDataCustom>> GetAllCustomGageData();
//        Task<IEnumerable<PlanningOverheadViewModel>> GetAllOverheadData();
//        Task<List<MetricsHistoryCustomData>> GetMetricsHistory();
//        Task<List<MetricsHistoryCustomData>> GetMetricsHistoryDaysPackedAhead();
//        Task<List<CustomOverHeads>> GetAllCustomOverheadData();
//        Task<bool> AddOverheadData(PlanningOverheadViewModel data);
//        Task<bool> UpdateOverheadData(PlanningOverheadViewModel data);
//        Task<bool> AddMetricData(MetricsGagingDataViewModel data);
//        Task<bool> UpdateMetricData(MetricsGagingDataViewModel data);
//        Task<IEnumerable<PurchasingPurchaseRequestsViewModel>> GetPOReqData();
//        Task<List<TempOverweightBoxModel>> GetOverweightBoxes();

//        Task<MetricsGoalsViewModel> InsertMetricsGoalAsync(MetricsGoalsViewModel metricsGoal);
//        Task<MetricsGoalsViewModel> GetMetricsGoalAsync(string GoalItem);
//        Task<IEnumerable<MetricsGoalsViewModel>> GetMetricGoalsAsync();
//        Task<bool> UpdateMetricsGoalAsync(MetricsGoalsViewModel metricsGoal);
//        Task<bool> DeleteMetricsGoalAsync(MetricsGoalsViewModel metricsGoal);

//        Task<int> InsertMetricsFieldAsync(MetricsFieldsViewModel FieldModel);
//        Task<MetricsFieldsViewModel> GetMetricsFieldAsync(int Id);
//        Task<IEnumerable<MetricsFieldsViewModel>> GetAllMetricsFieldAsync();
//        Task<MetricsFieldsViewModel> UpdateMetricsFieldAsync(MetricsFieldsViewModel FieldModel);
//    }
//}
