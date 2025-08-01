using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Logging;

[Table("Logging_ChangeLog")]
public class ChangeLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("RecordType", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string RecordType { get; set; }

    [Column("RecordID", TypeName = "int")] public int RecordId { get; set; }

    [Column("RecordField", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string RecordField { get; set; }

    [Column("OldValue", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string OldValue { get; set; }

    [Column("NewValue", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string NewValue { get; set; }

    [Column("UpdateDate", TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [Column("UpdatedBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string UpdatedBy { get; set; }

    [Column("Notes", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Notes { get; set; }
}