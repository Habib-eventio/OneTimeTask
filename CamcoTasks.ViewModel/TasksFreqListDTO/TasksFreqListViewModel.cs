namespace CamcoTasks.ViewModels.TasksFrequencyListDTO
{
    public class TasksFrequencyListViewModel
    {
        public int Id { get; set; }
        public string? Frequency { get; set; }
        public int Days { get; set; }
        public bool? IsSelectDay { get; set; }
    }
}
