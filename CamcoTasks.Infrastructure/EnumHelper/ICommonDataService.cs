using CamcoTasks.Infrastructure.ViewModel.Shared;
using System.Collections.Generic;

namespace CamcoTasks.Infrastructure.Common.EnumHelper;

public interface ICommonDataService
{
    List<ComboBoxModel<short>> GetJobTitleTypes();

    List<ComboBoxModel<short>> GetJobDescriptionsTypes();

    List<ComboBoxModel<short>> GetCompanies();

    List<ComboBoxModel<long>> PopulateStatusTypes();

    List<ComboBoxModel<short>> GetAboveAndBeyondRecognitionNoteTypes();

    List<ComboBoxModel<short>> PopulateDisciplineType();

    List<ComboBoxModel<short>> PopulateDrugTestResultType();

    List<ComboBoxModel<short>> PopulateDrugTestReasonType();

    List<ComboBoxModel<long>> PopulateLostTimeAdjustmentType();

    List<ComboBoxModel<long>> PopulateVacationTimeAdjustmentType();

    List<ComboBoxModel<short>> PopulateLoanType();

    List<ComboBoxModel<long>> PopulateGenderTypes();

    List<ComboBoxModel<long>> PopulateJobTypes();

    List<ComboBoxModel<long>> PopulatePaymentTypes();

    List<ComboBoxModel<long>> PopulateVacationDaysBaseTypes();

    List<ComboBoxModel<long>> PopulateEmploymentTypes();

    List<ComboBoxModel<short>> PopulateDrugTestFrequencyType();

    List<ComboBoxModel<short>> PopulateInsuranceType();

    List<ComboBoxModel<short>> PopulateBreakTypes();

    List<ComboBoxModel<long>> GetPriorities();

    List<ComboBoxModel<long>> PopulateVoluntaryReasons();

    List<ComboBoxModel<long>> PopulateInvoluntaryReasons();

    List<ComboBoxModel<long>> PopulateDisciplineTypes();

    List<ComboBoxModel<short>> PopulateOfficialDocumentTypes();

    List<ComboBoxModel<short>> PopulatePipFollowUpFrequencies();

    List<ComboBoxModel<short>> PopulateAutomaticEmailFrequencies();

    List<ComboBoxModel<short>> PopulateAutomaticEmailDayOfWeek();

    List<ComboBoxModel<long>> PopulateQuestionTypes();

    List<ComboBoxModel<short>> PopulateTrainingAndCertifications();

    List<ComboBoxModel<short>> PopulateInsuranceTypes();

    List<ComboBoxModel<short>> PopulateCertificationOrigins();

    List<ComboBoxModel<short>> PopulateDrugTestRequestReasons();

    List<ComboBoxModel<short>> PopulateRequestedTrainingAndCertifications();

    List<ComboBoxModel<short>> PopulateLeaveTypes();

    List<ComboBoxModel<int>> PopulateStatusType();

    List<ComboBoxModel<int>> PopulateApplicationTrackingGroups();

    string GetJobTitleTypeById(short id);

    string GetJobDescriptionsTypeById(short id);

    string GetStatusTypesById(long id);

    string GetLeaveTypeById(short id);

    string GetAboveAndBeyondNoteTypeById(long id);

    string GetDisciplineLevelById(long id);

    string GetDisciplineTypeById(short id);

    string GetOfficialDocumentTypeById(short id);

    string GetDrugTestFrequencyTypeById(short id);

    string GetDrugTestReasonTypeById(short id);

    string GetDrugTestResultTypeById(short id);

    string GetInsuranceTypeById(short id);

    string GetEmploymentTypeById(long id);

    string GetReasonById(long id);

    string GetPriorityById(long id);

    string GetLoanTypeById(long id);

    string GetHoursChangeTypeById(long id);

    string GetShiftNameById(short id);

    string GetBreakNameById(short id);

    string GetJobTypeById(short id);

    short GetTypeIdByTypeName(string typeName);

    short GetOfficialDocumentTypeIdByOfficialDocumentTypeName(string officialDocumentTypeName);

    string GetEmailGroupFrequencyById(short id);

    string GetDayByDayId(short id);

    string GetQuestionTypeById(long id);

    string GetCategoryNameByCategoryId(short id);

    string GetChangeTypeByChangeId(short id);

    string GetInsuranceTypeNameById(short id);

    string GetCertificationOriginById(short id);

    string GetTrainingAndCertificationById(short id);

    string GetRequestedCertificationById(short id);

    string GetDrugTestRequestReasonById(short id);

    string GetLcaPlaPhaseByPhaseId(short id);

    string GetChangeRequestStatusByStatusId(short id);

    string GetResponsiblePersonById(short id);

    string GetPipFollowupFrequencyTypeById(short id);

    string GetFormAndInformationTypeById(short id);

    string GetFormTypeById(long id);

    string GetApplicationStatusGroupById(int id);

}