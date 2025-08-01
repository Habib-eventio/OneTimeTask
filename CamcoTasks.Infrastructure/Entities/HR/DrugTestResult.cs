using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_DrugTestResults")]
public class DrugTestResult
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("EnumReasonForDrugTestId", TypeName = "smallint")]
    public short EnumReasonForDrugTestId { get; set; }

    [Column("TestDate", TypeName = "datetime2(7)")]
    public DateTime? TestDate { get; set; }

    [Column("ResultDate", TypeName = "datetime2(7)")]
    public DateTime? ResultDate { get; set; }

    [Column("EnumResultId", TypeName = "smallint")]
    public short? EnumResultId { get; set; }

    [Column("Remarks", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string Remarks { get; set; }

    [Column("AppointmentAcknowledgement", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string AppointmentAcknowledgement { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("LogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string LogId { get; set; }

    [Column("DrugTestFrequencyId", TypeName = "bigint")]
    public long? DrugTestFrequencyId { get; set; }

    [Column("PrescriptionDocument", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string PrescriptionDocument { get; set; }

    [Column("PrescriptionName", TypeName = "varchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    public string PrescriptionName { get; set; }
}