using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR
{
    [Table("HR_Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "bigint")]
        public long Id { get; set; }

        [Required, StringLength(255), MaxLength(255)]
        [Column("FirstName", TypeName = "varchar(255)")]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(255), MaxLength(255)]
        [Column("LastName", TypeName = "varchar(255)")]
        public string LastName { get; set; } = null!;

        [Column("SocialSecurityNumber", TypeName = "varchar(MAX)")]
        public string? SocialSecurityNumber { get; set; }

        [Column("GenderId", TypeName = "bigint")]
        public long? GenderId { get; set; }

        [Column("DateOfBirth", TypeName = "datetime2(7)")]
        public DateTime? DateOfBirth { get; set; }

        [Column("DateOfHire", TypeName = "datetime2(7)")]
        public DateTime DateOfHire { get; set; }

        [Column("ProbationEndDate", TypeName = "datetime2(7)")]
        public DateTime? ProbationEndDate { get; set; }

        [Column("IsFullTimeJob", TypeName = "bit")]
        public bool IsFullTimeJob { get; set; }

        [Column("ShiftId", TypeName = "smallint")]
        public short? ShiftId { get; set; }

        [Column("DepartmentId", TypeName = "bigint")]
        public long DepartmentId { get; set; }

        [Column("ShiftDifferential", TypeName = "varchar(MAX)")]
        public string? ShiftDifferential { get; set; }

        [Column("JobId", TypeName = "bigint")]
        public long JobId { get; set; }

        [Column("EmploymentStatusId", TypeName = "smallint")]
        public short? EmploymentStatusId { get; set; }

        [Column("IsActive", TypeName = "bit")]
        public bool IsActive { get; set; }
        [Column("IsAuditor", TypeName = "bit")]
        public bool? IsAuditor { get; set; }
        [Column("EmployeeTypeId", TypeName = "bigint")]
        public long EmployeeTypeId { get; set; }

        [Obsolete]
        [Column("IsSubContractor", TypeName = "bit")]
        public bool? IsSubContractor { get; set; }

        [Column("IsInternational", TypeName = "bit")]
        public bool IsInternational { get; set; }

        [Column("Address", TypeName = "varchar(500)")]
        public string? Address { get; set; }

        [Column("CellNumber", TypeName = "varchar(50)")]
        public string? CellNumber { get; set; }

        [Column("HomePhoneNumber", TypeName = "varchar(50)")]
        public string? HomePhoneNumber { get; set; }

        [Column("StateId", TypeName = "bigint")]
        public long? StateId { get; set; }

        [Column("CityId", TypeName = "bigint")]
        public long? CityId { get; set; }

        [Column("ZipCode", TypeName = "varchar(255)")]
        public string? ZipCode { get; set; }

        [Column("StreetAddress", TypeName = "varchar(255)")]
        public string? StreetAddress { get; set; }

        [Column("IsPaymentTypeSalary", TypeName = "bit")]
        public bool IsPaymentTypeSalary { get; set; }

        [Column("AnnualSalary", TypeName = "varchar(MAX)")]
        public string? AnnualSalary { get; set; }

        [Column("BaseHourlyRate", TypeName = "varchar(MAX)")]
        public string? BaseHourlyRate { get; set; }

        [Column("OtherHourlyBonus", TypeName = "varchar(MAX)")]
        public string? OtherHourlyBonus { get; set; }

        [Column("OtherHourlyBonusDescription", TypeName = "varchar(255)")]
        public string? OtherHourlyBonusDescription { get; set; }

        [Column("PercentOfDirectTime", TypeName = "decimal(7,2)")]
        public decimal? PercentOfDirectTime { get; set; }

        [Column("AnnualLostTimeAllocation", TypeName = "decimal(7,2)")]
        public decimal? AnnualLostTimeAllocation { get; set; }

        [Column("IsCustomVacations", TypeName = "bit")]
        public bool IsCustomVacations { get; set; }

        [Column("AnnualVacationAllocationHours", TypeName = "decimal(7,2)")]
        public decimal? AnnualVacationAllocationHours { get; set; }

        [Obsolete]
        [Column("ReferredById", TypeName = "bigint")]
        public long? ObsoleteReferredById { get; set; }

        [Column("CorrectReferredByIdEmployeeId", TypeName = "bigint")]
        public long? ReferredByEmployeeId { get; set; }

        [Obsolete]
        [Column("ManagerId", TypeName = "bigint")]
        public long? ObsoleteManagerId { get; set; }

        [Column("CorrectManagerIdEmployeeId", TypeName = "bigint")]
        public long? ManagerEmployeeId { get; set; }

        [Obsolete]
        [Column("LeaderId", TypeName = "bigint")]
        public long? ObsoleteLeaderId { get; set; }

        [Column("CorrectLeaderIdEmployeeId", TypeName = "bigint")]
        public long? LeaderEmployeeId { get; set; }

        [Column("TierLevelId", TypeName = "bigint")]
        public long? TierLevelId { get; set; }

        [Column("DrugTestFrequencyId", TypeName = "smallint")]
        public short DrugTestFrequencyId { get; set; }

        [Column("Notes", TypeName = "varchar(255)")]
        public string? Notes { get; set; }

        [Column("Image", TypeName = "varbinary(MAX)")]
        public byte[]? Image { get; set; }

        [Column("IsDeleted", TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Obsolete]
        [Column("CreatedById", TypeName = "bigint")]
        public long? ObsoleteCreatedById { get; set; }

        [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
        public long? CreatedByEmployeeId { get; set; }

        [Column("DateCreated", TypeName = "datetime2(7)")]
        public DateTime? DateCreated { get; set; }

        [Obsolete]
        [Column("UpdatedById", TypeName = "bigint")]
        public long? ObsoleteUpdatedById { get; set; }

        [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
        public long? UpdatedByEmployeeId { get; set; }

        [Column("DateUpdated", TypeName = "datetime2(7)")]
        public DateTime? DateUpdated { get; set; }

        [Column("CanUserSignIn", TypeName = "bit")]
        public bool CanUserSignIn { get; set; }

        [Column("EmployeeId", TypeName = "bigint")]
        public long LoginUserId { get; set; }

        [Required, StringLength(15)]
        [Column("CustomEmployeeId", TypeName = "varchar(15)")]
        public string CustomEmployeeId { get; set; } = null!;

        [Column("AvailableVacationHours", TypeName = "decimal(9,4)")]
        public decimal? AvailableVacationHours { get; set; }

        [Column("AvailableLostTimeHours", TypeName = "decimal(9,4)")]
        public decimal? AvailableLostTimeHours { get; set; }

        [Column("LogId", TypeName = "bigint")]
        public long? LogId { get; set; }

        [Column("SignedHandbookDocument", TypeName = "nvarchar(500)")]
        public string? SignedHandbookDocument { get; set; }

        [Column("SignedHandbookDocumentUploadDateTime", TypeName = "datetime2(7)")]
        public DateTime? SignedHandbookDocumentUploadDateTime { get; set; }

        [Column("NewHirePaperworkPdf", TypeName = "nvarchar(500)")]
        public string? NewHirePaperworkPdf { get; set; }

        [Column("NewHirePaperworkPdfUploadedDateTime", TypeName = "datetime2(7)")]
        public DateTime? NewHirePaperworkPdfUploadedDateTime { get; set; }

        [Column("NormalWorkHours", TypeName = "decimal(7,2)")]
        public decimal? NormalWorkHours { get; set; }

        [Column("ShiftStartTime", TypeName = "datetime2(7)")]
        public DateTime? ShiftStartTime { get; set; }

        [Column("ShiftEndTime", TypeName = "datetime2(7)")]
        public DateTime? ShiftEndTime { get; set; }

        [Column("IsSignedHandbookRequired", TypeName = "bit")]
        public bool IsSignedHandbookRequired { get; set; }

        [Obsolete]
        [Column("SecondaryLeaderId", TypeName = "bigint")]
        public long? ObsoleteSecondaryLeaderId { get; set; }

        [Column("CorrectSecondaryLeaderIdEmployeeId", TypeName = "bigint")]
        public long? SecondaryLeaderEmployeeId { get; set; }

        [Column("SecondaryJobId", TypeName = "bigint")]
        public long? SecondaryJobId { get; set; }

        [Column("IsLcaPlanActive", TypeName = "bit")]
        public bool IsLcaPlanActive { get; set; }

        [Obsolete]
        [Column("DeskLocationId", TypeName = "bigint")]
        public long? DeskLocationId { get; set; }

        [Column("ShirtSizeId", TypeName = "bigint")]
        public long? ShirtSizeId { get; set; }

        [Column("CompanyNameId", TypeName = "smallint")]
        public short CompanyNameId { get; set; }

        [Column("IsPersonFromOutsideCompany", TypeName = "bit")]
        public bool IsPersonFromOutsideCompany { get; set; }

        [Column("HasPhone", TypeName = "bit")]
        public bool HasPhone { get; set; }

        [Column("HasLaptop", TypeName = "bit")]
        public bool HasLaptop { get; set; }

        [Column("HasVpnAccess", TypeName = "bit")]
        public bool HasVpnAccess { get; set; }

        [Column("EmployeeQualityLevelStatus", TypeName = "bit")]
        public bool? EmployeeQualityLevelStatus { get; set; }

        [Column("EmployeeQualityLevelStatusId", TypeName = "bigint")]
        public long? EmployeeQualityLevelStatusId { get; set; }

        [Column("MailingAddress", TypeName = "varchar(500)")]
        public string? MailingAddress { get; set; }

        [Column("MailingAddressStateId", TypeName = "bigint")]
        public long? MailingAddressStateId { get; set; }

        [Column("MailingAddressCityId", TypeName = "bigint")]
        public long? MailingAddressCityId { get; set; }

        [Column("MailingAddressZipCode", TypeName = "varchar(255)")]
        public string? MailingAddressZipCode { get; set; }

        [Column("MailingAddressStreetAddress", TypeName = "varchar(255)")]
        public string? MailingAddressStreetAddress { get; set; }

        [Column("CountryCode", TypeName = "varchar(2)")]
        public string? CountryCode { get; set; }
        [Column("EmployeeDeskStatusId", TypeName = "bigint")]
        public long? EmployeeDeskStatusId { get; set; }
        [NotMapped]
        public string FullName => $"{LastName}, {FirstName}";
    }
}
