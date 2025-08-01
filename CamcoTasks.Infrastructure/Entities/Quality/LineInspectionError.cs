// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_LineInspectionErrors")]
public class LineInspectionError
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("InspectorId", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string InspectorId { get; set; }

    [Column("InspectionCounts", TypeName = "int")]
    public int InspectionCounts { get; set; }

    [Column("DateOfInspectionError", TypeName = "date")]
    public DateTime DateOfInspectionError { get; set; }

    [Column("IsShopNumberFaulty", TypeName = "bit")]
    public bool? IsShopNumberFaulty { get; set; }

    [Column("IsInspectionDateFaulty", TypeName = "bit")]
    public bool? IsInspectionDateFaulty { get; set; }

    [Column("IsOperatorNameFaulty", TypeName = "bit")]
    public bool? IsOperatorNameFaulty { get; set; }

    [Column("IsInspectorNameFaulty", TypeName = "bit")]
    public bool? IsInspectorNameFaulty { get; set; }

    [Column("InspectionAuditId", TypeName = "int")]
    public int? InspectionAuditId { get; set; }

    [Column("ErrorsDetail", TypeName = "varchar(200)")]
    [StringLength(200)]
    [MaxLength(200)]
    public string ErrorsDetail { get; set; }
}