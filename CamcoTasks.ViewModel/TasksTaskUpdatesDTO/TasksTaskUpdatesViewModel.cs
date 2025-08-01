using CamcoTasks.ViewModels.UpdateNotesDTO;
using System;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TasksTaskUpdatesDTO
{
    public class TasksTaskUpdatesViewModel
    {
        public int UpdateId { get; set; }
        public int? TaskID { get; set; }
        public int? RecurringID { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? UpcomingDate { get; set; } 
        public DateTime? DueDate { get; set; }
        public string Update { get; set; }
        public string QuestionAnswer { get; set; }
        public string FailedAuditList { get; set; }
        public string PictureLink { get; set; }
        public string FileLink { get; set; }
        public bool? IsPicture { get; set; }
        public bool IsDeleted { get; set; }
        public List<UpdateNotesViewModel> Notes { get; set; }
      
        public bool? IsAudit { get; set; }
        public bool? IsPass { get; set; }
        public decimal GraphNumber { get; set; }
        public string GraphDate { get; set; }

        public bool? TaskCompleted { get; set; }
        public string Color { get; set; } = "#1A3661";
        public string FailReason { get; set; } = "";
        public int? EmailId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? PostponeReason { get; set; }
        public int? PostponeDays { get; set; }
        public string UpdateBy { get; set; }
        public string UpdatedDocumentLink { get; set; }
    }
}
