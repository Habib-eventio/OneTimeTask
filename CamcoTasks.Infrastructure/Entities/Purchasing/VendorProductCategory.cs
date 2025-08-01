// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_VendorProductCategories")]
public class VendorProductCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Name { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }

    [Column("DateDeleted", TypeName = "datetime2(7)")]
    public DateTime? DateDeleted { get; set; }

    [Column("EnterredBy", TypeName = "nchar(10)")]
    [StringLength(10)]
    [MaxLength(10)]
    [Required]
    public string EnteredBy { get; set; }
}