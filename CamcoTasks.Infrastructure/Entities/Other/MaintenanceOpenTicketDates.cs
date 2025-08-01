using System;

namespace   CamcoTasks.Infrastructure.CustomModels.Other;

public class MaintenanceOpenTicketDates
{
    public int? TicketCount { get; set; }

    public string Date { get; set; }

    public DateTime ActualDate { get; set; }

    public string Year { get; set; }

    public int Priority { get; set; }
}