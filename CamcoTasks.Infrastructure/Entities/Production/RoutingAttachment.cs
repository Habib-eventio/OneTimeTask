// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_RoutingAttachments")]
public class RoutingAttachment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("AttachmentTypeID", TypeName = "int")]
    public int AttachmentTypeId { get; set; }

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

    [Column("Link", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Link { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }
}