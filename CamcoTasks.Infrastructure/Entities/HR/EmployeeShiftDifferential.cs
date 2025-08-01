using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EmployeeShiftDifferential")]
public class EmployeeShiftDifferential
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

    [Column("EnumShiftId", TypeName = "smallint")]
    public short? EnumShiftId { get; set; }

    [Column("ShiftDifferential", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Required]
    public string ShiftDifferential { get; set; }

    [Column("DateEffective", TypeName = "datetime2(7)")]
    public DateTime DateEffective { get; set; }

    [Column("PreviousShiftDifferential", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Required]
    public string PreviousShiftDifferential { get; set; }

    [Column("LogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string LogId { get; set; }

    [Column("Attachment", TypeName = "varchar(250)")]
    [StringLength(250)]
    [MaxLength(250)]
    public string Attachment { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }
}