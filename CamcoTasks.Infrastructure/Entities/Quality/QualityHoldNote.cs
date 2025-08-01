using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_QualityHoldNotes")]
public partial class QualityHoldNote
{
    [Column("QualityHoldNotesId", TypeName = "int")]
    public int Id { get; set; }

    [Column("QualityHoldId", TypeName = "int")]
    public int? QualityHoldId { get; set; }

    [Column("QualityHoldNotes", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string QualityHoldNotes { get; set; }

    [Column("Date", TypeName = "dateTime")]
    public DateTime? Date { get; set; }

    [Column("UpdatedBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string UpdatedBy { get; set; }

    [Browsable(false)]
    [NotMapped]
    [Column("SsmaTimeStamp", TypeName = "timestamp")]
    public byte[] SsmaTimeStamp { get; set; }
}