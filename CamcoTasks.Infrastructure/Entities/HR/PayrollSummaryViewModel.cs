using System;

namespace CamcoTasks.Infrastructure.CustomModels.HR;

public class PayrollSummaryViewModel
{
    public DateTime WeekStartDate { get; set; }

    public decimal TotalWeeklyGrossPayment { get; set; }
}