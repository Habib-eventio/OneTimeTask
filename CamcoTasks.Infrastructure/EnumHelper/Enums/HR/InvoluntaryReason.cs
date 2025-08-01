namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum InvoluntaryReason
{
    [CustomDisplay("Failure To Perform Job Duties")]
    FailureToPerformJobDuties = 7,

    [CustomDisplay("Laid Off")] LaidOff = 8,

    [CustomDisplay("Positive Drug Screen")]
    PositiveDrugScreen = 9,

    [CustomDisplay("Replaced / Not a Good Fit")]
    ReplacedNotAGoodFit = 10,

    [CustomDisplay("Theft (Items and/or Time)")]
    TheftItemsAndOrTime = 11,

    [CustomDisplay("Excessive Absenteeism/Tardiness")]
    ExcessiveAbsenteeismTardiness = 12,
}