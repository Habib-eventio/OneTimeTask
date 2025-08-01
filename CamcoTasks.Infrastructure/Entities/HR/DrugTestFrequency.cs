using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_DrugTestFrequencies")]
public class DrugTestFrequency
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("EnumDrugTestFrequencyId", TypeName = "smallint")]
    public short EnumDrugTestFrequencyId { get; set; }

    [Column("ChangeReason", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    [Required]
    public string ChangeReason { get; set; }

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

    [Column("LogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string LogId { get; set; }

    [Column("ChangeFormAttachment", TypeName = "varchar(200)")]
    [StringLength(200)]
    [MaxLength(200)]
    public string ChangeFormAttachment { get; set; }
}