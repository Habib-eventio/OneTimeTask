using System;
using System.ComponentModel.DataAnnotations;

namespace CamcoTasks.Infrastructure.CustomModels.IT;

public class OpenTicketsModel
{
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
    public DateTime Date { get; set; }

    public int TicketCount { get; set; }
}