using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EmploymentsHistory")]
public class EmploymentHistory
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("EnumOldEmploymentStatusId", TypeName = "smallint")]
    public short? EnumOldEmploymentStatusId { get; set; }

    [Column("EnumNewEmploymentStatusId", TypeName = "smallint")]
    public short EnumNewEmploymentStatusId { get; set; }

    [Column("StartDate", TypeName = "datetime2(7)")]
    public DateTime StartDate { get; set; }

    [Column("EndDate", TypeName = "datetime2(7)")]
    public DateTime? EndDate { get; set; }

    [Column("Remarks", TypeName = "nvarchar(1000)")]
    [StringLength(1000)]
    [MaxLength(1000)]
    public string Remarks { get; set; }

    [Column("SeparationAndTerminationDocument", TypeName = "varchar(1000)")]
    [StringLength(1000)]
    [MaxLength(1000)]
    public string SeparationAndTerminationDocument { get; set; }

    [Column("SupportingDocument", TypeName = "varchar(1000)")]
    [StringLength(1000)]
    [MaxLength(1000)]
    public string SupportingDocument { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("LogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string LogId { get; set; }
}