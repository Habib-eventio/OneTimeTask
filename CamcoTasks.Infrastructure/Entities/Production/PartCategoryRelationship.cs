// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_PartCategoryRelationship")]
public class PartCategoryRelationship
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string PartNumber { get; set; }

    [Column("PackingCategoryID", TypeName = "int")]
    public int PackingCategoryId { get; set; }

    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string EnteredBy { get; set; }

    [Column("Notes", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Notes { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime DateEntered { get; set; }

    [Column("DateUpdated", TypeName = "datetime")]
    public DateTime DateUpdated { get; set; }
}