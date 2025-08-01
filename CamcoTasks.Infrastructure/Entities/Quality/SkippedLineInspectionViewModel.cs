using System;

namespace ERP.Data.CustomModels.Quality
{
    public class SkippedLineInspectionViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long EmployeeId { get; set; }
        public int LineInspectionId { get; set; }
        public int? PartRunningId { get; set; }
        public string CustomEmployeeId { get; set; }
        public int ShopOrderId { get; set; }
        public string EmployeeName { get; set; }
        public string ShopOrderNumber { get; set; }
        public string PartNumber { get; set; }
        public string Machine { get; set; }
    }
}
