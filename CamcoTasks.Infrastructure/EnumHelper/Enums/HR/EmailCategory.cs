namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum EmailCategory
{
    [CustomDisplay("Automated")] Automated = 1,

    [CustomDisplay("Action Based")] ActionBased = 2,

    [CustomDisplay("Other")] Other = 3
}