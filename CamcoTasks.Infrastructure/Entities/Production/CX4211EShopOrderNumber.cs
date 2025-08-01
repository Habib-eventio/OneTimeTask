// This File Needs to be reviewed Still. Don't Remove this comment.
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_Cx4211EShopOrderNumbers")]
public class CX4211EShopOrderNumber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("SoNumber", TypeName = "int")]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public int ShopOrderNumber { get; set; }

    [Column("SoDescription", TypeName = "varchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    [Required]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public string ShopOrderNumberDescription { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("ShopOrder")]
    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }

}