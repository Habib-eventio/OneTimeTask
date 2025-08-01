// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_CustomerOrders")]
public class ProductionCustomerOrder
{
    [Column("CO_PART", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string PartNumber { get; set; }

    [Column("CO_MAT'L_COST", TypeName = "money")]
    public decimal? MaterialCost { get; set; }

    [Column("CO_CLOSED", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string CustomerOrderClosed { get; set; }

    [Column("CO_MAT'L", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string MaterialDetail { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }
}