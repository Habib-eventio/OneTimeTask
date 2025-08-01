using System;

namespace CamcoTasks.Infrastructure.Entities;

public class WeeklyInvoice
{
    public DateTime ActualDate { get; set; }

    public string DateReported { get; set; }

    public decimal WeeklyAmount { get; set; }

    public string WeeklyAmountFormatted { get; set; }

    public decimal? AverageWeeks { get; set; }

    public string Year { get; set; }
}