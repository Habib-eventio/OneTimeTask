using System;

namespace CamcoTasks.ViewModels.LoggingChangeLogDTO
{
    public class LoggingChangeLogViewModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public string RecordField { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
