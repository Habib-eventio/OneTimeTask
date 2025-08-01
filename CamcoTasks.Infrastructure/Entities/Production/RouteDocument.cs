// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_RouteDocument")]
public class RouteDocument
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RouteDocumentID", TypeName = "int")]
    public int Id { get; set; }

    [Column("RouteDocumentTypeID", TypeName = "int")]
    public int RouteDocumentTypeId { get; set; }

    [Column("Type", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Type { get; set; }

    [Column("DocumentLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string DocumentLink { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string PartNumber { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }

    [Column("DateLastChanged", TypeName = "datetime2(7)")]
    public DateTime DateLastChanged { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Description { get; set; }

    [Column("Operation", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Operation { get; set; }

    [Column("Revision", TypeName = "int")] public int Revision { get; set; }
}