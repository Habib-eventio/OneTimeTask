namespace CamcoTasks.ViewModels.MetricsGoalsDTO
{
    public class MetricsGoalsViewModel
    {
        public int Id { get; set; }
        public string GoalItem { get; set; }
        public string GoalDescription { get; set; }
        public string GoalCaution { get; set; }
        public string GoalAlert { get; set; }
        public bool IsBelowGoal { get; set; }
        public bool IsBelowCaution { get; set; }
        public bool IsBelowAlert { get; set; }
    }
}
