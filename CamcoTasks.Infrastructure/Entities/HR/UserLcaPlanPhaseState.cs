using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_UserLcaPlanPhaseStates")]
public class UserLcaPlanPhaseState
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("LcaPlanPhaseId", TypeName = "smallint")]
    public short LcaPlanPhaseId { get; set; }

    [Column("LcaPlanPhaseStartDate", TypeName = "datetime2(7)")]
    public DateTime LcaPlanPhaseStartDate { get; set; }

    [Column("LcaPlanPhaseEndDate", TypeName = "datetime2(7)")]
    public DateTime LcaPlanPhaseEndDate { get; set; }

    [Column("IsCompleted", TypeName = "bit")]
    public bool IsCompleted { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }
}