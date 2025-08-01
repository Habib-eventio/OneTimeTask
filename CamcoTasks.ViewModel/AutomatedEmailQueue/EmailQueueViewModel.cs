using System;

namespace CamcoTasks.ViewModels.EmailQueue
{
    public class EmailQueueViewModel
    {
        public int EmailId { get; set; }
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool HasBeenSent { get; set; } = false;
        public string Attachment { get; set; }
        public DateTime TimeEntered { get; set; } = DateTime.Now;
        public DateTime? TimeSent { get; set; }
        public bool HasError { get; set; } = false;
        public int? EmailTypeId { get; set; }
    }
}
