// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_PartType")]
public class PartType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("PartType", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string PartTypeName { get; set; }

    [Column("Description", TypeName = "nvarchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    [Required]
    public string Description { get; set; }

    [Column("EnteredById", TypeName = "bigint")]
    public long EnteredById { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }
}