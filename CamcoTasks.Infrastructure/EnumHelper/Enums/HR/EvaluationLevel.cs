namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum EvaluationLevel
{
    [CustomDisplay("N/A")] NotAvailable = 1,

    [CustomDisplay("POOR")] Poor = 2,

    [CustomDisplay("FAIR")] Fair = 3,

    [CustomDisplay("GOOD")] Good = 4,

    [CustomDisplay("EXCELLENT")] Excellent = 5
}