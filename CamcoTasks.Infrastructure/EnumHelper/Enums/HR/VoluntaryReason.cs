namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum VoluntaryReason
{
    [CustomDisplay("Resigned With Notice")]
    ResignedWithNotice = 1,

    [CustomDisplay("Retired")] Retired = 2,

    [CustomDisplay("Resigned Without Notice")]
    ResignedWithoutNotice = 3,

    [CustomDisplay("No Call, No Show")] NoCallNoShow = 4,

    [CustomDisplay("Personal Reasons")] PersonalReasons = 5,

    [CustomDisplay("Other")] Other = 6
}