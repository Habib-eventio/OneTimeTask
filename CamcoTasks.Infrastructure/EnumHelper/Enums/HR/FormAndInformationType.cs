namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;



public enum FormAndInformationType
{
    [CustomDisplay("FMLA")] FMLA = 1,

    [CustomDisplay("Labeled Worker's Compensation")]
    LabeledWorkersCompensation = 2,

    [CustomDisplay("Minor-Aged Employees")]
    MinorAgedEmployees = 3,

    [CustomDisplay("Employee Insurance Benefits")]
    EmployeeInsuranceBenefits = 4,

    [CustomDisplay("Interview And New Hire Forms")]
    InterviewAndNewHireForms = 5,

    [CustomDisplay("Employee Profile Changes")]
    EmployeeProfileChanges = 6,

    [CustomDisplay("Employee Management Forms")]
    EmployeeManagementForms = 7,

    [CustomDisplay("Employee Handbook And Policies")]
    EmployeeHandbookAndPolicies = 8
}