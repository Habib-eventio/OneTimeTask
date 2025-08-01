using System;
using System.Collections.Generic;

namespace CamcoTasks.Infrastructure.CustomModels.Other;

public class POsTotals
{
    public string Date { get; set; }

    public int Count { get; set; }

    public string Year { get; set; }

    public DateTime ActualDate { get; set; }

    public List<SelectedPo> PoList { get; set; }
}