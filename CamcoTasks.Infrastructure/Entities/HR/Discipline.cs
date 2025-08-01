using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_Disciplines")]
public class Discipline
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("IncidentDate", TypeName = "datetime2(7)")]
    public DateTime IncidentDate { get; set; }

    [Column("NatureOfViolationId", TypeName = "int")]
    public int NatureOfViolationId { get; set; }

    [Column("EnumDisciplineLevelId", TypeName = "smallint")]
    public short EnumDisciplineLevelId { get; set; }

    [Column("EnumDisciplineId", TypeName = "smallint")]
    public short EnumDisciplineId { get; set; }

    [Column("DisciplineLevelReductionDate", TypeName = "datetime2(7)")]
    public DateTime? DisciplineLevelReductionDate { get; set; }

    [Column("IncidentDescription", TypeName = "varchar(3500)")]
    [StringLength(3500)]
    [MaxLength(3500)]
    public string IncidentDescription { get; set; }

    [Column("CorrectiveAction", TypeName = "varchar(2000)")]
    [StringLength(2000)]
    [MaxLength(2000)]
    public string CorrectiveAction { get; set; }

    [Column("SignedDisciplineNoticeDocument", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string SignedDisciplineNoticeDocument { get; set; }

    [Browsable(false)]
    [Column("EmployeeReceivingDisciplineId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeReceivingDisciplineIdEmployeeId Column). Please refer to CorrectEmployeeReceivingDisciplineIdEmployeeId")]
    public long? ObsoleteEmployeeReceivingDisciplineId { get; set; }

    [Column("CorrectEmployeeReceivingDisciplineIdEmployeeId", TypeName = "bigint")]
    public long EmployeeReceivingDisciplineEmployeeId { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("IsLocked", TypeName = "bit")] public bool IsLocked { get; set; }

    [Column("JobTitleId", TypeName = "bigint")]
    public long? JobTitleId { get; set; }
}