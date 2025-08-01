using System;

namespace CamcoTasks.ViewModels.JobDescriptions
{
    public class JobDescriptionsViewModal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? RecommendedTierLevelId { get; set; }
        public string? Details { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
