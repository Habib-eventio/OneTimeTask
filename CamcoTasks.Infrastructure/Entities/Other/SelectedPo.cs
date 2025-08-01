using System;

namespace CamcoTasks.Infrastructure.CustomModels.Other;

public class SelectedPo
{
    public string Status { get; set; }

    public DateTime? DateRequested { get; set; }

    public string Description { get; set; }

    public int PrNumber { get; set; }

    public string PoNumber { get; set; }
}