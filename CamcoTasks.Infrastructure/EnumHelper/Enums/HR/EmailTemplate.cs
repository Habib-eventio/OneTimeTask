namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum EmailTemplate
{
    [CustomDisplay("PerformanceImprovementPlan")]
    PerformanceImprovementPlan,

    [CustomDisplay("PerformanceImprovementPlanFollowUps")]
    PerformanceImprovementPlanFollowUps,

    [CustomDisplay("PayrollList")] PayrollList,

    [CustomDisplay("PerformanceReview")] PerformanceReview,

    [CustomDisplay("ExceptionEmail")] ExceptionEmail,

    [CustomDisplay("AddDiscipline")] AddDiscipline,

    [CustomDisplay("DisciplineReduction")] DisciplineReduction,

    [CustomDisplay("DrugTest")] DrugTest,

    [CustomDisplay("EmployeeTerminationSeparation")]
    EmployeeTerminationSeparation,

    [CustomDisplay("SubmitPayroll")] SubmitPayroll,

    [CustomDisplay("DrugTestSkipped")] DrugTestSkipped,

    [CustomDisplay("EmploymentStatusChanged")]
    EmploymentStatusChanged,

    [CustomDisplay("DrugTestScheduled")] DrugTestScheduled,

    [CustomDisplay("TrainingAndCertificationRequest")]
    TrainingAndCertificationRequest,

    [CustomDisplay("EmployeeProfileChanges")]
    EmployeeProfileChanges,

    [CustomDisplay("PasswordChangedEmail")]
    PasswordChangedEmail,

    [CustomDisplay("EmployeeChangeRequestApproval")]
    EmployeeChangeRequestApproval
}