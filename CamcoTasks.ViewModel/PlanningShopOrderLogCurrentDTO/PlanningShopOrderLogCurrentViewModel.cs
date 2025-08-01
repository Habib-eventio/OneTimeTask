using System;

namespace CamcoTasks.ViewModels.PlanningShopOrderLogCurrentDTO
{
    public class PlanningShopOrderLogCurrentViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Customer { get; set; }
        public double? QuantityToStock { get; set; }
        public DateTime? ToStockDate { get; set; }
        public string Notes { get; set; }
        public DateTime? DateClosed { get; set; }

        public string EnteredBy { get; set; }
    }
}
