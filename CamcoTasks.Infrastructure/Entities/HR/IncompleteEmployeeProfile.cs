//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace CamcoTasks.Infrastructure.Entities.HR;

//[Table("HR_IncompleteEmployeeProfiles")]
//public class IncompleteEmployeeProfile
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("FirstName", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Required]
//    public string FirstName { get; set; }

//    [Column("LastName", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Required]
//    public string LastName { get; set; }

//    [Column("SocialSecurityNumber", TypeName = "varchar(MAX)")]
//    [MaxLength]
//    public string SocialSecurityNumber { get; set; }

//    [Column("GenderId", TypeName = "bigint")]
//    public long? GenderId { get; set; }

//    [Column("DateOfBirth", TypeName = "datetime2(7)")]
//    public DateTime? DateOfBirth { get; set; }

//    [Column("Image", TypeName = "varbinary(MAX)")]
//    public byte[] Image { get; set; }

//    [Column("DateOfHire", TypeName = "datetime2(7)")]
//    public DateTime? DateOfHire { get; set; }

//    [Column("ProbationEndDate", TypeName = "datetime2(7)")]
//    public DateTime? ProbationEndDate { get; set; }

//    [Column("IsFullTimeJob", TypeName = "bit")]
//    public bool? IsFullTimeJob { get; set; }

//    [Column("ShiftId", TypeName = "smallint")]
//    public short? ShiftId { get; set; }

//    [Column("DepartmentId", TypeName = "bigint")]
//    public long? DepartmentId { get; set; }

//    [Column("JobId", TypeName = "bigint")] public long? JobId { get; set; }

//    [Column("EmploymentStatusId", TypeName = "smallint")]
//    public short? EmploymentStatusId { get; set; }

//    [Column("EmployeeTypeId", TypeName = "bigint")]
//    public long? EmployeeTypeId { get; set; }

//    [Column("TierLevelId", TypeName = "bigint")]
//    public long? TierLevelId { get; set; }

//    [Browsable(false)]
//    [Column("ManagerId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectManagerIdEmployeeId Column). Please refer to CorrectManagerIdEmployeeId")]
//    public long? ObsoleteManagerId { get; set; }

//    [Column("CorrectManagerIdEmployeeId", TypeName = "bigint")]
//    public long? ManagerEmployeeId { get; set; }

//    [Browsable(false)]
//    [Column("LeaderId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectLeaderIdEmployeeId Column). Please refer to CorrectLeaderIdEmployeeId")]
//    public long? ObsoleteLeaderId { get; set; }

//    [Column("CorrectLeaderIdEmployeeId", TypeName = "bigint")]
//    public long? LeaderEmployeeId { get; set; }

//    [Column("IsSubContractor", TypeName = "bit")]
//    public bool? IsSubContractor { get; set; }

//    [Column("StateId", TypeName = "bigint")]
//    public long? StateId { get; set; }

//    [Column("CityId", TypeName = "bigint")]
//    public long? CityId { get; set; }

//    [Column("ZipCode", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string ZipCode { get; set; }

//    [Column("StreetAddress", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string StreetAddress { get; set; }

//    [Column("IsInternational", TypeName = "bit")]
//    public bool? IsInternational { get; set; }

//    [Column("Address", TypeName = "varchar(500)")]
//    [StringLength(500)]
//    [MaxLength(500)]
//    public string Address { get; set; }

//    [Column("MailingAddress", TypeName = "varchar(500)")]
//    [StringLength(500)]
//    [MaxLength(500)]
//    public string MailingAddress { get; set; }

//    [Column("MailingAddressStateId", TypeName = "bigint")]
//    public long? MailingAddressStateId { get; set; }

//    [Column("MailingAddressCityId", TypeName = "bigint")]
//    public long? MailingAddressCityId { get; set; }

//    [Column("MailingAddressZipCode", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string MailingAddressZipCode { get; set; }

//    [Column("MailingAddressStreetAddress", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string MailingAddressStreetAddress { get; set; }

//    [Column("CountryCode", TypeName = "varchar(2)")]
//    [StringLength(2)]
//    [MaxLength(2)]
//    public string CountryCode { get; set; }

//    [Column("CellNumber", TypeName = "varchar(50)")]
//    [StringLength(50)]
//    [MaxLength(50)]
//    public string CellNumber { get; set; }

//    [Column("HomePhoneNumber", TypeName = "varchar(50)")]
//    [StringLength(50)]
//    [MaxLength(50)]
//    public string HomePhoneNumber { get; set; }

//    [Column("CamcoEmail", TypeName = "nvarchar(256)")]
//    [StringLength(256)]
//    [MaxLength(256)]
//    public string CamcoEmail { get; set; }

//    [Column("IsPaymentTypeSalary", TypeName = "bit")]
//    public bool? IsPaymentTypeSalary { get; set; }

//    [Column("AnnualSalary", TypeName = "varchar(MAX)")]
//    [MaxLength]
//    public string AnnualSalary { get; set; }

//    [Column("BaseHourlyRate", TypeName = "varchar(MAX)")]
//    [MaxLength]
//    public string BaseHourlyRate { get; set; }

