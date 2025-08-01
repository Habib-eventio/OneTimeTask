namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum VacationTimeAdjustmentType
{
    [CustomDisplay("Raise accrued vacation hours")]
    Raise = 1,

    [CustomDisplay("Reduce accrued vacation hours")]
    Reduce = 2
}