using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.LineInspection;

[Table("LineInspection_DataEntryFormImages")]
public class DataEntryFormImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("EntryId", TypeName = "int")] public int? EntryId { get; set; }

    [Column("ImageUrl", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ImageUrl { get; set; }

    [Column("ImageType", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ImageType { get; set; }
}