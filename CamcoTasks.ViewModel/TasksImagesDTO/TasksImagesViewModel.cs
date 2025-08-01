using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.ViewModels.TasksImagesDTO
{
    public class TasksImagesViewModel : UploadFiles
    {
        public int Id { get; set; }
        public int RecurringId { get; set; }
        public int? UpdateId { get; set; }
        public int OneTimeId { get; set; }
        public string PictureLink { get; set; }
        public bool IsDeleted { get; set; }
        public string FileName { get; set; }
        public string ImageNote { get; set; }

    }
}
