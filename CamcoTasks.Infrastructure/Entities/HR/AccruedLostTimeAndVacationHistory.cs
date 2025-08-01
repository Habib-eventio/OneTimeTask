using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_AccruedLostTimeAndVacationHistories")]
public class AccruedLostTimeAndVacationHistory
{
    [Key]
    [Column("Id", TypeName = "bigint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("AccruedUsedTime", TypeName = "decimal(9,4)")]
    public decimal AccruedUsedTime { get; set; }

    [Column("DateAccruedUsedTime", TypeName = "datetime2(7)")]
    public DateTime DateAccruedUsedTime { get; set; }

    [Column("AccruedTime", TypeName = "decimal(9,4)")]
    public decimal AccruedTime { get; set; }

    [Column("EnumTypeId", TypeName = "smallint")]
    public short EnumTypeId { get; set; }

    [Column("Details", TypeName = "varchar(500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    public string Details { get; set; }

    [Column("EnumHistoryTypeId", TypeName = "smallint")]
    public short EnumHistoryTypeId { get; set; }

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