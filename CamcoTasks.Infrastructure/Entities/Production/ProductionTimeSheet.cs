// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_TimeSheets")]
public class ProductionTimeSheet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("EmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime DateUpdated { get; set; }

    [Column("CreatedById", TypeName = "bigint")]
    public long CreatedById { get; set; }

    [Column("UpdatedById", TypeName = "bigint")]
    public long? UpdatedById { get; set; }

    [Column("Date", TypeName = "date")] public DateTime Date { get; set; }

    [Column("TimeSheetTypeId", TypeName = "int")]
    public int TimeSheetTypeId { get; set; }

    [Column("Path", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string Path { get; set; }

    [Column("Reason", TypeName = "varchar(2500)")]
    [StringLength(2500)]
    [MaxLength(2500)]
    public string Reason { get; set; }

    [Column("IsProductionSheetCompleted", TypeName = "bit")]
    public bool? IsProductionSheetCompleted { get; set; }
}