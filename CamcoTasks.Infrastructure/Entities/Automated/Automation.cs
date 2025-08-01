using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Automated;

[Table("Automated_Automations")]
public class Automation
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("AutomationType", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Obsolete("This is Now Obsolete. Instead Refer to EmailTypeId")]
    public string Type { get; set; }

    [Column("EmailTypeId", TypeName = "int")]
    public int EmailTypeId { get; set; }

    [Column("Name", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Obsolete("This is Now Obsolete. Instead Refer to EmailName in HR_EmailTypes")]
    public string Name { get; set; }

    [Column("TimerTick", TypeName = "int")]
    public int TimerTicks { get; set; }

    [Column("EarliestTime", TypeName = "time(7)")]
    public TimeSpan EarliestTimeToRun { get; set; }

    [Column("LatestTime", TypeName = "time(7)")]
    public TimeSpan LatestTimeToRun { get; set; }

    [Column("LastRun", TypeName = "datetime2(0)")]
    public DateTime LastRunDate { get; set; }

    [Column("IsEnabled", TypeName = "bit")]
    public bool IsEnabled { get; set; }

    [Column("WeekdayToRun", TypeName = "nvarchar(MAX)")]
    [Required]
    [MaxLength]
    public string WeekdayToRun { get; set; }

    [Column("NeedsRestart", TypeName = "bit")]
    public bool IsRestartNeeded { get; set; }

    [Column("Monday", TypeName = "bit")] public bool IsMondaySelected { get; set; }

    [Column("Tuesday", TypeName = "bit")] public bool IsTuesdaySelected { get; set; }

    [Column("Wednesday", TypeName = "bit")]
    public bool IsWednesdaySelected { get; set; }

    [Column("Thursday", TypeName = "bit")] public bool IsThursdaySelected { get; set; }

    [Column("Friday", TypeName = "bit")] public bool IsFridaySelected { get; set; }

    [Column("Saturday", TypeName = "bit")] public bool IsSaturdaySelected { get; set; }

    [Column("Sunday", TypeName = "bit")] public bool IsSundaySelected { get; set; }

    [Column("IsRunningNow", TypeName = "bit")]
    public bool IsRunningNow { get; set; }
}