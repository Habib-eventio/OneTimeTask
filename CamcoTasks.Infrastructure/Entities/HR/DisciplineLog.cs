using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_DisciplineLogs")]
public class DisciplineLog
{
    [Column("LogId", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("DisciplineId", TypeName = "bigint")]
    public long DisciplineId { get; set; }

    [Column("LogTypeId", TypeName = "int")]
    public int LogTypeId { get; set; }

    [Column("OldValue", TypeName = "varchar(2500)")]
    [StringLength(2500)]
    [MaxLength(2500)]
    public string OldValue { get; set; }

    [Column("NewValue", TypeName = "varchar(2500)")]
    [StringLength(2500)]
    [MaxLength(2500)]
    [Required]
    public string NewValue { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime DateUpdated { get; set; }

    [Browsable(false)]
    [Column("UpdatedBy", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByEmployeeId Column). Please refer to CorrectUpdatedByEmployeeId")]
    public long? ObsoleteUpdatedBy { get; set; }

    [Column("CorrectUpdatedByEmployeeId", TypeName = "bigint")]
    public long? UpdatedByEmployeeId { get; set; }
}