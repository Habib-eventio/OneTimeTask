namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum LostTimeAdjustmentType
{
    [CustomDisplay("Raise accrued lost time hours")]
    Raise = 1,

    [CustomDisplay("Reduce accrued lost time hours")]
    Reduce = 2
}