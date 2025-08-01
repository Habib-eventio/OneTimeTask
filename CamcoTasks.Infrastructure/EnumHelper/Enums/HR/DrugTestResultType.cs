namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum DrugTestResultType
{
    [CustomDisplay("Negative")] 
    Negative = 1,

    [CustomDisplay("Positive-Marijuana Only")]
    PositiveMarijuanaOnly = 2,

    [CustomDisplay("Positive-Other Drugs")]
    PositiveOtherDrugs = 3,

    [CustomDisplay("Others")] 
    Others = 4,

    [CustomDisplay("Employee Not At Work")] 
    EmployeeNotAtWork = 5,

    [CustomDisplay("Positive-Other Drugs-No Prescription")]
    PositiveOtherDrugsNoPrescription = 6,

    [CustomDisplay("Positive-Other Drugs-Prescribed")]
    PositiveOtherDrugsPrescribed = 7,

    [CustomDisplay("Employee Refused To Do Test")] 
    EmployeeRefusedToDoTest = 8
}