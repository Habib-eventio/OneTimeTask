namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum DrugTestRequestReasons
{
    [CustomDisplay("PreEmployment")] PreEmployment = 0,

    [CustomDisplay("InjuryMishap")] InjuryMishap = 1,

    [CustomDisplay("RequestedByDepartmentHead")]
    RequestedByDepartmentHead = 2,

    [CustomDisplay("Other")] Other = 3
}