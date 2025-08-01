// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.Automated;
using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("Automations_ScheduledEmails")]
public class ScheduledEmail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }


    [Column("AddedByEmpId", TypeName = "bigint")]
    public long AddedByEmployeeId { get; set; }

    [ForeignKey("AddedByEmployeeId")] public virtual Employee AddedByEmployee { get; set; }

    [Column("Subject", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Subject { get; set; }

    [Column("Body", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Body { get; set; }

    [Column("Attachment", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Attachment { get; set; }

    [Column("SendTime", TypeName = "datetime")]
    public DateTime SendTime { get; set; }

    [Column("AddedDate", TypeName = "datetime")]
    public DateTime AddedDate { get; set; }

    [Column("EmailQueueId", TypeName = "int")]
    public int? EmailQueueId { get; set; }

    [ForeignKey("EmailQueueId")] public virtual EmailQueue EmailQueue { get; set; }

    public virtual ICollection<ScheduledEmailSendToEmployee> ScheduledEmailSendToEmployees { get; set; }

    public ScheduledEmail()
    {
        ScheduledEmailSendToEmployees = new List<ScheduledEmailSendToEmployee>();
    }
}