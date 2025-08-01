using System;
using System.Collections.Generic;

namespace CamcoTasks.Infrastructure.Entities.TimeClock;

public class EmployeeWeeklyHoursReportViewModel
{
    public string EmployeeId { get; set; }

    public string EmployeeLastName { get; set; }

    public string EmployeeFullName { get; set; }

    public string ApprovedOrNot { get; set; }

    public double? RegularHours { get; set; }

    public double? VacationHours { get; set; }

    public double? LostTimeHours { get; set; }

    public double? BusinessTimeHours { get; set; }

    public double? ExtraTimeCategoriesHours { get; set; }

    public List<EmployeeCheckInOut> RegularHoursDetails { get; set; } = new();

    public List<EmployeeVacationRequests> VacationHoursDetails { get; set; } = new();

    public List<LostTimeReport> LostTimeHoursDetails { get; set; } = new();

    //public List<BusinessTime> BusinessTimeDetails { get; set; } = new();

    //public List<EmployeeExtraTimeRecord> ExtraTimeCategoriesDetails { get; set; } = new();

    public class EmployeeVacationRequests
    {
        public int Id { get; set; }

        public DateTime? RequestedDate { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public double? TotalHours { get; set; }

        public bool? IsApproved { get; set; }

        public string Reason { get; set; }

        public string DisapprovedReason { get; set; }

        public string AvailableVacationHours { get; set; }

        public int? VacationId { get; set; }

        public string IsDeleteOrEdit { get; set; }

        public List<VacationRequestNoteViewModel> Notes = new();
    }

    public class LostTimeReport
    {
        public string EmployeeName { get; set; }

        public string EmployeeId { get; set; }

        public string LostDate { get; set; }

        public double? TotalHours { get; set; }

        public string Reason { get; set; }
    }

    public class EmployeeCheckInOut
    {
        public int CheckInOutId { get; set; }

        public string EmployeeId { get; set; }

        public string CheckInTime { get; set; }

        public string CheckOutTime { get; set; }

        public string Date { get; set; }

        public string Day { get; set; }

        public string Edit { get; set; }

        public double Hours { get; set; }
    }

    public class BusinessTime
    {
        public int Id { get; set; }

        public int CheckInOutId { get; set; }

        public string BusinessDate { get; set; }

        public string BusinessDay { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string LeftTime { get; set; }

        public string ReturnedTime { get; set; }

        public string PlaceOfGoing { get; set; }

        public double? Hours { get; set; }
    }

    //public class EmployeeExtraTimeRecord
    //{
    //    public int ExtraTimeId { get; set; }

    //    public string Date { get; set; }

    //    public string ExtraTimeCategory { get; set; }

    //    public double TotalHours { get; set; }

    //    public string EmployeeId { get; set; }

    //    public string EmployeeName { get; set; }

    //    public string Day { get; set; }
    //}

    public class VacationRequestNoteViewModel
    {
        public int Id { get; set; }

        public int? RequestId { get; set; }

        public string OptionalNote { get; set; }

        public string SubmittedBy { get; set; }

        public DateTime? DateEntered { get; set; }
    }
}