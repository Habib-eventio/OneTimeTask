using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_HireAgreementChecksheets")]
public class HireAgreementChecksheet
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("EmployeeFirstName", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string EmployeeFirstName { get; set; }

    [Column("EmployeeLastName", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string EmployeeLastName { get; set; }

    [Column("TodaysDate", TypeName = "datetime2(7)")]
    public DateTime TodaysDate { get; set; }

    [Column("IsPaymentTypeSalary", TypeName = "bit")]
    public bool IsPaymentTypeSalary { get; set; }

    [Column("PayRate", TypeName = "decimal(18,2)")]
    public decimal PayRate { get; set; }

    [Column("TierLevelId", TypeName = "bigint")]
    public long TierLevelId { get; set; }

    [Column("JobTitleId", TypeName = "bigint")]
    public long JobTitleId { get; set; }

    [Column("StartDate", TypeName = "datetime2(7)")]
    public DateTime StartDate { get; set; }

    [Column("DepartmentId", TypeName = "bigint")]
    public long DepartmentId { get; set; }

    [Column("ExpectedHoursPerWeek", TypeName = "decimal(18,2)")]
    public decimal ExpectedHoursPerWeek { get; set; }

    [Column("EnumShiftId", TypeName = "smallint")]
    public short EnumShiftId { get; set; }

    [Browsable(false)]
    [Column("ManagerId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectManagerIdEmployeeId Column). Please refer to CorrectManagerIdEmployeeId")]
    public long? ObsoleteManagerId { get; set; }

    [Column("CorrectManagerIdEmployeeId", TypeName = "bigint")]
    public long ManagerEmployeeId { get; set; }

    [Column("Notes", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string Notes { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("SignedDocument", TypeName = "varchar(250)")]
    [StringLength(250)]
    [MaxLength(250)]
    public string SignedDocument { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Browsable(false)]
    [Column("UpdatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
    public long? ObsoleteUpdatedById { get; set; }

    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
    public long? UpdatedByEmployeeId { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime? DateUpdated { get; set; }

    [Browsable(false)]
    [Column("LeaderId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectLeaderIdEmployeeId Column). Please refer to CorrectLeaderIdEmployeeId")]
    public long? ObsoleteLeaderId { get; set; }

    [Column("CorrectLeaderIdEmployeeId", TypeName = "bigint")]
    public long? LeaderEmployeeId { get; set; }
}