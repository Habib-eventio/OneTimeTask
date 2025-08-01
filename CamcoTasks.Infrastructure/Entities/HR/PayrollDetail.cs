using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PayrollDetails")]
public class PayrollDetail
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("PayrollId", TypeName = "bigint")]
    public long PayrollId { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("FurtherActionNeeded", TypeName = "bit")]
    public bool FurtherActionNeeded { get; set; }

    [Column("PaymentType", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string PaymentType { get; set; }

    [Column("BasePayRate", TypeName = "decimal(9,4)")]
    public decimal? BasePayRate { get; set; }

    [Column("RegularHours", TypeName = "decimal(7,2)")]
    public decimal? RegularHours { get; set; }

    [Column("OvertimeHours", TypeName = "decimal(7,2)")]
    public decimal? OvertimeHours { get; set; }

    [Column("VacationHours", TypeName = "decimal(7,2)")]
    public decimal? VacationHours { get; set; }

    [Column("NewVacationRemaining", TypeName = "decimal(9,4)")]
    public decimal? NewVacationRemaining { get; set; }

    [Column("VacationRemaining", TypeName = "decimal(9,4)")]
    public decimal? VacationRemaining { get; set; }

    [Column("HolidayHours", TypeName = "decimal(7,2)")]
    public decimal? HolidayHours { get; set; }

    [Column("LostTimeHours", TypeName = "decimal(7,2)")]
    public decimal? LostTimeHours { get; set; }

    [Column("NewLostTimeRemaining", TypeName = "decimal(9,4)")]
    public decimal? NewLostTimeRemaining { get; set; }

    [Column("LostTimeRemaining", TypeName = "decimal(9,4)")]
    public decimal? LostTimeRemaining { get; set; }

    [Column("OtherPaidTimeOffHours", TypeName = "decimal(7,2)")]
    public decimal? OtherPaidTimeOffHours { get; set; }

    [Column("UnpaidTimeOff", TypeName = "decimal(7,2)")]
    public decimal? UnpaidTimeOff { get; set; }

    [Column("ShiftDifference", TypeName = "decimal(7,2)")]
    public decimal? ShiftDifference { get; set; }

    [Column("OtherHourlyBonusAmount", TypeName = "decimal(7,2)")]
    public decimal? OtherHourlyBonusAmount { get; set; }

    [Column("RetroPay", TypeName = "decimal(7,2)")]
    public decimal? RetroPay { get; set; }

    [Column("Bonus", TypeName = "decimal(7,2)")]
    public decimal? Bonus { get; set; }

    [Column("Reimbursement", TypeName = "decimal(7,2)")]
    public decimal? Reimbursement { get; set; }

    [Column("MiscDeductions", TypeName = "decimal(7,2)")]
    public decimal? MiscDeductions { get; set; }

    [Column("CurrentBalanceLoan", TypeName = "decimal(7,2)")]
    public decimal? CurrentBalanceLoan { get; set; }

    [Column("ThisWeeksPayment", TypeName = "decimal(7,2)")]
    public decimal? ThisWeeksPayment { get; set; }

    [Column("ResultingBalance", TypeName = "decimal(7,2)")]
    public decimal? ResultingBalance { get; set; }

    [Column("SocialSecurity", TypeName = "decimal(7,2)")]
    public decimal? SocialSecurity { get; set; }

    [Column("Fica", TypeName = "decimal(7,2)")]
    public decimal? Fica { get; set; }

    [Column("DirectTimeHours", TypeName = "decimal(7,2)")]
    public decimal? DirectTimeHours { get; set; }

    [Column("DirectTimePercentage", TypeName = "decimal(7,2)")]
    public decimal? DirectTimePercentage { get; set; }

    [Column("Notes", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string Notes { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("LogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string LogId { get; set; }

    [Column("WeeklyAccruedVacationHours", TypeName = "decimal(9,4)")]
    public decimal? WeeklyAccruedVacationHours { get; set; }

    [Column("WeeklyAccruedLostTime", TypeName = "decimal(9,4)")]
    public decimal? WeeklyAccruedLostTime { get; set; }

    [Column("WeeklyLoanPayment", TypeName = "decimal(7,2)")]
    public decimal? WeeklyLoanPayment { get; set; }

    [Column("VacationHoursTierOrCustom", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string VacationHoursTierOrCustom { get; set; }

    [Column("CustomVacationHours", TypeName = "decimal(7,2)")]
    public decimal? CustomVacationHours { get; set; }

    [Column("WeeksGrossPayment", TypeName = "decimal(7,2)")]
    public decimal? WeeksGrossPayment { get; set; }

    [Column("FullPartTime", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string FullPartTime { get; set; }

    [Column("SendForCheckPrinting", TypeName = "bit")]
    public bool? SendForCheckPrinting { get; set; }

    [Column("DepartmentId", TypeName = "bigint")]
    public long? DepartmentId { get; set; }

    [Column("ForReview", TypeName = "bit")]
    public bool? ForReview { get; set; }

    [Column("Approved", TypeName = "bit")] public bool? Approved { get; set; }

    [Column("Modified", TypeName = "bit")] public bool? Modified { get; set; }

    [Column("ChangesTrail", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string ChangesTrail { get; set; }

    [Column("SubContractor", TypeName = "bit")]
    public bool? SubContractor { get; set; }

    [Browsable(false)]
    [Column("PresentManagerId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectPresentManagerIdEmployeeId Column). Please refer to CorrectPresentManagerIdEmployeeId")]
    public long? ObsoletePresentManagerId { get; set; }

    [Column("CorrectPresentManagerIdEmployeeId", TypeName = "bigint")]
    public long? PresentManagerEmployeeId { get; set; }

    [Browsable(false)]
    [Column("InterimManagerId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectInterimManagerIdEmployeeId Column). Please refer to CorrectInterimManagerIdEmployeeId")]
    public long? ObsoleteInterimManagerId { get; set; }

    [Column("CorrectInterimManagerIdEmployeeId", TypeName = "bigint")]
    public long? InterimManagerEmployeeId { get; set; }

    [Browsable(false)]
    [Column("ThirdManagerId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectThirdManagerIdEmployeeId Column). Please refer to CorrectThirdManagerIdEmployeeId")]
    public long? ObsoleteThirdManagerId { get; set; }

    [Column("CorrectThirdManagerIdEmployeeId", TypeName = "bigint")]
    public long? ThirdManagerEmployeeId { get; set; }

    [Browsable(false)]
    [Column("Supervisor", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectSupervisorEmployeeId Column). Please refer to CorrectSupervisorEmployeeId")]
    public long? ObsoleteSupervisor { get; set; }

    [Column("CorrectSupervisorEmployeeId", TypeName = "bigint")]
    public long? SupervisorEmployeeId { get; set; }

    [Column("LineInspectionsPassPercentage", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string LineInspectionsPassPercentage { get; set; }

    [Column("FailedLineInspectionPercentageByInspector", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string FailedLineInspectionPercentageByInspector { get; set; }

    [Column("GeneralDisciplineCurrentPhaseId", TypeName = "bigint")]
    public long? GeneralDisciplineCurrentPhaseId { get; set; }

    [Column("GeneralDisciplineCurrentPhaseDate", TypeName = "datetime2(7)")]
    public DateTime? GeneralDisciplineCurrentPhaseDate { get; set; }

    [Column("GeneralDisciplineRecentId", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string GeneralDisciplineRecentId { get; set; }

    [Column("SafetyDisciplineCurrentPhaseId", TypeName = "bigint")]
    public long? SafetyDisciplineCurrentPhaseId { get; set; }

    [Column("SafetyDisciplineCurrentPhaseDate", TypeName = "datetime2(7)")]
    public DateTime? SafetyDisciplineCurrentPhaseDate { get; set; }

    [Column("SafetyDisciplineRecentId", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string SafetyDisciplineRecentId { get; set; }

    [Column("AttendanceDisciplineCurrentPhaseId", TypeName = "bigint")]
    public long? AttendanceDisciplineCurrentPhaseId { get; set; }

    [Column("AttendanceDisciplineCurrentPhaseDate", TypeName = "datetime2(7)")]
    public DateTime? AttendanceDisciplineCurrentPhaseDate { get; set; }

    [Column("AttendanceDisciplineRecentId", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string AttendanceDisciplineRecentId { get; set; }

    [Column("QualityDisciplineCurrentPhaseId", TypeName = "bigint")]
    public long? QualityDisciplineCurrentPhaseId { get; set; }

    [Column("QualityDisciplineCurrentPhaseDate", TypeName = "datetime2(7)")]
    public DateTime? QualityDisciplineCurrentPhaseDate { get; set; }

    [Column("QualityDisciplineRecentId", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string QualityDisciplineRecentId { get; set; }

    [Column("TimeSheetCellPhoneDisciplineCurrentPhaseId", TypeName = "bigint")]
    public long? TimeSheetCellPhoneDisciplineCurrentPhaseId { get; set; }

    [Column("TimeSheetCellPhoneDisciplineCurrentPhaseDate", TypeName = "datetime2(7)")]
    public DateTime? TimeSheetCellPhoneDisciplineCurrentPhaseDate { get; set; }

    [Column("TimeSheetCellPhoneDisciplineRecentId", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string TimeSheetCellPhoneDisciplineRecentId { get; set; }

    [Column("FiveSDisciplineCurrentPhaseId", TypeName = "bigint")]
    public long? FiveSDisciplineCurrentPhaseId { get; set; }

    [Column("FiveSDisciplineCurrentPhaseDate", TypeName = "datetime2(7)")]
    public DateTime? FiveSDisciplineCurrentPhaseDate { get; set; }

    [Column("FiveSDisciplineRecentId", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string FiveSDisciplineRecentId { get; set; }

    [Column("FiveSOnTimeCompletionPercentage", TypeName = "decimal(9,4)")]
    public decimal? FiveSOnTimeCompletionPercentage { get; set; }

    [Column("JarvisClockInOnTimePercentage", TypeName = "decimal(9,4)")]
    public decimal? JarvisClockInOnTimePercentage { get; set; }

    [Column("MinutesProducedPerHourLast30Days", TypeName = "decimal(9,4)")]
    public decimal? MinutesProducedPerHourLast30Days { get; set; }

    [Column("MinutesProducedPerHourLast90Days", TypeName = "decimal(9,4)")]
    public decimal? MinutesProducedPerHourLast90Days { get; set; }

    [Column("NormalWorkingHours", TypeName = "decimal(7,2)")]
    public decimal? NormalWorkingHours { get; set; }

    [Column("IsDisplayFiveSOnTimeCompletionPercentage", TypeName = "bit")]
    public bool? IsDisplayFiveSOnTimeCompletionPercentage { get; set; }

    [Column("IsDisplayJarvisClockInOnTimePercentage", TypeName = "bit")]
    public bool? IsDisplayJarvisClockInOnTimePercentage { get; set; }

    [Column("IsDisplayMinutesProducedPerHourLast30Days", TypeName = "bit")]
    public bool? IsDisplayMinutesProducedPerHourLast30Days { get; set; }

    [Column("IsDisplayMinutesProducedPerHourLast90Days", TypeName = "bit")]
    public bool? IsDisplayMinutesProducedPerHourLast90Days { get; set; }

    public virtual Payroll Payroll { get; set; }
}