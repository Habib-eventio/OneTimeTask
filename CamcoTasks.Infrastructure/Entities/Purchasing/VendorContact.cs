// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_VendorContacts")]
public class VendorContact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("ShippingVendorID", TypeName = "int")]
    public int ShippingVendorId { get; set; }

    [Column("TypeID", TypeName = "int")] public int TypeId { get; set; }

    [Column("Name", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Name { get; set; }

    [Column("PhoneNumber", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string PhoneNumber { get; set; }

    [Column("Email", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Email { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime DateEntered { get; set; }

    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string EnteredBy { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}