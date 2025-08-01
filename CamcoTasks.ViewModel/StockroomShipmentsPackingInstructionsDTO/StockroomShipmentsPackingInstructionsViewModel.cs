using System;

namespace CamcoTasks.ViewModels.StockroomShipmentsPackingInstructionsDTO
{
    public class StockroomShipmentsPackingInstructionsViewModel
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public string Customer { get; set; }
        public string PartNumber { get; set; }
        public string BoxSize { get; set; }
        public int Qty { get; set; }
        public string Instruct { get; set; }
        public string Comment { get; set; }
        public string PicLink { get; set; }
        public string BagType { get; set; }
        public int? StackHeight { get; set; }
        public string AdditionalDocs { get; set; }
        public string CrumpledPaper { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public byte[] SsmaTimeStamp { get; set; }
        public string ProdCreatedBy { get; set; }
    }
}
