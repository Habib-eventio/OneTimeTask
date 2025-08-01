using System;

namespace CamcoTasks.ViewModels.UpdateNotesDTO
{
    public class UpdateNotesViewModel
    {
        public int ID { get; set; }
        public int? UpdateID { get; set; }
        public DateTime NoteDate { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
    }
}
