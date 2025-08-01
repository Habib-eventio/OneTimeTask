namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum ApplicationTrackingGroup
{
    [CustomDisplay("Pending Applications")]
    PendingApplications = 1,

    [CustomDisplay("Applications In Process")]
    ApplicationsInProcess = 2,

    [CustomDisplay("Applicants Not Hired")]
    ApplicantsNotHired = 3,

    [CustomDisplay("Applicants Hired")] ApplicantsHired = 4
}