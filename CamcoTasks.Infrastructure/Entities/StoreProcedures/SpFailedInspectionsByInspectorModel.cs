// This File Needs to be reviewed Still. Don't Remove this comment.

using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpFailedInspectionsByInspectorModel
{
    public IList<SpFailedInspectionsByInspectorReportModel> FailedInspectionsByInspectorReport { get; set; }
}

public class SpFailedInspectionsByInspectorReportModel
{
    public string InspectorId { get; set; }

    public string InspectorName { get; set; }

    public int TotalFailedInspectionsLast30Days { get; set; }

    public int FailPercentageLast30Days { get; set; }

    public int TotalInspectionsLast30Days { get; set; }

    public int TotalFailedInspectionsLast3Months { get; set; }

    public int FailPercentageLast3Months { get; set; }

    public int TotalInspectionsLast3Months { get; set; }

    public int TotalFailedInspectionsLast1Year { get; set; }

    public int FailPercentageLast1Year { get; set; }

    public int TotalInspectionsLast1Year { get; set; }
}