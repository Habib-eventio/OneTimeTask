namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum AutomaticEmailFrequency
{
    [CustomDisplay("Daily")] Daily = 1,

    [CustomDisplay("Monthly")] Monthly = 2,

    [CustomDisplay("BiWeekly")] BiWeekly = 3,

    [CustomDisplay("BiMonthly")] BiMonthly = 4,

    [CustomDisplay("Weekly")] Weekly = 5
}