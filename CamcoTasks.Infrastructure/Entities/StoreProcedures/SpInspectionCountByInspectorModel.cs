// This File Needs to be reviewed Still. Don't Remove this comment.

using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpInspectionCountByInspectorModel
{
    public IList<SpInspectionsCountByInspectorReportModel> InspectionsCountByInspectorReport { get; set; }
}

public class SpInspectionsCountByInspectorReportModel
{
    public string InspectorId { get; set; }

    public string InspectorName { get; set; }

    public int Last7Days { get; set; }

    public int Last1Month { get; set; }

    public int Last3Months { get; set; }

    public int Last1Year { get; set; }
}