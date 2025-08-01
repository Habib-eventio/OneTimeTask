namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum EmploymentType
{
    [CustomDisplay("Permanent")] Permanent = 1,

    [CustomDisplay("Temporary")] Temporary = 2,

    [CustomDisplay("SubContractor - UpWork")]
    SubContractorUpWork = 3
}