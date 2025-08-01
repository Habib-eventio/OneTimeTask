// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_BrokenSheets")]
public class BrokenSheet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string PartNumber { get; set; }

    [Column("OperationNumber", TypeName = "nvarchar(10)")]
    [StringLength(10)]
    [MaxLength(10)]
    [Required]
    public string OperationNumber { get; set; }

    [Column("Sheet", TypeName = "nvarchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string Sheet { get; set; }

    [Column("DateEnterred", TypeName = "datetime")]
    public DateTime DateEntered { get; set; }

    [Column("DateFixed", TypeName = "datetime")]
    public DateTime? DateFixed { get; set; }

    [Column("EnterredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string EnteredBy { get; set; }
}