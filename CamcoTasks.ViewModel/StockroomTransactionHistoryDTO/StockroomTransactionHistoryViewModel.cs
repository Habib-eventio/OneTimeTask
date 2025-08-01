using System;

namespace CamcoTasks.ViewModels.StockroomTransactionHistoryDTO
{
    public class StockroomTransactionHistoryViewModel
    {
        public int TransId { get; set; }
        public string TransactionType { get; set; }
        public string PartNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? QuantityMoved { get; set; }
        public string CompletedBy { get; set; }
        public string Conumber { get; set; }
        public string Location { get; set; }
        public string Sonumber { get; set; }
        public string Note { get; set; }
        public string PackType { get; set; }
        public string PalId { get; set; }
        public bool? IsPartial { get; set; }
        public int? Ponum1 { get; set; }
        public int OldStockQty { get; set; }
        public int NewStockQty { get; set; }
        public byte[] SsmaTimeStamp { get; set; }
        public string Ponum { get; set; }
        public int? ItemId { get; set; }
    }
}
