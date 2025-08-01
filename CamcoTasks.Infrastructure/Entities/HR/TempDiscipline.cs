using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_TempDisciplines")]
public class TempDiscipline
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("IncidentDate", TypeName = "datetime2(7)")]
    public DateTime? IncidentDate { get; set; }

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

    [Column("CorrectiveAction", TypeName = "varchar(3500)")]
    [StringLength(3500)]
    [MaxLength(3500)]
    public string CorrectiveAction { get; set; }

    [Browsable(false)]
    [Column("EmployeeReceivingDisciplineId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeReceivingDisciplineIdEmployeeId Column). Please refer to CorrectEmployeeReceivingDisciplineIdEmployeeId")]
    public long? ObsoleteEmployeeReceivingDisciplineId { get; set; }

    [Column("CorrectEmployeeReceivingDisciplineIdEmployeeId", TypeName = "bigint")]
    public long EmployeeReceivingDisciplineEmployeeId { get; set; }

    [Browsable(false)]
    [Column("CurrentUserId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCurrentUserIdEmployeeId Column). Please refer to CorrectCurrentUserIdEmployeeId")]
    public long? ObsoleteCurrentUserId { get; set; }

    [Column("CorrectCurrentUserIdEmployeeId", TypeName = "bigint")]
    public long CurrentUserEmployeeId { get; set; }

    [Column("JobTitleId", TypeName = "bigint")]
    public long? JobTitleId { get; set; }
}