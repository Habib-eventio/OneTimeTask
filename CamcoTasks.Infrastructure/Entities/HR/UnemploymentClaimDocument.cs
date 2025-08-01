using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_UnemploymentClaimDocuments")]
public class UnemploymentClaimDocument
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("DocumentTypeId", TypeName = "smallint")]
    public short DocumentTypeId { get; set; }

    [Column("UnemploymentClaimId", TypeName = "int")]
    public int UnemploymentClaimId { get; set; }

    [Column("DateDocumentWasSubmitted", TypeName = "datetime2(7)")]
    public DateTime DateDocumentWasSubmitted { get; set; }

    [Column("Attachment", TypeName = "varchar(250)")]
    [StringLength(250)]
    [MaxLength(250)]
    [Required]
    public string Attachment { get; set; }
}