namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum DrugTestReasonType
{
    [CustomDisplay("Accident")] Accident = 1,

    [CustomDisplay("Frequency")] Frequency = 2,

    [CustomDisplay("Pre-Employment")] PreEmployment = 3,

    [CustomDisplay("Suspicion")] Suspicion = 4
}