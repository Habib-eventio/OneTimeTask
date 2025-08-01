// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_VendorContactTypes")]
public class VendorContactType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Name { get; set; }

    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string EnteredBy { get; set; }

    [Column("DateEntered", TypeName = "datetime")]
    public DateTime DateEntered { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}