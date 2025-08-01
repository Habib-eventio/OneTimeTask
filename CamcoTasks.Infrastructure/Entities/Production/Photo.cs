// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_ProductionPhotos")]
public class Photo
{
    [Key]
    [Column("Id", TypeName = "bigint")]
    public long Id { get; set; }

    [Column("EmployeeId", TypeName = "bigint")]
    public long? EmployeeId { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(MAX)")]
    public string PartNumber { get; set; }

    [Column("ShopOrder", TypeName = "nvarchar(MAX)")]
    public string ShopOrder { get; set; }

    [Column("OperationNumber", TypeName = "nvarchar(MAX)")]
    public string OperationNumber { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime? DateEntered { get; set; }

    [Column("TypeId", TypeName = "int")] public int? TypeId { get; set; }

    [Column("PictureNumber", TypeName = "int")]
    public int? PictureNumber { get; set; }

    [Column("ProductionImage", TypeName = "nvarchar(MAX)")]
    public string ProductionImage { get; set; }

    [Column("TravelerImage", TypeName = "nvarchar(MAX)")]
    public string TravelerImage { get; set; }
}