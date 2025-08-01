// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_WorkcenterData")]
public class WorkCenterDatum
{
    [Column("WorkCenterTag", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string WorkCenterTag { get; set; }

    [Column("Description", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Description { get; set; }

    [Obsolete]
    [Column("Setup Time", TypeName = "float")]
    public double? SetupTime { get; set; }

    [Column("MachineCount", TypeName = "float")]
    public double? MachineCount { get; set; }

    [Obsolete]
    [Column("EFF PERCENT", TypeName = "int")]
    public int? EFFPercent { get; set; }

    [Column("Area", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Area { get; set; }

    [Column("IsCycleTimeRequired", TypeName = "bit")]
    public bool IsCycleTimeRequired { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("IsQualitySignOffRequired", TypeName = "bit")]
    public bool IsQualitySignOffRequired { get; set; }

    [Column("IsOperationalDrawingSheetRequired", TypeName = "bit")]
    public bool IsOperationalDrawingSheetRequired { get; set; }

    [Column("IsCheckSheetRequired", TypeName = "bit")]
    public bool IsCheckSheetRequired { get; set; }

    [Column("IsPackingInstructionsRequired", TypeName = "bit")]
    public bool IsPackingInstructionsRequired { get; set; }

    [Column("CreatedDate", TypeName = "DateTime")]
    public DateTime CreatedDate { get; set; }

    [Column("IsActive", TypeName = "bit")] public bool IsActive { get; set; }

    [Column("IsDelete", TypeName = "bit")] public bool IsDelete { get; set; }
}