using System;

namespace ERP.Data.CustomModels.Other;

public class SummaryReportDetails
{
    public int ActivityId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime DateCreated { get; set; }

    public int TimeSpent { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}