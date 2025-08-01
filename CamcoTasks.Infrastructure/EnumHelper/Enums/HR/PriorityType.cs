namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum PriorityType
{
    [CustomDisplay("High 1-7 days $30.00 per day")]
    High = 1,

    [CustomDisplay("Medium 1-14 days $20.00 per day")]
    Medium = 2,

    [CustomDisplay("Low 1-30 days $10.00 per day")]
    Low = 3,
}