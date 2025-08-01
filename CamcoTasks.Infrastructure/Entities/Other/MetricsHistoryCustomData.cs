using System;

namespace CamcoTasks.Infrastructure.CustomModels.Other;

public class MetricsHistoryCustomData
{
    public DateTime ActualDate { get; set; }

    public string Date { get; set; }

    public decimal DaysPackedAhead { get; set; }

    public decimal SixDaysPackedAhead { get; set; }

    public decimal ShopOutputTotal { get; set; }

    public decimal ShopOutputExpenses { get; set; }

    public double InventoryTotal { get; set; }

    public double PkgInventoryTotal { get; set; }

    public double WIPTotal { get; set; }

    public string Year { get; set; }

    public decimal ShopOutputProfitLoss { get; set; }
}