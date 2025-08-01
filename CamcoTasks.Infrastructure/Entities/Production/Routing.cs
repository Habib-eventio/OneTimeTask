// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_Routings")]
public class Routing
{
    [Column("RM_SWITCH", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RmSwitch { get; set; }

    [Column("RM_WKCTR", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RmWkctr { get; set; }

    [Column("RM_DESC", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string RmDesc { get; set; }

    [Column("VERIFIED CYCLETIME", TypeName = "bit")]
    public bool VerifiedCycleTime { get; set; }

    [Column("IR OCT-11", TypeName = "money")]
    public decimal? IrOct11 { get; set; }

    [Column("RM_PART", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string RmPart { get; set; }

    [Column("RM_OP", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RmOp { get; set; }

    [Column("RM_CT", TypeName = "float")] public double? RmCt { get; set; }

    [Column("CT_MINUTES")] public double? CtMinutes { get; set; }

    [Column("ODS_Num", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string OdsNum { get; set; }

    [Column("SetupSheet", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SetupSheet { get; set; }

    [Column("Check Sheet", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string CheckSheet { get; set; }

    [Column("DNCNUMBER", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string DncNumber { get; set; }

    [Column("SETUPPIC1", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SetupPicture1 { get; set; }

    [Column("QCPic1", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QcPicture1 { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("WORKHOLDINGPIC", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string WorkHoldingPicture { get; set; }

    [Column("DEBURR1", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Deburr1 { get; set; }

    [Column("PkgInst", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string PackageInst { get; set; }

    [Column("SETUPPIC2", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SetupPicture2 { get; set; }

    [Column("SETUPPIC3", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SetupPicture3 { get; set; }

    [Column("SETUPPIC4", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SetupPicture4 { get; set; }

    [Column("SETUPPIC5", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string SetupPicture5 { get; set; }

    [Column("QCPic2", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QcPicture2 { get; set; }

    [Column("QCPic3", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QcPicture3 { get; set; }

    [Column("QCPic4", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QcPicture4 { get; set; }

    [Column("QCPic5", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QcPicture5 { get; set; }

    [Column("Deburr2", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Deburr2 { get; set; }

    [Column("Deburr3", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Deburr3 { get; set; }

    [Column("QUALNOTE", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QualNote { get; set; }

    [Column("QualityHold", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string QualityHold { get; set; }

    [Browsable(false)]
    [NotMapped]
    [Column("tstamp", TypeName = "timestamp")]
    public byte[] Tstamp { get; set; }

    [Column("DateEntered", TypeName = "datetime2(7)")]
    public DateTime? DateEntered { get; set; }

    [Column("CMMSetupSheet", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string CMMSetupSheet { get; set; }

    [Column("CMMMasterProgramNumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string CMMMasterProgramNumber { get; set; }

    [Column("CMMQuickCheckProgramNumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string CMMQuickCheckProgramNumber { get; set; }

    [Column("CMMCriticalDimensionsProgramNumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string CMMCriticalDimensionsProgramNumber { get; set; }

    [Column("ODSSheetID", TypeName = "int")]
    public int? ODSSheetID { get; set; }

    [Column("FixedCost", TypeName = "decimal")]
    public decimal FixedCost { get; set; }
}