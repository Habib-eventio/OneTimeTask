using System;

namespace CamcoTasks.ViewModels.PlanningCustomerOrdersCurrentDTO
{
    public class PlanningCustomerOrdersCurrentViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string CONumber { get; set; }
        public string Customer { get; set; }
        public string PartNumber { get; set; }
        public string Po { get; set; }
        public double? TotalPo { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string Notes { get; set; }
        public int? LineNumber { get; set; }
        public string ShipCust { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? OverrideShipDate { get; set; }
        public string OverrideShipMethod { get; set; }
    }
}
