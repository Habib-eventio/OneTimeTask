// This File Needs to be reviewed Still. Don't Remove this comment.

namespace CamcoTasks.Infrastructure.Repository.HR
{
    public class DepartmentViewModel
    {
            public long Id { get; set; }
            public string Name { get; set; }
            public string? DepartmentAbbreviation { get; set; }
            public string? DepartmentImage { get; set; }
            public long PrimaryManagerId { get; set; }
            public bool PrimaryManagerCanApproveTimeSheet { get; set; }
            public long? LeaderId { get; set; }
            public bool LeaderCanApproveTimeSheet { get; set; }
            public bool IsDeleted { get; set; }
            public long? CreatedById { get; set; }
            public DateTime? DateCreated { get; set; }
            public long? UpdatedById { get; set; }
            public DateTime? DateUpdated { get; set; }
    }
}
