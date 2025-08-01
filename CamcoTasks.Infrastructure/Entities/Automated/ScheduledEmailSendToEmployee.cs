// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Automated;

[Table("Automations_ScheduledEmailSendToEmployees")]
public class ScheduledEmailSendToEmployee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("SentToId", TypeName = "bigint")]
    public long SentToEmployeeId { get; set; }

    [ForeignKey("SentToEmployeeId")] public virtual Employee SentToEmployee { get; set; }

    [Column("ScheduledEmailId", TypeName = "int")]
    public int ScheduledEmailId { get; set; }

    [ForeignKey("ScheduledEmailId")] public virtual ScheduledEmail ScheduledEmail { get; set; }
}