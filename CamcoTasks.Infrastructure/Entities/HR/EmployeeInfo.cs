using System;

namespace CamcoTasks.Infrastructure.Entities;

public class EmployeeInfo
{
    public long HrEmployeeId { get; set; }

    public int ShiftId { get; set; }

    public string EmployeeId { get; set; }

    public string FullName { get; set; }

    public int ExtraTimeCategoryFieldId { get; set; }

    public string ExtraTimeCategoryField { get; set; }

    public string EmployeeAbbreviatedName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Department { get; set; }

    public string EmployeeEmail { get; set; }

    public string DepartmentAbbreviatedNames { get; set; }

    public string ExpectedClockInTime { get; set; }

    public string FirstClockInTime { get; set; }

    public string ExpectedClockOutTime { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsRemotelyWorking { get; set; }

    public bool IsSelected { get; set; }

    public bool IsManager { get; set; }

    public DateTime? EmailSentDate { get; set; }

    public DateTime? ShiftStartTime { get; set; }

    public DateTime? ShiftEndTime { get; set; }

    public long DepartmentId { get; set; }

    public string StatusBubble { get; set; }

    public string EmployeeStatus { get; set; }

    public string PlaceOfGoing { get; set; }

    public bool IsWorkingThroughUpwork { get; set; }

    public bool IsLate { get; set; }

    public int TotalCheckSheetsPrepared { get; set; }
}