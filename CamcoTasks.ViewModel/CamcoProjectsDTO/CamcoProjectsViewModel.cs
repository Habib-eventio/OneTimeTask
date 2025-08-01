using System;

namespace CamcoTasks.ViewModels.CamcoProjectsDTO
{
    public class CamcoProjectsViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsPostponed { get; set; }
        public string Title { get; set; }
        public string EnteredBy { get; set; }
        public string Champion { get; set; }

        public long? EnteredByEmployeeId { get; set; }
        public long? ChampionEmployeeId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ProjectType { get; set; }
        public string Notes { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectTypeName { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string PostponedReason { get; set; }
        public long? UpdatedById { get; set; }
    }
}
