using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_TestAssignments")]
public class TestAssignment
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("TestName", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string TestName { get; set; }

    [Column("TestDescription", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    [Required]
    public string TestDescription { get; set; }

    [Column("IsTestDurationAvailable", TypeName = "bit")]
    public bool IsTestDurationAvailable { get; set; }

    [Column("TestDuration", TypeName = "time(7)")]
    public TimeSpan? TestDuration { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long? EmployeeId { get; set; }

    [Column("IsDone", TypeName = "bit")] public bool IsDone { get; set; }

    [Column("TestDate", TypeName = "datetime2(7)")]
    public DateTime? TestDate { get; set; }

    [Column("TimeTakenToCompleteTest", TypeName = "time(7)")]
    public TimeSpan? TimeTakenToCompleteTest { get; set; }

    [Column("IsTestStarted", TypeName = "bit")]
    public bool IsTestStarted { get; set; }

    [Column("TimeSinceTestStarted", TypeName = "datetime2(7)")]
    public DateTime? TimeSinceTestStarted { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Browsable(false)]
    [Column("AdministratorId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectAdministratorIdEmployeeId Column). Please refer to CorrectAdministratorIdEmployeeId")]
    public long? ObsoleteAdministratorId { get; set; }

    [Column("CorrectAdministratorIdEmployeeId", TypeName = "bigint")]
    public long AdministratorEmployeeId { get; set; }

    [Column("TestTakerName", TypeName = "varchar(150)")]
    [StringLength(150)]
    [MaxLength(150)]
    [Required]
    public string TestTakerName { get; set; }

    [Column("IsGuestUser", TypeName = "bit")]
    public bool IsGuestUser { get; set; }
}