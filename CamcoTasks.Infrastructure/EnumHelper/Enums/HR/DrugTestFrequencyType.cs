namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum DrugTestFrequencyType
{
    [CustomDisplay("Pre Employment")] PreEmployment = 1,

    [CustomDisplay("Yearly Random")] YearlyRandom = 2,

    [CustomDisplay("Quarterly Random")] QuarterlyRandom = 3,

    [CustomDisplay("Monthly Random")] MonthlyRandom = 4,

    [CustomDisplay("Weekly Random")] WeeklyRandom = 5,

    [CustomDisplay("Twice A Week Random")] TwiceAWeekRandom = 6,

    [CustomDisplay("Drug Test Not Needed")]
    DrugTestNotNeeded = 7,

    [CustomDisplay("Six Month Random")] SixMonthsRandom = 8,

    [CustomDisplay("2 Years Random")] TwoYearsRandom = 9,

    [CustomDisplay("5 Years Random")] FiveYearsRandom = 10,

    [CustomDisplay("Thrice A Week Random")]
    ThriceAWeekRandom = 11
}