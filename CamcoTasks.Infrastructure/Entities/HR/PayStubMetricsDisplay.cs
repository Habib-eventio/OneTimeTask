using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PayStubMetricsDisplay")]
public class PayStubMetricsDisplay
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("JobTitleId", TypeName = "bigint")]
    public long JobTitleId { get; set; }

    [Column("IsDisplayFiveSOnTimeCompletionPercentage", TypeName = "bit")]
    public bool IsDisplayFiveSOnTimeCompletionPercentage { get; set; }

    [Column("IsDisplayJarvisClockInOnTimePercentage", TypeName = "bit")]
    public bool IsDisplayJarvisClockInOnTimePercentage { get; set; }

    [Column("IsDisplayMinutesProducedPerHourLast30Days", TypeName = "bit")]
    public bool IsDisplayMinutesProducedPerHourLast30Days { get; set; }

    [Column("IsDisplayMinutesProducedPerHourLast90Days", TypeName = "bit")]
    public bool IsDisplayMinutesProducedPerHourLast90Days { get; set; }
}