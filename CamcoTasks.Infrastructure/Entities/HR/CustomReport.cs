using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_CustomReports")]
public class CustomReport
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ReportTitle", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string ReportTitle { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("IsFullName", TypeName = "bit")]
    public bool IsFullName { get; set; }

    [Column("IsGender", TypeName = "bit")] public bool IsGender { get; set; }

    [Column("IsDateOfBirth", TypeName = "bit")]
    public bool IsDateOfBirth { get; set; }

    [Column("IsSocialSecurityNumber", TypeName = "bit")]
    public bool IsSocialSecurityNumber { get; set; }

    [Column("IsEmail", TypeName = "bit")] public bool IsEmail { get; set; }

    [Column("IsCellNumber", TypeName = "bit")]
    public bool IsCellNumber { get; set; }

    [Column("IsHomePhoneNumber", TypeName = "bit")]
    public bool IsHomePhoneNumber { get; set; }

    [Column("IsInternational", TypeName = "bit")]
    public bool IsInternational { get; set; }

    [Column("IsStreetAddress", TypeName = "bit")]
    public bool IsStreetAddress { get; set; }

    [Column("IsZipCode", TypeName = "bit")]
    public bool IsZipCode { get; set; }

    [Column("IsCity", TypeName = "bit")] public bool IsCity { get; set; }

    [Column("IsState", TypeName = "bit")] public bool IsState { get; set; }

    [Column("IsEmploymentStatus", TypeName = "bit")]
    public bool IsEmploymentStatus { get; set; }

    [Column("IsDateOfHire", TypeName = "bit")]
    public bool IsDateOfHire { get; set; }

    [Column("IsEmployeeTypeId", TypeName = "bit")]
    public bool IsEmployeeTypeId { get; set; }

    [Column("IsJobType", TypeName = "bit")]
    public bool IsJobType { get; set; }

    [Column("IsJob", TypeName = "bit")] public bool IsJob { get; set; }

    [Column("IsShift", TypeName = "bit")] public bool IsShift { get; set; }

    [Column("IsExpectedWorkingHoursPerWeek", TypeName = "bit")]
    public bool IsExpectedWorkingHoursPerWeek { get; set; }

    [Column("IsReferredBy", TypeName = "bit")]
    public bool IsReferredBy { get; set; }

    [Column("IsDepartment", TypeName = "bit")]
    public bool IsDepartment { get; set; }

    [Column("IsDepartmentHead", TypeName = "bit")]
    public bool IsDepartmentHead { get; set; }

    [Column("IsManager", TypeName = "bit")]
    public bool IsManager { get; set; }

    [Column("IsLeader", TypeName = "bit")] public bool IsLeader { get; set; }

    [Column("IsTierLevel", TypeName = "bit")]
    public bool IsTierLevel { get; set; }

    [Column("IsDrugTestFrequency", TypeName = "bit")]
    public bool IsDrugTestFrequency { get; set; }

    [Column("IsPaymentType", TypeName = "bit")]
    public bool IsPaymentType { get; set; }

    [Column("IsAnnualSalary", TypeName = "bit")]
    public bool IsAnnualSalary { get; set; }

    [Column("IsBaseHourlyRate", TypeName = "bit")]
    public bool IsBaseHourlyRate { get; set; }

    [Column("IsOtherHourlyBonus", TypeName = "bit")]
    public bool IsOtherHourlyBonus { get; set; }

    [Column("IsShiftDifferential", TypeName = "bit")]
    public bool IsShiftDifferential { get; set; }

    [Column("IsPercentOfDirectTime", TypeName = "bit")]
    public bool IsPercentOfDirectTime { get; set; }

    [Column("IsVacationDaysBase", TypeName = "bit")]
    public bool IsVacationDaysBase { get; set; }

    [Column("IsAnnualLostTimeAllocation", TypeName = "bit")]
    public bool IsAnnualLostTimeAllocation { get; set; }

    [Column("IsAnnualVacationAllocationDays", TypeName = "bit")]
    public bool IsAnnualVacationAllocationDays { get; set; }

    [Column("IsAnnualVacationAllocationHours", TypeName = "bit")]
    public bool IsAnnualVacationAllocationHours { get; set; }

    [Column("IsAvailableLostTimeHours", TypeName = "bit")]
    public bool IsAvailableLostTimeHours { get; set; }

    [Column("IsAvailableVacationHours", TypeName = "bit")]
    public bool IsAvailableVacationHours { get; set; }

    [Column("IsShiftStartTime", TypeName = "bit")]
    public bool IsShiftStartTime { get; set; }

    [Column("IsYearInService", TypeName = "bit")]
    public bool IsYearInService { get; set; }

    [Column("IsNewHirePaperworkPdfUploaded", TypeName = "bit")]
    public bool IsNewHirePaperworkPdfUploaded { get; set; }

    [Column("IsUploadSignedEmployeeHandBook", TypeName = "bit")]
    public bool IsUploadSignedEmployeeHandBook { get; set; }

    [Column("IsEmployeeDeskStatus", TypeName = "bit")]
    public bool IsEmployeeDeskStatus { get; set; }

    [Column("IsShiftEndTime", TypeName = "bit")]
    public bool IsShiftEndTime { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("IsPermanentSaved", TypeName = "bit")]
    public bool IsPermanentSaved { get; set; }

    [Column("IsSecondaryLeaderId", TypeName = "bit")]
    public bool IsSecondaryLeaderId { get; set; }

    [Column("IsSecondaryJobTitle", TypeName = "bit")]
    public bool IsSecondaryJobTitle { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }
}