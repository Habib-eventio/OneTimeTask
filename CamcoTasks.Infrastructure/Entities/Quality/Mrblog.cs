// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_MRBLog")]
public class Mrblog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("Customer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Customer { get; set; }

    [Column("Quantity", TypeName = "int")] public int? Quantity { get; set; }

    [Column("QuantityOut", TypeName = "int")]
    public int? QuantityOut { get; set; }

    [Column("QuantityCurrent")] public int? QuantityCurrent { get; set; }

    [Column("PartPrice", TypeName = "money")]
    public decimal? PartPrice { get; set; }

    [Column("MRBInDate", TypeName = "datetime")]
    public DateTime? MrbinDate { get; set; }

    [Column("RemovedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RemovedBy { get; set; }

    [Column("MRBOutDate", TypeName = "datetime")]
    public DateTime? MrboutDate { get; set; }

    [Column("ResponsiblePerson", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ResponsiblePerson { get; set; }

    [Column("Reason", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Reason { get; set; }

    [Column("Location", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Location { get; set; }

    [Column("OriginalSO#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OriginalSo { get; set; }

    [Column("RepairSO#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string RepairSo { get; set; }

    [Column("FinalDispositionNotes", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string FinalDispositionNotes { get; set; }

    [Column("CPARIssued", TypeName = "bit")]
    public bool CparIssued { get; set; }

    [Column("CPAR#", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Cpar { get; set; }

    [Column("MovedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string MovedBy { get; set; }

    [Column("EngineeringRevision", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string EngineeringRevision { get; set; }
}