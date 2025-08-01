using System;
using System.Collections.Generic;

namespace CamcoTasks.Infrastructure.Entities;

public class RmaCustom
{
    public int Count { get; set; }

    public decimal Average { get; set; }

    public DateTime ActualDate { get; set; }

    public string Date { get; set; }

    public string Year { get; set; }

    public List<SelectedRMAs> SelectedRMAs { get; set; }
}