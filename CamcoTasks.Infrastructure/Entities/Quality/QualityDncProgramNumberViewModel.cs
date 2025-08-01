using System;

namespace ERP.Data.CustomModels.Quality;

public class QualityDncProgramNumberViewModel
{
    public int Id { get; set; }
    public string ProgramNumber { get; set; }
    public string OperationNumber { get; set; }
    public string PartNumber { get; set; }
    public string ProgramType { get; set; }
    public long? EnteredById { get; set; }
    public string EnteredByName { get; set; }
    public string Customer { get; set; }
    public bool IsNewRecord { get; set; }
    public string ClassOfCmm { get; set; }
    public string Description { get; set; }
    public string GageId { get; set; }
    public DateTime DateEntered { get; set; }
    public string EngineeringRevision { get; set; }
}