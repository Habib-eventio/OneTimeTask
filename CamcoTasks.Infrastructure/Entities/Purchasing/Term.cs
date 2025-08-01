// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_Terms")]
public class Term
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(MAX)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string Name { get; set; }

    [Column("NumberOfDays", TypeName = "int")]
    public int? NumberOfDays { get; set; }

    [Column("PaymentDay", TypeName = "int")]
    public int? PaymentDay { get; set; }

    [Column("PaymentStartCloseDay", TypeName = "int")]
    public int? PaymentStartCloseDay { get; set; }

    [Column("PaymentEndCloseDay", TypeName = "int")]
    public int? PaymentEndCloseDay { get; set; }

    [Column("IsCyclical", TypeName = "bit")]
    public bool IsCyclical { get; set; }

    [Column("EnterredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string EnteredBy { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}