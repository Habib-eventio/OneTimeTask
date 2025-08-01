// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_VendorProductRelationships")]
public class VendorProductRelationship
{
    [Key]
    [Column("ProductCategoryID", TypeName = "int")]
    public int ProductCategoryId { get; set; }

    [Key]
    [Column("VendorID", TypeName = "int")]
    public int VendorId { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }

    [Column("DateDeleted", TypeName = "datetime2(7)")]
    public DateTime? DateDeleted { get; set; }

    [Column("EnterredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string EnteredBy { get; set; }
}