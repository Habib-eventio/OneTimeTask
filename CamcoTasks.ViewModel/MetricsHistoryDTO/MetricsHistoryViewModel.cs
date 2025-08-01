using System;

namespace CamcoTasks.ViewModels.MetricsHistoryDTO
{
    public class MetricsHistoryViewModel
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public decimal DaysPackedAhead { get; set; }
		public decimal ShopOutputTotal { get; set; }
		public decimal ShopOutputExpenses { get; set; }
		public decimal ShopOutputProfetLoss { get; set; }
		public double InventoryTotal { get; set; }
		public double PkgInventoryTotal { get; set; }
		public double WIPTotal { get; set; }
        public decimal ShopOutputProfitLoss { get; internal set; }
    }
}
