namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum InsuranceType
{
    [CustomDisplay("Dental")] Dental = 1,

    [CustomDisplay("Group Life")] GroupLife = 2,

    [CustomDisplay("Health")] Health = 3,

    [CustomDisplay("Vision")] Vision = 4,

    [CustomDisplay("VOL Life")] VolLife = 5,

    [CustomDisplay("VOL STD")] VolStd = 6,

    [CustomDisplay("Accident")] Accident = 7,

    [CustomDisplay("Critical Illness")] CriticalIllness = 8
}