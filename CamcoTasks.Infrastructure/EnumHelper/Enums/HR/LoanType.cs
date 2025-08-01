namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum LoanType
{
    [CustomDisplay("AdjustedUp")] AdjustedUp = 1,

    [CustomDisplay("AdjustedDown")] AdjustedDown = -2,

    [CustomDisplay("LoanPayment")] LoanPayment = -1,

    [CustomDisplay("NewLoan")] NewLoan = 2,
}