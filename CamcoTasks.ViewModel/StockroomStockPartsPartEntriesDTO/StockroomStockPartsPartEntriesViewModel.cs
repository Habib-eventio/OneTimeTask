using System;

namespace CamcoTasks.ViewModels.StockroomStockPartsPartEntriesDTO
{
    public class StockroomStockPartsPartEntriesViewModel
    {
        public int PartLocId { get; set; }
        public int? PackInstId { get; set; }
        public string PartNumber { get; set; }
        public double StockQty { get; set; }
        public double PkgQty { get; set; }
        public string Location { get; set; }
        public decimal Cost { get; set; }
        public DateTime? LastAudit { get; set; }
        public byte[] SsmaTimeStamp { get; set; }
        public double? DefaultStock { get; set; }
    }
}
