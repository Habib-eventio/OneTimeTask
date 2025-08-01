// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_ShippingMethods")]
public class ShippingMethod
{
    [Column("SHIPPING METHOD", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ShippingMethodName { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("EnteredBy", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string EnteredBy { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime DateEntered { get; set; }
}