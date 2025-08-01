namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum CertificationOrigin
{
    [CustomDisplay("Camco")] Camco = 1,

    [CustomDisplay("Medical")] Medical = 2,

    [CustomDisplay("School")] School = 3,

    [CustomDisplay("Other")] Other = 4
}