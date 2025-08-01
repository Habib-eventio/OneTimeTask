// This File Needs to be reviewed Still. Don't Remove this comment.

using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpMinutesProducedPerHourResultModel
{
    public IList<SpMinutesProducedPerHourListModel> MinutesProducedPerHours { get; set; }
}

public class SpMinutesProducedPerHourListModel
{
    public long EmployeeId { get; set; }

    public string EmployeeName { get; set; }

    public decimal MinsProdPerProdHours { get; set; }
}