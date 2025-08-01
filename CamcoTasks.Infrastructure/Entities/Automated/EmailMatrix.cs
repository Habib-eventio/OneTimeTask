using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Automated;

[Table("Automated_EmailMatrix")]
public class EmailMatrix
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Browsable(false)]
    [Column("EmployeeID", TypeName = "int")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public int? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("Employee", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Obsolete("This is Obsolete. Please refer to HR_Employees Table to Get Employee Name")]
    public string EmployeeName { get; set; }

    [Column("Email", TypeName = "nvarchar(MAX)")]
    [Required]
    [MaxLength]
    public string Email { get; set; }

    [Column("MoveTickets", TypeName = "bit")]
    public bool CanReceiveMoveTicketEmails { get; set; }

    [Column("POStatus", TypeName = "bit")] public bool CanReceivePurchaseOrderStatusEmails { get; set; }

    [Column("DailyPOTotals", TypeName = "bit")]
    public bool CanReceiveDailyPurchaseOrderTotalsEmails { get; set; }

    [Column("KanbanIR", TypeName = "bit")] public bool CanReceiveKanbanIREmails { get; set; }

    [Column("KanbanITT", TypeName = "bit")]
    public bool CanReceiveKanbanITTEmails { get; set; }

    [Column("KanbanChina", TypeName = "bit")]
    public bool CanReceiveKanbanChinaEmails { get; set; }

    [Column("KanbanDresser", TypeName = "bit")]
    public bool CanReceiveKanbanDresserEmails { get; set; }

    [Column("KanbanUniversal", TypeName = "bit")]
    public bool CanReceiveKanbanUniversalEmails { get; set; }

    [Column("KanbanRaymond", TypeName = "bit")]
    public bool CanReceiveKanbanRaymondEmails { get; set; }

    [Column("Tasks", TypeName = "bit")] public bool CanReceiveTasksEmails { get; set; }

    [Column("ReocurringTasks", TypeName = "bit")]
    public bool CanReceiveRecurringTasksEmails { get; set; }

    [Column("UnpackedCOs", TypeName = "bit")]
    public bool CanReceiveUnpackedCustomerOrdersEmails { get; set; }

    [Column("PastDuePORequests", TypeName = "bit")]
    public bool CanReceivePastDuePurchaseOrderRequestsEmails { get; set; }

    [Column("DailyTicketEmails", TypeName = "bit")]
    public bool CanReceiveDailyTicketEmails { get; set; }

    [Column("JobType", TypeName = "nvarchar(MAX)")]
    [Required]
    [MaxLength]
    public string JobType { get; set; }
}