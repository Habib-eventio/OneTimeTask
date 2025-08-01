using System;

namespace ERP.Data.CustomModels.Other;

public class ActivityParams
{
    public int PageSize { get; set; } = 20;

    public int PageNumber { get; set; } = 1;

    public string SortColumnName { get; set; }

    public string SortDirection { get; set; } = "asc";

    public int EmployeeId { get; set; }

    public int ObserverId { get; set; }

    public int ReportTypeId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateStarted { get; set; }

    public DateTime? DateCompleted { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public bool? IsApproved { get; set; }

    public bool? IsCompleted { get; set; }
}