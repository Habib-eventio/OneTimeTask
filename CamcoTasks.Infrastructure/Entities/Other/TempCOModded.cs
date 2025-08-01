using System;

namespace CamcoTasks.Infrastructure.Entities;

public class TempCOModded
{
    public string CustomerOrderNumber { get; set; }

    public double OrderQty { get; set; }

    public double SumOfStockQty { get; set; }

    public string Customer { get; set; }

    public string PartNumber { get; set; }

    public DateTime DueDate { get; set; }
}