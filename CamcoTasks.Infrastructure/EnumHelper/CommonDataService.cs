using CamcoTasks.Infrastructure.Common.EnumHelper;
using CamcoTasks.Infrastructure.EnumHelper;
using CamcoTasks.Infrastructure.EnumHelper.Enums.HR;
using CamcoTasks.Infrastructure.EnumHelper.Enums.Task;
using CamcoTasks.Infrastructure.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CamcoTasks.Infrastructure.EnumHelper;

public class CommonDataService : ICommonDataService
{
    public List<TModel> GetComboBoxModels<TEnum, TModel, TId>()
        where TEnum : Enum
        where TModel : ComboBoxModel<TId>, new()
    {
        var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        return enumValues.Select(value =>
        {
            var enumField = value.GetType().GetField(value.ToString());
            var displayText = enumField.GetCustomAttributes(typeof(CustomDisplayAttribute), false)
                .FirstOrDefault() is CustomDisplayAttribute displayAttribute
                ? displayAttribute.DisplayText
                : value.ToString();
            return new TModel
            {
                Id = (TId)Convert.ChangeType(value, typeof(TId)),
                Text = displayText
            };
        }).ToList();
    }

    public List<ComboBoxModel<short>> GetJobTitleTypes()
    {
        return GetComboBoxModels<JobTitleType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> GetJobDescriptionsTypes()
    {
        return GetComboBoxModels<JobDescriptionType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> GetCompanies()
    {
        return GetComboBoxModels<CompanyName, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<long>> PopulateStatusTypes()
    {
        return GetComboBoxModels<ConfirmationType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<short>> GetAboveAndBeyondRecognitionNoteTypes()
    {
        return GetComboBoxModels<AboveAndBeyondRecognitionNoteType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateDisciplineType()
    {
        return GetComboBoxModels<DisciplinePhaseType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateDrugTestResultType()
    {
        return GetComboBoxModels<DrugTestResultType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateDrugTestReasonType()
    {
        return GetComboBoxModels<DrugTestReasonType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<long>> PopulateLostTimeAdjustmentType()
    {
        return GetComboBoxModels<LostTimeAdjustmentType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulateVacationTimeAdjustmentType()
    {
        return GetComboBoxModels<VacationTimeAdjustmentType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<short>> PopulateLoanType()
    {
        return GetComboBoxModels<LoanType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<long>> PopulateGenderTypes()
    {
        return GetComboBoxModels<GenderType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulateJobTypes()
    {
        return GetComboBoxModels<JobType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulatePaymentTypes()
    {
        return GetComboBoxModels<PaymentType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulateVacationDaysBaseTypes()
    {
        return GetComboBoxModels<VacationDaysBaseType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulateEmploymentTypes()
    {
        return GetComboBoxModels<EmploymentType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<short>> PopulateDrugTestFrequencyType()
    {
        return GetComboBoxModels<DrugTestFrequencyType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateInsuranceType()
    {
        return GetComboBoxModels<InsuranceType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateBreakTypes()
    {
        return GetComboBoxModels<BreakType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<long>> GetPriorities()
    {
        return GetComboBoxModels<PriorityType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulateVoluntaryReasons()
    {
        return GetComboBoxModels<VoluntaryReason, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulateInvoluntaryReasons()
    {
        return GetComboBoxModels<InvoluntaryReason, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<long>> PopulateDisciplineTypes()
    {
        return GetComboBoxModels<DisciplineType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<short>> PopulateOfficialDocumentTypes()
    {
        return GetComboBoxModels<OfficialDocumentType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulatePipFollowUpFrequencies()
    {
        return GetComboBoxModels<PipFollowupFrequencyType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateAutomaticEmailFrequencies()
    {
        return GetComboBoxModels<AutomaticEmailFrequency, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateAutomaticEmailDayOfWeek()
    {
        return GetComboBoxModels<DayOfWeekEnum, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<long>> PopulateQuestionTypes()
    {
        return GetComboBoxModels<QuestionType, ComboBoxModel<long>, long>();
    }

    public List<ComboBoxModel<short>> PopulateTrainingAndCertifications()
    {
        return GetComboBoxModels<TrainingAndCertifications, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateInsuranceTypes()
    {
        return GetComboBoxModels<IncidentInsuranceType, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateCertificationOrigins()
    {
        return GetComboBoxModels<CertificationOrigin, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateDrugTestRequestReasons()
    {
        return GetComboBoxModels<DrugTestRequestReasons, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateRequestedTrainingAndCertifications()
    {
        return GetComboBoxModels<RequestedTrainingAndCertification, ComboBoxModel<short>, short>();
    }

    public List<ComboBoxModel<short>> PopulateLeaveTypes()
    {
        return GetComboBoxModels<LeaveType, ComboBoxModel<short>, short>();
    }

    public string GetJobTitleTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<JobTitleType, short>(id);
    }

    public string GetJobDescriptionsTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<JobDescriptionType, short>(id);
    }

    public string GetStatusTypesById(long id)
    {
        return EnumHelper.GetTextFromEnum<ConfirmationType, long>(id);
    }

    public string GetLeaveTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<LeaveType, short>(id);
    }

    public string GetAboveAndBeyondNoteTypeById(long id)
    {
        return EnumHelper.GetTextFromEnum<AboveAndBeyondRecognitionNoteType, long>(id);
    }

    public string GetDisciplineLevelById(long id)
    {
        return EnumHelper.GetTextFromEnum<DisciplinePhaseType, long>(id);
    }

    public string GetDisciplineTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<DisciplineType, short>(id);
    }

    public string GetOfficialDocumentTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<OfficialDocumentType, short>(id);
    }

    public string GetDrugTestFrequencyTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<DrugTestFrequencyType, short>(id);
    }

    public string GetDrugTestReasonTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<DrugTestReasonType, short>(id);
    }

    public string GetDrugTestResultTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<DrugTestResultType, short>(id);
    }

    public string GetInsuranceTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<InsuranceType, short>(id);
    }

    public string GetEmploymentTypeById(long id)
    {
        return EnumHelper.GetTextFromEnum<EmploymentType, long>(id);
    }

    public string GetReasonById(long id)
    {
        string result = EnumHelper.GetTextFromEnum<VoluntaryReason, long>(id);
        if (result == "Not Found")
        {
            return EnumHelper.GetTextFromEnum<InvoluntaryReason, long>(id);
        }

        return result;
    }

    public string GetPriorityById(long id)
    {
        return EnumHelper.GetTextFromEnum<PriorityType, long>(id);
    }

    public string GetLoanTypeById(long id)
    {
        return EnumHelper.GetTextFromEnum<LoanType, long>(id);
    }

    public string GetHoursChangeTypeById(long id)
    {
        return EnumHelper.GetTextFromEnum<HoursChangeType, long>(id);
    }

    public string GetShiftNameById(short id)
    {
        return EnumHelper.GetTextFromEnum<AssignedShiftType, short>(id);
    }

    public string GetBreakNameById(short id)
    {
        return EnumHelper.GetTextFromEnum<BreakType, short>(id);
    }

    public string GetJobTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<JobType, short>(id);
    }

    public short GetTypeIdByTypeName(string typeName)
    {
        return (short)EnumHelper.GetEnumValueFromText<DisciplineType, DisciplineType>(typeName);
    }

    public short GetOfficialDocumentTypeIdByOfficialDocumentTypeName(string officialDocumentTypeName)
    {
        return (short)EnumHelper.GetEnumValueFromText<OfficialDocumentType, OfficialDocumentType>(
            officialDocumentTypeName);
    }

    public string GetEmailGroupFrequencyById(short id)
    {
        return EnumHelper.GetTextFromEnum<AutomaticEmailFrequency, short>(id);
    }

    public string GetDayByDayId(short id)
    {
        return EnumHelper.GetTextFromEnum<DayOfWeekEnum, short>(id);
    }

    public string GetQuestionTypeById(long id)
    {
        return EnumHelper.GetTextFromEnum<QuestionType, long>(id);
    }

    public string GetCategoryNameByCategoryId(short id)
    {
        return EnumHelper.GetTextFromEnum<EmailCategory, short>(id);
    }

    public string GetChangeTypeByChangeId(short id)
    {
        return EnumHelper.GetTextFromEnum<ChangeRequestType, short>(id);
    }

    public string GetInsuranceTypeNameById(short id)
    {
        return EnumHelper.GetTextFromEnum<IncidentInsuranceType, short>(id);
    }

    public string GetCertificationOriginById(short id)
    {
        return EnumHelper.GetTextFromEnum<CertificationOrigin, short>(id);
    }

    public string GetTrainingAndCertificationById(short id)
    {
        return EnumHelper.GetTextFromEnum<TrainingAndCertifications, short>(id);
    }

    public string GetRequestedCertificationById(short id)
    {
        return EnumHelper.GetTextFromEnum<RequestedTrainingAndCertification, short>(id);
    }

    public string GetDrugTestRequestReasonById(short id)
    {
        return EnumHelper.GetTextFromEnum<DrugTestRequestReasons, short>(id);
    }

    public string GetLcaPlaPhaseByPhaseId(short id)
    {
        return EnumHelper.GetTextFromEnum<LcaPlanPhase, short>(id);
    }

    public string GetChangeRequestStatusByStatusId(short id)
    {
        return EnumHelper.GetTextFromEnum<EmployeeChangeRequestStateEnum, short>(id);
    }

    public string GetResponsiblePersonById(short id)
    {
        return EnumHelper.GetTextFromEnum<ResponsiblePersonType, short>(id);
    }

    public string GetPipFollowupFrequencyTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<PipFollowupFrequencyType, short>(id);
    }

    public string GetFormTypeById(long id)
    {
        return EnumHelper.GetTextFromEnum<FormTypes, long>(id);
    }

    public string GetFormAndInformationTypeById(short id)
    {
        return EnumHelper.GetTextFromEnum<FormAndInformationType, short>(id);
    }

    public string GetApplicationStatusGroupById(int id)
    {
        return EnumHelper.GetTextFromEnum<ApplicationTrackingGroup, int>(id);
    }

    public List<ComboBoxModel<int>> PopulateStatusType()
    {
        return GetComboBoxModels<StatusType, ComboBoxModel<int>, int>();
    }

    public List<ComboBoxModel<int>> PopulateApplicationTrackingGroups()
    {
        return GetComboBoxModels<ApplicationTrackingGroup, ComboBoxModel<int>, int>();
    }

}