using System;

namespace CamcoTasks.ViewModels.ItTicket
{
    public class ItTicketsViewModel
    {
        public int ItemNum { get; set; }
        public string InitialDesc { get; set; }
        public DateTime? InitDate { get; set; }
        public string SubBy { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Status { get; set; }
        public string Urgency { get; set; }
        public string Type { get; set; }
        public string AssignedTo { get; set; }
        public string Image { get; set; }
        public int? TicketDatabaseId { get; set; }
        public int? PendingReviewCount { get; set; }
        public string AttachedFile { get; set; }
        public bool? Active { get; set; }
        public DateTime? PendingReviewDate { get; set; }
        public bool? IsUi { get; set; }
        public DateTime? ChangedDate { get; set; }
        public int? PriorityNumber { get; set; }
        public bool? IanHelpNeeded { get; set; }
        public string ContactNumber { get; set; }
        public string ComputerReporting { get; set; }
    }
}
