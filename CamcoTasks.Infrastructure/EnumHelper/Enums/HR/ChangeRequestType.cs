using CamcoTasks.Infrastructure.EnumHelper;

namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum ChangeRequestType
{
    [CustomDisplay("EmploymentStatus")] EmploymentStatus = 1,

    [CustomDisplay("PaymentChangeRequest")]
    PaymentChangeRequest = 2,

    [CustomDisplay("PayRate")] PayRate = 3,

    [CustomDisplay("PaymentType")] PaymentType = 4,

    [CustomDisplay("TierLevel")] TierLevel = 5,

    [CustomDisplay("JobTitle")] JobTitle = 6,

    [CustomDisplay("Department")] Department = 7,

    [CustomDisplay("ShiftStartEnd")] ShiftStartEnd = 8,

    [CustomDisplay("ShitDifferential")] ShitDifferential = 9,

    [CustomDisplay("Manager")] Manager = 10,

    [CustomDisplay("Leader")] Leader = 11,

    [CustomDisplay("JobStatus")] JobStatus = 12,

    [CustomDisplay("ExpectedMinimumHours")]
    ExpectedMinimumHours = 13,

    [CustomDisplay("PercentOfDirectTime")] PercentOfDirectTime = 14,
}