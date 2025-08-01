// This File Needs to be reviewed Still. Don't Remove this comment.

using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpGetErrorsTrackingByInspectorModel
{
    public IList<SpErrorsTrackingByInspectorReportModel> ErrorsTrackingByInspectorReport { get; set; }

}

public class SpErrorsTrackingByInspectorReportModel
{
    public string InspectorId { get; set; }

    public string InspectorName { get; set; }

    public decimal TotalErrorsPercentage { get; set; }

    public int Last90DaysInspectionErrorCounts { get; set; }

    public int Total90DaysInspectionCounts { get; set; }

    public int TotalInspectionErrorCounts { get; set; }

    public int TotalInspectionCounts { get; set; }
}