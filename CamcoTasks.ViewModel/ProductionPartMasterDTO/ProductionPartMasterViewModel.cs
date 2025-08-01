namespace CamcoTasks.ViewModels.ProductionPartMasterDTO
{
    public class ProductionPartMasterViewModel
    {
        public string PmPart { get; set; }
        public string EngPrintRev { get; set; }
        public string PmDesc { get; set; }
        public string PmCustCode { get; set; }
        public string PmPartLength { get; set; }
        public decimal? PmPartPrice { get; set; }
        public bool PmKanban { get; set; }
        public string PmTerms { get; set; }
        public string PmOilfreq { get; set; }
        public string PmShippingmethod { get; set; }
        public string PmCoordinator { get; set; }
        public int? PmCustCode2 { get; set; }
        public int? PmCustCode3 { get; set; }
        public int? PmGroup { get; set; }
        public double? PmLeadtime { get; set; }
        public string PmRevlevel { get; set; }
    }
}
