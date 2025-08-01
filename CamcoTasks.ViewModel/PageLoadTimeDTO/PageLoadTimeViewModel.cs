using System;

namespace CamcoTasks.ViewModels.PageLoadTimeDTO
{
    public class PageLoadTimeViewModel
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string SectionName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
