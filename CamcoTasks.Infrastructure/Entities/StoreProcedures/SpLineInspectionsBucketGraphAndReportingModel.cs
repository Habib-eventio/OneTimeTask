// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpLineInspectionsDataModel
{
    public IList<SpBucketsReportModel> BucketsReport { get; set; }

    public IList<SpBucketDetailReportModel> BucketDetailsReport { get; set; }

    public IList<SpGraphReportModel> GraphReport { get; set; }

    public IList<SpGeneralInformationModel> GeneralInformation { get; set; }
}

public class SpGeneralInformationModel
{
    public int TotalInspections { get; set; }

    public int TotalIncompleteInspections { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public int GoalValue { get; set; }

    public int PassPercentageAverage30Days { get; set; }

    public int PassPercentageAverage3Months { get; set; }

    public int PassPercentageAverage1Year { get; set; }
}

public class SpBucketsReportModel
{
    public string OperatorId { get; set; }

    public string OperatorName { get; set; }

    public int TotalInspectionsLast30Days { get; set; }

    public int PassPercentageLast30Days { get; set; }

    public int TotalInspectionsLast3Months { get; set; }

    public int PassPercentageLast3Months { get; set; }

    public int TotalInspectionsLast1Year { get; set; }

    public int PassPercentageLast1Year { get; set; }
}

public class SpBucketDetailReportModel
{
    public int InspectionAuditId { get; set; }

    public string OperatorId { get; set; }

    public string OperatorName { get; set; }

    public DateTime AuditDate { get; set; }

    public string InspectorName { get; set; }

    public string Status { get; set; }

    public string Path { get; set; }

    public string EnteredByName { get; set; }

    public string DataState { get; set; }
}

public class SpGraphReportModel
{
    public DateTime X { get; set; }

    public int Y { get; set; }
}