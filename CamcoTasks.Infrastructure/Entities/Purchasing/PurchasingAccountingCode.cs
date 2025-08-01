// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Table("Purchasing_AccountingCodes")]
public class PurchasingAccountingCode
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("LetterCode", TypeName = "char(1)")]
    [StringLength(1)]
    [MaxLength(1)]
    [Required]
    public string LetterCode { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Description { get; set; }
}