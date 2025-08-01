using CamcoTasks.Infrastructure.EnumHelper;

namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;

public enum AboveAndBeyondFieldType
{
    [CustomDisplay("DateEntered")] DateEntered = 1,

    [CustomDisplay("EnteredBy")] EnteredBy = 2,

    [CustomDisplay("NoteType")] NoteType = 3,

    [CustomDisplay("Date")] Date = 4,

    [CustomDisplay("EmployeeId")] EmployeeId = 5,

    [CustomDisplay("Note")] Note = 6,

    [CustomDisplay("EmployeeNotified")] EmployeeNotified = 7,

    [CustomDisplay("EmployeeManagerNotified")]
    EmployeeManagerNotified = 8
}