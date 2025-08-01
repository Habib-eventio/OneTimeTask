namespace CamcoTasks.ViewModels.ModelsViewModel
{
    public class TaskCompletionViewModel
    {
        public string EmployeeName { get; set; }
        public string Color { get; set; }
        public int Percentage { get; set; }

        //Informative
        public int TotalCount { get; set; }
        public int CompletedCount { get; set; }
        public int FailedCount { get; set; }
    }
}
