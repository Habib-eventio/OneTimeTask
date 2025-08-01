// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_RouteDocumentType")]
public class RouteDocumentType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RouteDocumentTypeID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Type", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Type { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Description { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }

    [Column("DateLastChanged", TypeName = "datetime2(7)")]
    public DateTime DateLastChanged { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("FolderPath", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string FolderPath { get; set; }
}