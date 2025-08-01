using System;

namespace CamcoTasks.Infrastructure.CustomModels.Surveillance;

public class BathroomAuditParams
{
    public DateTime AuditDate { get; set; }

    public long? EmployeeId { get; set; }

    public int BreakTypeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}