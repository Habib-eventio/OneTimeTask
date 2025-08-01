using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_WeeklyInsuranceLogs")]
public class WeeklyInsuranceLog
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("WeeklyInsuranceId", TypeName = "bigint")]
    public long WeeklyInsuranceId { get; set; }

    [Column("LogTypeId", TypeName = "int")]
    public int LogTypeId { get; set; }

    [Column("OldValue", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    [Required]
    public string OldValue { get; set; }

    [Column("NewValue", TypeName = "varchar(1500)")]
    [StringLength(1500)]
    [MaxLength(1500)]
    [Required]
    public string NewValue { get; set; }

    [Browsable(false)]
    [Column("UpdatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
    public long? ObsoleteUpdatedById { get; set; }

    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
    public long UpdatedByEmployeeId { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime DateUpdated { get; set; }
}