// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_DNC_ProgramNumbers")]
public class DncProgramNumber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("ProgramNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ProgramNumber { get; set; }

    [Column("OperationNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string OperationNumber { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("EnteredById", TypeName = "bigint")]
    public long? EnteredById { get; set; }

    [Column("Customer", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Customer { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime DateEntered { get; set; }

    [Column("ProgramType", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ProgramType { get; set; }

    [Column("EngineeringRev", TypeName = "nvarchar(2)")]
    [StringLength(2)]
    [MaxLength(2)]
    public string EngineeringReview { get; set; }

    [Column("ClassOfCmm", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ClassOfCmm { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Description { get; set; }

    [Column("GageId", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string SerialNumber { get; set; }
}