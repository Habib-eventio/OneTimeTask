// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_RouteDocuments_RevisionHistory")]
public class RouteDocumentsRevisionHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RevisionID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Revision", TypeName = "int")] public int Revision { get; set; }

    [Column("RevisionDate", TypeName = "datetime2(0)")]
    public DateTime RevisionDate { get; set; }

    [Column("LastRevision", TypeName = "int")]
    public int LastRevision { get; set; }

    [Column("OldDocLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string OldDocumentLink { get; set; }

    [Column("NewDocLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string NewDocumentLink { get; set; }

    [Column("RevisedBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string RevisedBy { get; set; }
}