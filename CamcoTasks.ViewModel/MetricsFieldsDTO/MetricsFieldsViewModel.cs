namespace CamcoTasks.ViewModels.MetricsFieldsDTO
{
    public class MetricsFieldsViewModel
    {
        public int Id { get; set; }
        public bool IsMetrics { get; set; }
        public string DataName { get; set; }
        public string DataValue { get; set; }
        public bool IsGoalRequired { get; set; }
        public int GoalId { get; set; }

        public string IsModalClosed { get; set; } = "modal";
    }
}
