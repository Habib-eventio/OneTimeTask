using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_OfficeAudits")]
public class OfficeAudit
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("ExcelFilePath", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string ExcelFilePath { get; set; }

    [Column("AuditDate", TypeName = "datetime2(7)")]
    public DateTime? AuditDate { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Browsable(false)]
    [Column("UpdatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
    public long? ObsoleteUpdatedById { get; set; }

    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
    public long? UpdatedByEmployeeId { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime? DateUpdated { get; set; }
}