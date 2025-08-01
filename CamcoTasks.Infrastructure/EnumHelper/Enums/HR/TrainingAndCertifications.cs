namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum TrainingAndCertifications
{
    [CustomDisplay("ReCertification")] ReCertification = 1,

    [CustomDisplay("NewTraining")] NewTraining = 2,

    [CustomDisplay("RetrainingRequired")] RetrainingRequired = 3,

    [CustomDisplay("NewCertification")] NewCertification = 4
}