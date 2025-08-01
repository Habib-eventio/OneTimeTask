// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.Collections.Generic;

namespace ERP.Data.StoreProcedures;

public class SpProductionSheetsDataModel
{
    public IList<SpProductionTimeSheetModel> ProductionTimeSheets { get; set; }

    public IList<SpPartNumberModel> PartNumbers { get; set; }

    public IList<SpShopNumberModel> ShopNumbers { get; set; }

    public IList<SpOperationNumberModel> OperationNumbers { get; set; }

    public IList<SpEmployeesProductionTimeSheetModel> Employees { get; set; }

    public IList<SpCx4211EShopOrderNumbersModel> Cx4211EShopOrderNumbers { get; set; }

}

public class SpProductionTimeSheetModel
{
    public int Id { get; set; }

    public string EmployeeId { get; set; }

    public string EmployeeName { get; set; }

    public DateTime Date { get; set; }

    public string CreatedBy { get; set; }

    public string Path { get; set; }

}

public class SpPartNumberModel
{
    public string PartNumber { get; set; }

    public string So { get; set; }
}

public class SpShopNumberModel
{
    public string So { get; set; }
}

public class SpOperationNumberModel
{
    public string RmPa { get; set; }

    public string RmOp { get; set; }

}

public class SpEmployeesProductionTimeSheetModel
{
    public long EmpId { get; set; }

    public string FullName { get; set; }

}

public class SpCx4211EShopOrderNumbersModel
{
    public int SoNumber { get; set; }

    public string SoDescription { get; set; }
}