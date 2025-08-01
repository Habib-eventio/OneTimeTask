using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;
using System;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.EmployeeDTO
{
    public static class EmployeeDTONew
    {
        public static Employee Map(EmployeeViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new Employee
            {
                Id = Convert.ToInt32(viewModel.Id),
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DepartmentId = viewModel.DepartmentId,
                IsDeleted = viewModel.IsDeleted,
                IsActive = viewModel.IsActive,
                IsAuditor = viewModel.IsAuditor,
                SocialSecurityNumber = viewModel.SocialSecurityNumber,
                GenderId = viewModel.GenderId,
                DateOfBirth = viewModel.DateOfBirth,
                DateOfHire = viewModel.DateOfHire,
                ProbationEndDate = viewModel.ProbationEndDate,
                IsFullTimeJob = viewModel.IsFullTimeJob,
                ShiftId = viewModel.ShiftId,
                ShiftDifferential = viewModel.ShiftDifferential,
                JobId = viewModel.JobId,
                EmploymentStatusId = viewModel.EmploymentStatusId,
                EmployeeTypeId = viewModel.EmployeeTypeId,
                IsInternational = viewModel.IsInternational,
                Address = viewModel.Address,
                CellNumber = viewModel.CellNumber,
                HomePhoneNumber = viewModel.HomePhoneNumber,
                StateId = viewModel.StateId,
                CityId = viewModel.CityId,
                ZipCode = viewModel.ZipCode,
                StreetAddress = viewModel.StreetAddress,
                IsPaymentTypeSalary = viewModel.IsPaymentTypeSalary,
                AnnualSalary = viewModel.AnnualSalary,
                BaseHourlyRate = viewModel.BaseHourlyRate,
                OtherHourlyBonus = viewModel.OtherHourlyBonus,
                OtherHourlyBonusDescription = viewModel.OtherHourlyBonusDescription,
                PercentOfDirectTime = viewModel.PercentOfDirectTime,
                AnnualLostTimeAllocation = viewModel.AnnualLostTimeAllocation,
                IsCustomVacations = viewModel.IsCustomVacations,
                AnnualVacationAllocationHours = viewModel.AnnualVacationAllocationHours,
                ReferredByEmployeeId = viewModel.ReferredById,
                ManagerEmployeeId = viewModel.ManagerId,
                LeaderEmployeeId = viewModel.LeaderId,
                TierLevelId = viewModel.TierLevelId,
                DrugTestFrequencyId = viewModel.DrugTestFrequencyId,
                Notes = viewModel.Notes,
                Image = viewModel.Image,
                CreatedByEmployeeId = viewModel.CreatedById,
                DateCreated = viewModel.DateCreated,
                UpdatedByEmployeeId = viewModel.UpdatedById,
                DateUpdated = viewModel.DateUpdated,
                CanUserSignIn = viewModel.CanUserSignIn,
                CustomEmployeeId = viewModel.CustomEmployeeId,
                AvailableVacationHours = viewModel.AvailableVacationHours,
                AvailableLostTimeHours = viewModel.AvailableLostTimeHours,
                LogId = viewModel.LogId,
                SignedHandbookDocument = viewModel.SignedHandbookDocument,
                LoginUserId = viewModel.LoginUserId,
                SignedHandbookDocumentUploadDateTime = viewModel.SignedHandbookDocumentUploadDateTime,
                NewHirePaperworkPdf = viewModel.NewHirePaperworkPdf,
                NewHirePaperworkPdfUploadedDateTime = viewModel.NewHirePaperworkPdfUploadedDateTime,
                NormalWorkHours = viewModel.NormalWorkHours,
                ShiftStartTime = viewModel.ShiftStartTime,
                EmployeeDeskStatusId = viewModel.EmployeeDeskStatusId,
                ShiftEndTime = viewModel.ShiftEndTime,
                ShirtSizeId = viewModel.ShirtSizeId
            };
        }

        public static EmployeeViewModel Map(Employee entity)
        {
            if (entity == null) { return null; }

            return new EmployeeViewModel
            {
                Id = entity.Id,
                //Email = entity.LoginUser.Email,
                //UserName = entity.LoginUser.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DepartmentId = entity.DepartmentId,
                FullName = entity.FullName,
                IsDeleted = entity.IsDeleted,
                IsActive = entity.IsActive,
                IsAuditor = entity.IsAuditor,
                SocialSecurityNumber = entity.SocialSecurityNumber,
                GenderId = entity.GenderId,
                DateOfBirth = entity.DateOfBirth,
                DateOfHire = entity.DateOfHire,
                ProbationEndDate = entity.ProbationEndDate,
                IsFullTimeJob = entity.IsFullTimeJob,
                ShiftId = entity.ShiftId,
                ShiftDifferential = entity.ShiftDifferential,
                JobId = entity.JobId,
                EmploymentStatusId = entity.EmploymentStatusId,
                EmployeeTypeId = entity.EmployeeTypeId,
                IsInternational = entity.IsInternational,
                Address = entity.Address,
                CellNumber = entity.CellNumber,
                HomePhoneNumber = entity.HomePhoneNumber,
                StateId = entity.StateId,
                CityId = entity.CityId,
                ZipCode = entity.ZipCode,
                StreetAddress = entity.StreetAddress,
                IsPaymentTypeSalary = entity.IsPaymentTypeSalary,
                AnnualSalary = entity.AnnualSalary,
                BaseHourlyRate = entity.BaseHourlyRate,
                OtherHourlyBonus = entity.OtherHourlyBonus,
                OtherHourlyBonusDescription = entity.OtherHourlyBonusDescription,
                PercentOfDirectTime = entity.PercentOfDirectTime,
                AnnualLostTimeAllocation = entity.AnnualLostTimeAllocation,
                IsCustomVacations = entity.IsCustomVacations,
                AnnualVacationAllocationHours = entity.AnnualVacationAllocationHours,
                ReferredById = entity.ReferredByEmployeeId,
                ManagerId = entity.ManagerEmployeeId,
                LeaderId = entity.LeaderEmployeeId,
                TierLevelId = entity.TierLevelId,
                DrugTestFrequencyId = entity.DrugTestFrequencyId,
                Notes = entity.Notes,
                Image = entity.Image,
                CreatedById = entity.CreatedByEmployeeId,
                DateCreated = entity.DateCreated,
                UpdatedById = entity.UpdatedByEmployeeId,
                DateUpdated = entity.DateUpdated,
                CanUserSignIn = entity.CanUserSignIn,
                CustomEmployeeId = entity.CustomEmployeeId,
                AvailableVacationHours = entity.AvailableVacationHours,
                AvailableLostTimeHours = entity.AvailableLostTimeHours,
                LogId = entity.LogId,
                SignedHandbookDocument = entity.SignedHandbookDocument,
                LoginUserId = entity.LoginUserId,
                SignedHandbookDocumentUploadDateTime = entity.SignedHandbookDocumentUploadDateTime,
                NewHirePaperworkPdf = entity.NewHirePaperworkPdf,
                NewHirePaperworkPdfUploadedDateTime = entity.NewHirePaperworkPdfUploadedDateTime,
                NormalWorkHours = entity.NormalWorkHours,
                ShiftStartTime = entity.ShiftStartTime,
                EmployeeDeskStatusId = entity.EmployeeDeskStatusId,
                ShiftEndTime = entity.ShiftEndTime,
                ShirtSizeId = entity.ShirtSizeId,
            };

        }

        public static IEnumerable<EmployeeViewModel> Map(IEnumerable<Employee> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
        public static IEnumerable<Employee> Map(IEnumerable<EmployeeViewModel> viewModel)
        {
            if (viewModel == null) { yield break; }
            foreach (var item in viewModel)
            {
                yield return Map(item);
            }
        }
    }
}
