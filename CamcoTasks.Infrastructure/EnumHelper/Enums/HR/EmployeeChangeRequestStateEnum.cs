namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum EmployeeChangeRequestStateEnum
{
    [CustomDisplay("New")] New = 1,

    [CustomDisplay("Approval Required")] ApprovalRequired = 2,

    [CustomDisplay("Rejected")] Rejected = 3,

    [CustomDisplay("Cancel")] Cancel = 4,

    [CustomDisplay("Overdue - Not Approved")]
    OverdueNotApproved = 5,

    [CustomDisplay("Approved")] Approved = 6
}