namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum HoursChangeType
{
    [CustomDisplay("Adjusted Down")] AdjustedDown = -2,

    [CustomDisplay("Used")] AvailedUsed = -1,

    [CustomDisplay("Earned")] Earned0 = 0,

    [CustomDisplay("Earned")] Earned1 = 1,

    [CustomDisplay("Adjusted Up")] AdjustedUp = 2
}