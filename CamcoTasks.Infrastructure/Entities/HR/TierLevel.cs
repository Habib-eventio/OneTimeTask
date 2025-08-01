using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  CamcoTasks.Infrastructure.Entities.HR;
[Table("HR_TierLevels")]
public class TierLevel
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("Name", TypeName = "varchar(30)")]
    public string Name { get; set; }

    [Column("InsurancePercentage", TypeName = "decimal(7,2)")]
    public decimal InsurancePercentage { get; set; }

    [Column("HolidaysPerYear", TypeName = "int")]
    public int HolidaysPerYear { get; set; }

    [Column("HoursPerWeek", TypeName = "decimal(8,4)")]
    public decimal HoursPerWeek { get; set; }

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
    public DateTime? DateCreated { get; set; }

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