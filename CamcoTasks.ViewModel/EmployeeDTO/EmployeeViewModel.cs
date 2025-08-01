using CamcoTasks.ViewModels.Department;
using CamcoTasks.ViewModels.LoginDTO;
using System;

namespace CamcoTasks.ViewModels.EmployeeDTO
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public long? GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public bool IsFullTimeJob { get; set; }
        public short? ShiftId { get; set; }
        public long DepartmentId { get; set; }
        public string? ShiftDifferential { get; set; }
        public long JobId { get; set; }
        public short? EmploymentStatusId { get; set; }
        public long EmployeeTypeId { get; set; }
        public bool? IsSubContractor { get; set; }
        public bool IsInternational { get; set; }
        public string? Address { get; set; }
        public string? CellNumber { get; set; }
        public string? HomePhoneNumber { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public string? ZipCode { get; set; }
        public string? StreetAddress { get; set; }
        public bool IsPaymentTypeSalary { get; set; }
        public string? AnnualSalary { get; set; }
        public string? BaseHourlyRate { get; set; }
        public string? OtherHourlyBonus { get; set; }
        public string? OtherHourlyBonusDescription { get; set; }
        public decimal? PercentOfDirectTime { get; set; }
        public decimal? AnnualLostTimeAllocation { get; set; }
        public bool IsCustomVacations { get; set; }
        public decimal? AnnualVacationAllocationHours { get; set; }
        public long? ReferredById { get; set; }
        public long? ManagerId { get; set; }
        public long? LeaderId { get; set; }
        public long? TierLevelId { get; set; }
        public short DrugTestFrequencyId { get; set; }
        public string? Notes { get; set; }
        public byte[]? Image { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool CanUserSignIn { get; set; }
        public string CustomEmployeeId { get; set; }
        public decimal? AvailableVacationHours { get; set; }
        public decimal? AvailableLostTimeHours { get; set; }
        public long? LogId { get; set; }
        public string? SignedHandbookDocument { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public long LoginUserId { get; set; }
        public DateTime? SignedHandbookDocumentUploadDateTime { get; set; }
        public string? NewHirePaperworkPdf { get; set; }
        public DateTime? NewHirePaperworkPdfUploadedDateTime { get; set; }
        public decimal? NormalWorkHours { get; set; }
        public DateTime? ShiftStartTime { get; set; }
        public long? EmployeeDeskStatusId { get; set; }
        public DateTime? ShiftEndTime { get; set; }
        public string DepartmentAbbreviatedName { get; set; }
        public bool IsManager { get; set; }

        public bool? IsAuditor { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public long? ShirtSizeId { get; set; }

        public virtual DepartmentViewModel Department { get; set; }
        public virtual LoginViewModel Login { get; set; }
    }
}
