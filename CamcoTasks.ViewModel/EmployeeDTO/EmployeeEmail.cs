namespace CamcoTasks.ViewModels.EmployeeDTO
{
    public class EmployeeEmail
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
