namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum DisciplinePhaseType
{
    [CustomDisplay("Disciplinary Phase 0 - No Disciplines")]
    DisciplinaryPhase0 = 1,

    [CustomDisplay("Disciplinary Phase I - Coaching Session Note")]
    DisciplinaryPhase1 = 2,

    [CustomDisplay("Disciplinary Phase II - Verbal Warning")]
    DisciplinaryPhase2 = 3,

    [CustomDisplay("Disciplinary Phase III - Written Warning")]
    DisciplinaryPhase3 = 4,

    [CustomDisplay("Disciplinary Phase IV - 3 Day Suspension Without Pay")]
    DisciplinaryPhase4 = 5,

    [CustomDisplay("Disciplinary Phase V - Termination")]
    DisciplinaryPhase5 = 6
}