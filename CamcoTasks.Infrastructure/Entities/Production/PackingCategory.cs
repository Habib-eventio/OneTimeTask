// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_PackingCategories")]
public class PackingCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Title", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Title { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Description { get; set; }

    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string EnteredBy { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime DateEntered { get; set; }

    [Column("Definition", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Definition { get; set; }
}