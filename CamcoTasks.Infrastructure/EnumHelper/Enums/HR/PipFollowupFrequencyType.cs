namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum PipFollowupFrequencyType
{
    [CustomDisplay("Daily")] Daily = 1,

    [CustomDisplay("Weekly")] Weekly = 2,

    [CustomDisplay("TwicePerWeekly")] TwicePerWeekly = 3,

    [CustomDisplay("TwicePerBiMonthly")] TwicePerBiMonthly = 4,

    [CustomDisplay("Monthly")] Monthly = 5,

    [CustomDisplay("SixMonths")] SixMonths = 6,

    [CustomDisplay("EightMonths")] EightMonths = 7,

    [CustomDisplay("Yearly")] Yearly = 8,

    [CustomDisplay("TwoMonths")] TwoMonths = 9,

    [CustomDisplay("TwoWeeks")] TwoWeeks = 10
}