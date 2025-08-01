using System;

namespace CamcoTasks.Infrastructure.Entities;

public class SelectedRMAs
{
    public DateTime RmaDate { get; set; }

    public int Id { get; set; }

    public string Reason { get; set; }
}