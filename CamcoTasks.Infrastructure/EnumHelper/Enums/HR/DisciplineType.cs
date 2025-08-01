namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum DisciplineType
{
    [CustomDisplay("Attendance")] Attendance = 1,

    [CustomDisplay("General")] General = 2,

    [CustomDisplay("Quality")] Quality = 3,

    [CustomDisplay("Safety")] Safety = 4,

    [CustomDisplay("Timesheet / Cell Phone")]
    TimeSheetCellPhone = 5,

    [CustomDisplay("5S")] FiveS = 6
}