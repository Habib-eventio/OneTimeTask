// This File Needs to be reviewed Still. Don't Remove this comment.

using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpMinProdDetailModel
{
    public IList<SpMinProdModel> MinProdViewModel { get; set; }
}

public class SpMinProdModel
{
    public long EmployeeId { get; set; }

    public string EmployeeName { get; set; }

    public decimal MinsProdPerProdHours { get; set; }
}