using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EmailGroups")]
public class EmailGroup
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("EmailTypeId", TypeName = "int")]
    public int EmailTypeId { get; set; }

    [Browsable(false)]
    [Column("EmailEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmailEmployeesId Column). Please refer to CorrectEmailEmployeesId")]
    public string ObsoleteEmailEmployeesId { get; set; }

    [Column("CorrectEmailEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string EmailEmployeesId { get; set; }

    [Column("EmailJobsId", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string EmailJobsId { get; set; }

    [Column("IsActive", TypeName = "bit")] public bool IsActive { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("EmailGroupFrequencyId", TypeName = "smallint")]
    public short? EmailGroupFrequencyId { get; set; }

    [Column("EumDayOfWeekId", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    public string EumDayOfWeekId { get; set; }

    [Column("NumberOfMonths", TypeName = "int")]
    public int? NumberOfMonths { get; set; }
}