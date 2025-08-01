// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheet")]
public class LatheCheckSheet
{
    [Column("Customer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Customer { get; set; }

    [Column("OpNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OperationNumber { get; set; }

    [Column("DateRevised", TypeName = "datetime")]
    public DateTime? DateRevised { get; set; }

    [Column("RevisedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RevisedBy { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("DrawingNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string DrawingNumber { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("CopyId", TypeName = "int")] public int? CopyId { get; set; }

    [Column("LineInspectionAuditId", TypeName = "int")]
    public int? LineInspectionAuditId { get; set; }
}