//    [Column("ShiftDifferential", TypeName = "varchar(MAX)")]
//    [MaxLength]
//    public string ShiftDifferential { get; set; }

//    [Column("OtherHourlyBonus", TypeName = "varchar(MAX)")]
//    [MaxLength]
//    public string OtherHourlyBonus { get; set; }

//    [Column("OtherHourlyBonusDescription", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string OtherHourlyBonusDescription { get; set; }

//    [Column("InsuranceTypeId", TypeName = "bigint")]
//    public long? InsuranceTypeId { get; set; }

//    [Column("EmployeeShare", TypeName = "decimal(7,2)")]
//    public decimal? EmployeeShare { get; set; }

//    [Column("CamcoShare", TypeName = "decimal(7,2)")]
//    public decimal? CamcoShare { get; set; }

//    [Column("InsuranceValues", TypeName = "varchar(1000)")]
//    [StringLength(1000)]
//    [MaxLength(1000)]
//    public string InsuranceValues { get; set; }

//    [Column("PercentOfDirectTime", TypeName = "decimal(7,2)")]
//    public decimal? PercentOfDirectTime { get; set; }

//    [Column("IsCustomVacations", TypeName = "bit")]
//    public bool? IsCustomVacations { get; set; }

//    [Column("AnnualLostTimeAllocation", TypeName = "decimal(7,2)")]
//    public decimal? AnnualLostTimeAllocation { get; set; }

//    [Column("AnnualVacationAllocationDays", TypeName = "decimal(7,2)")]
//    public decimal? AnnualVacationAllocationDays { get; set; }

//    [Column("AnnualVacationAllocationHours", TypeName = "decimal(7,2)")]
//    public decimal? AnnualVacationAllocationHours { get; set; }

//    [Browsable(false)]
//    [Column("ReferredById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectReferredByIdEmployeeId Column). Please refer to CorrectReferredByIdEmployeeId")]
//    public long? ObsoleteReferredById { get; set; }

//    [Column("CorrectReferredByIdEmployeeId", TypeName = "bigint")]
//    public long? ReferredByEmployeeId { get; set; }

//    [Column("DrugTestFrequencyId", TypeName = "smallint")]
//    public short? DrugTestFrequencyId { get; set; }

//    [Column("Notes", TypeName = "varchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Notes { get; set; }

//    [Browsable(false)]
//    [Column("CreatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
//    public long? ObsoleteCreatedById { get; set; }

//    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
//    public long CreatedByEmployeeId { get; set; }

//    [Column("DateCreated", TypeName = "datetime2(7)")]
//    public DateTime DateCreated { get; set; }

//    [Column("NormalWorkHours", TypeName = "decimal(7,2)")]
//    public decimal? NormalWorkHours { get; set; }

//    [Column("ShiftStartTime", TypeName = "datetime2(7)")]
//    public DateTime? ShiftStartTime { get; set; }

//    [Column("EmployeeDeskStatusId", TypeName = "bigint")]
//    public long? EmployeeDeskStatusId { get; set; }

//    [Column("ShiftEndTime", TypeName = "datetime2(7)")]
//    public DateTime? ShiftEndTime { get; set; }

//    [Column("IsSignedHandbookRequired", TypeName = "bit")]
//    public bool IsSignedHandbookRequired { get; set; }

//    [Browsable(false)]
//    [Column("SecondaryLeaderId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectSecondaryLeaderIdEmployeeId Column). Please refer to CorrectSecondaryLeaderIdEmployeeId")]
//    public long? ObsoleteSecondaryLeaderId { get; set; }

//    [Column("CorrectSecondaryLeaderIdEmployeeId", TypeName = "bigint")]
//    public long? SecondaryLeaderEmployeeId { get; set; }

//    [Column("SecondaryJobId", TypeName = "bigint")]
//    public long? SecondaryJobId { get; set; }

//    [Column("DeskLocationId", TypeName = "varchar(100)")]
//    [StringLength(100)]
//    [MaxLength(100)]
//    public string DeskLocationId { get; set; }

//    [Column("AllocatedActionsTypeIds", TypeName = "varchar(250)")]
//    [StringLength(250)]
//    [MaxLength(250)]
//    public string AllocatedActionsTypeIds { get; set; }

//    [Column("CompanyNameId", TypeName = "smallint")]
//    public short? CompanyNameId { get; set; }

//    [Column("IsPersonFromOutsideCompany", TypeName = "bit")]
//    public bool? IsPersonFromOutsideCompany { get; set; }

//    [Column("EmployeeQualityLevelStatus", TypeName = "bit")]
//    public bool? EmployeeQualityLevelStatus { get; set; }

//    [Column("EmployeeQualityLevelStatusId", TypeName = "bigint")]
//    public long? EmployeeQualityLevelStatusId { get; set; }

//    [Column("HasPhone", TypeName = "bit")]
//    public bool HasPhone { get; set; }

//    [Column("HasLaptop", TypeName = "bit")]
//    public bool HasLaptop { get; set; }

//    [Column("HasVpnAccess", TypeName = "bit")]
//    public bool HasVpnAccess { get; set; }
//}