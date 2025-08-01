namespace ERP.Data.CustomModels.Quality;

public class FlaggedCheckSheetViewModel
{
    public int CheckSheetId { get; set; }
    public string ActualDimensionFound { get; set; }
    public string FlagOption { get; set; }
    public string OperationNumber { get; set; }
    public string PartNumber { get; set; }
    public string DrawingNumber { get; set; }
    public string FlaggedField { get; set; }
    public int FlagId { get; set; }
    public string FieldValue { get; set; }
    public int? FlagOptionId { get; set; }
    public int? BubbleNumber { get; set; }
    public string FeatureType { get; set; }
    public int CharacteristicId { get; set; }
    public string CharacteristicField { get; set; }
}