using System;

namespace CamcoTasks.Infrastructure.CustomModels.Other;

public class TempOverweightBoxModel
{
    public string CustomerOrderNumber { get; set; }

    public DateTime ShipmentClosedDate { get; set; }

    public string WhoPacked { get; set; }

    public string Customer { get; set; }

    public string PartNumber { get; set; }

    public double ExpectedWeight { get; set; }

    public double ActualWeight { get; set; }

    public double PartWeight { get; set; }

    public double BoxWeight { get; set; }

    public DateTime TransactionDate { get; set; }

    public int PackQty { get; set; }

    public double PercentOff { get; set; }
}