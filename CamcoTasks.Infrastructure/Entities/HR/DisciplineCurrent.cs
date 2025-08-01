using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("HR_DisciplineCurrent")]
public class DisciplineCurrent
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

    [Column("EnumCurrentDisciplineLevelId", TypeName = "smallint")]
    public short EnumCurrentDisciplineLevelId { get; set; }

    [Column("DateEffective", TypeName = "datetime2(7)")]
    public DateTime DateEffective { get; set; }

    [Column("DateOfDownGrade", TypeName = "datetime2(7)")]
    public DateTime DateOfDownGrade { get; set; }

    [Column("EnumDisciplineId", TypeName = "smallint")]
    public short EnumDisciplineId { get; set; }
}