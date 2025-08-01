using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_EmployeeTerminationSeparationForms")]
public class EmployeeTerminationSeparationForm
{
    [Key]
    [Column("Id", TypeName = "bigint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("TerminationDate", TypeName = "datetime2(7)")]
    public DateTime TerminationDate { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("FormDate", TypeName = "datetime2(7)")]
    public DateTime FormDate { get; set; }

    [Column("EnumReasonId", TypeName = "smallint")]
    public short EnumReasonId { get; set; }

    [Column("Explanation", TypeName = "varchar(5000)")]
    [StringLength(5000)]
    [MaxLength(5000)]
    [Required]
    public string Explanation { get; set; }

    [Column("TerminationSeparationDocument", TypeName = "varchar(250)")]
    [StringLength(250)]
    [MaxLength(250)]
    public string TerminationSeparationDocument { get; set; }

    [Column("SupportingDocument", TypeName = "varchar(250)")]
    [StringLength(250)]
    [MaxLength(250)]
    public string SupportingDocument { get; set; }

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

    [Column("EmploymentStatusId", TypeName = "smallint")]
    public short? EmploymentStatusId { get; set; }
}