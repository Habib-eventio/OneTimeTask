// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_ProgramInformation")]
public class ProgramInformation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Machine", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Machine { get; set; }

    [Column("ProgNum", TypeName = "int")] public int ProgramNumber { get; set; }

    [Column("PartName", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string PartName { get; set; }

    [Column("Blueprint", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Blueprint { get; set; }

    [Column("Customer", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Customer { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime? DateEntered { get; set; }

    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string EnteredBy { get; set; }
}