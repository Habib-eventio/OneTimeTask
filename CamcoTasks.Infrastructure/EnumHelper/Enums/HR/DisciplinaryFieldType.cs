namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum DisciplinaryFieldType
{
    [CustomDisplay("EmployeeReceivingDisciplineId")]
    EmployeeReceivingDisciplineId = 1,

    [CustomDisplay("IncidentDate")] IncidentDate = 2,

    [CustomDisplay("NatureOfViolationId")] NatureOfViolationId = 3,

    [CustomDisplay("DisciplineLevelId")] DisciplineLevelId = 4,

    [CustomDisplay("DisciplineLevelReductionDate")]
    DisciplineLevelReductionDate = 5,

    [CustomDisplay("IncidentDescription")] IncidentDescription = 6,

    [CustomDisplay("CorrectiveAction")] CorrectiveAction = 7,

    [CustomDisplay("SignedDisciplineNoticeDocument")]
    SignedDisciplineNoticeDocument = 8,

    [CustomDisplay("CreatedById")] CreatedById = 9,

    [CustomDisplay("DateEntered")] DateEntered = 10,

    [CustomDisplay("CurrentDisciplineLevelId")]
    CurrentDisciplineLevelId = 11,

    [CustomDisplay("JobTitleId")] JobTitleId = 12,
}