namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum OfficialDocumentType
{
    [CustomDisplay("Discipline")] Discipline = 1,

    [CustomDisplay("Termination")] Termination = 2,

    [CustomDisplay("Policies")] Policies = 3,

    [CustomDisplay("E-Sides")] ESides = 4,

    [CustomDisplay("Statements")] Statements = 5,

    [CustomDisplay("Misc")] Misc = 6
}