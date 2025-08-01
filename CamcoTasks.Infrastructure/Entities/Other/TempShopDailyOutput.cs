using System;

namespace CamcoTasks.Infrastructure.Entities;
public class TempShopDailyOutput
{
    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    public decimal Expenses { get; set; }

    public decimal PL { get; set; }
}