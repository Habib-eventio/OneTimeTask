namespace ERP.Data.Entities.Production;

public class TimeSheetDatum
{
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    public double? BurdenTime { get; set; }
}
