using ERP.Data.Entities.HR;
using ERP.Data.Entities.LineInspection;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Quality;

[Table("Quality_SkippedLineInspections")]
public class SkippedLineInspection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("Date", TypeName = "datetime2(7)")]
    public DateTime Date { get; set; }

    [Column("EmployeeId", TypeName = "long")]
    public long EmployeeId { get; set; }

    [Column("LineInspectionId", TypeName = "int")]
    public int LineInspectionId { get; set; }

    [Column("PartRunningId", TypeName = "int")]
    public int? PartRunningId { get; set; }

    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    //[ForeignKey("ShopOrderId")] public virtual ShopOrderNumberLogCurrent ShopOrderLog { get; set; }

    //[ForeignKey("EmployeeId")] public virtual Employee Employee { get; set; }

    //[ForeignKey("LineInspectionId")] public virtual QualityLineInspection LineInspection { get; set; }

    //[ForeignKey("PartRunningId")] public virtual PartRunningList PartRunningList { get; set; }
}