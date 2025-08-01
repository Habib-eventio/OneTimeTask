using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_LineInspectionTimeDurationLog")]
public class LineInspectionTimeDurationLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [ForeignKey("InspectionObj")]
    [Column("InspectionAuditId", TypeName = "int")]
    public int? InspectionAuditId { get; set; }

    [Column("StartTime", TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    [Column("EndTime", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    //public virtual QualityLineInspection InspectionObj { get; set; }
}