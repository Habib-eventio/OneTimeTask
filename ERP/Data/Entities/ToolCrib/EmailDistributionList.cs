namespace ERP.Data.Entities.ToolCrib;

public class EmailDistributionList
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public bool IsActive { get; set; }
    public int? EmailOptionId { get; set; }
}
