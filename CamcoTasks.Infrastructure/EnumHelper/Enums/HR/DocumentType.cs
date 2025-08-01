namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum DocumentType
{
    [CustomDisplay("Pdf")] Pdf = 1,

    [CustomDisplay("WordDocument")] WordDocument = 2,

    [CustomDisplay("Image")] Image = 3,

    [CustomDisplay("Excel")] Excel = 4
}