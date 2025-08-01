namespace CamcoTasks.ViewModels.ToolcribBoxesDTO
{
    public class ToolcribBoxesViewModel
    {
        public int BoxId { get; set; }
        public string Qrn { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Vendor { get; set; }
        public int? VenNum { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public byte[] SsmaTimeStamp { get; set; }
    }
}
