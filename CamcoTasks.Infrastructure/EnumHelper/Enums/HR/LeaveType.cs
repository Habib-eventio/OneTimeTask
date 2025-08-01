namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum LeaveType
{
    [CustomDisplay("FMLA/Intermittent")] FmlaOrIntermittent = 1,

    [CustomDisplay("FMLA/Continuous")] FmlaOrContinuous = 2,

    [CustomDisplay("FMLA/ReduceSchedule")] FmlaOrReduceSchedule = 3,

    [CustomDisplay("NON-FMLA/ExcusedMedical")]
    NonFmlaOrExcusedMedical = 4,

    [CustomDisplay("NON-FMLA/Personal")] NonFmlaOrPersonal = 5,

    [CustomDisplay("NON-FMLA/Disability")] NonFmlaOrDisability = 6,
}