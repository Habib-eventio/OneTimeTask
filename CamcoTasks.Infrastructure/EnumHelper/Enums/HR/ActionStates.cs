namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;
public enum ActionStates
{
    [CustomDisplay("InternalFailure")] InternalFailure,

    [CustomDisplay("UpdateSuccess")] UpdateSuccess,

    [CustomDisplay("AddSuccess")] AddSuccess,

    [CustomDisplay("DeleteSuccess")] DeleteSuccess,

    [CustomDisplay("DuplicateRecord")] DuplicateRecord,

    [CustomDisplay("FormInvalid")] FormInvalid,

    [CustomDisplay("NoRecordFound")] NoRecordFound,

    [CustomDisplay("DepartmentManagerSelection")]
    DepartmentManagerSelection,

    [CustomDisplay("PasswordChanged")] PasswordChanged,

    [CustomDisplay("DuplicatePassword")] DuplicatePassword,

    [CustomDisplay("PasswordNotMatched")] PasswordNotMatched,

    [CustomDisplay("EmployeeActive")] EmployeeActive,

    [CustomDisplay("EmployeeInActive")] EmployeeInActive,

    [CustomDisplay("InCorrectPassword")] InCorrectPassword,

    [CustomDisplay("DuplicateUsername")] DuplicateUsername,

    [CustomDisplay("RestoreSuccess")] RestoreSuccess,

    [CustomDisplay("CertificationExpirationDateInvalid")]
    CertificationExpirationDateInvalid,

    [CustomDisplay("InvalidNewEmploymentStatusDate")]
    InvalidNewEmploymentStatusDate,

    [CustomDisplay("UserNotFound")] UserNotFound,

    [CustomDisplay("UserLoginSuccess")] UserLoginSuccess,

    [CustomDisplay("LoanAdjustmentSuccess")]
    LoanAdjustmentSuccess,

    [CustomDisplay("LoanAdjustmentFailure")]
    LoanAdjustmentFailure,

    [CustomDisplay("EmailExist")] EmailExist,

    [CustomDisplay("UserWithFirstAndLastNameAlreadyExist")]
    UserWithFirstAndLastNameAlreadyExist,

    [CustomDisplay("InvalidEmail")] InvalidEmail,

    [CustomDisplay("InvalidDaysLimit")] InvalidDaysLimit,

    [CustomDisplay("InvalidDaysLimitWeekly")]
    InvalidDaysLimitWeekly,

    [CustomDisplay("TestWithNoQuestions")] TestWithNoQuestions,

    [CustomDisplay("PipOpen")] PipOpen,

    [CustomDisplay("PipClosed")] PipClosed,

    [CustomDisplay("PaymentTypeAndWorkingHoursMismatch")]
    PaymentTypeAndWorkingHoursMismatch,

    [CustomDisplay("ShiftHoursAndWorkingHoursMismatch")]
    ShiftHoursAndWorkingHoursMismatch,

    [CustomDisplay("ChangeRequestRejected")]
    ChangeRequestRejected,

    [CustomDisplay("ChangeRequestApproved")]
    ChangeRequestApproved,
}