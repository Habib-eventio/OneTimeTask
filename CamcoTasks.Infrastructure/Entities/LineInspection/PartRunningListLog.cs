using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.LineInspection;

[Table("LineInspection_PartsRunningListLog")]
public class PartRunningListLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("MachineId", TypeName = "int")]
    public int? MachineId { get; set; }

    [Column("OperatorId", TypeName = "bigint")]
    public long? OperatorId { get; set; }

    [Column("InspectionStatus", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string InspectionStatus { get; set; }

    [Column("InspectionCount", TypeName = "int")]
    public int? InspectionCount { get; set; }

    [Column("ShopOrderNumber", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the relevant Id column as defined by the foreign key relationship.")]
    public string ShopOrderNumber { get; set; }

    [Column("OperationNumber", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string OperationNumber { get; set; }

    [Column("MachineStatus", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string MachineStatus { get; set; }

    [Column("ChangedTime", TypeName = "datetime")]
    public DateTime? ChangedTime { get; set; }

    [Column("ChangedById", TypeName = "bigint")]
    public long? ChangedById { get; set; }

    [Column("PartRunningId", TypeName = "int")]
    public int? PartRunningId { get; set; }

    [Column("IsGaging", TypeName = "bit")] public bool IsGaging { get; set; } = false;

    [ForeignKey("ShopOrder")]
    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    public virtual Employee ChangedBy { get; set; }

    public virtual PartRunningList PartRunning { get; set; }
}