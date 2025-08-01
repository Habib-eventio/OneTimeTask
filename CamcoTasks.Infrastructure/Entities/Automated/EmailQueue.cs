using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("Automated_EmailQueue")]
public class EmailQueue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("EmailID", TypeName = "int")]
    public int Id { get; set; }

    [Required]
    [MaxLength]
    [Column("SendTo", TypeName = "nvarchar(MAX)")]
    public string SendTo { get; set; }

    [Required]
    [MaxLength]
    [Column("Subject", TypeName = "nvarchar(MAX)")]
    public string Subject { get; set; }

    [Required]
    [MaxLength]
    [Column("Body", TypeName = "nvarchar(MAX)")]
    public string Body { get; set; }

    [Column("HasBeenSent", TypeName = "bit")]
    public bool HasBeenSent { get; set; }

    [MaxLength]
    [Column("Attachment", TypeName = "nvarchar(MAX)")]
    public string Attachment { get; set; }

    [Column("TimeEntered", TypeName = "datetime2(7)")]
    public DateTime TimeEntered { get; set; }

    [Column("TimeSent", TypeName = "datetime2(7)")]
    public DateTime? TimeSent { get; set; }

    [Column("HasError", TypeName = "bit")] public bool HasError { get; set; }

    [Column("EmailTypeId", TypeName = "int")]
    public int? EmailTypeId { get; set; }

    public virtual ICollection<ScheduledEmail> ScheduledEmails { get; set; }

    public EmailQueue()
    {
        ScheduledEmails = new List<ScheduledEmail>();
    }
}