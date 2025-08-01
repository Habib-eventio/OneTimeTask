// This File Needs to be reviewed Still. Don't Remove this comment.
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_EngineeringReviews")]
public class EngineeringReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("PartNumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public string PartNumber { get; set; }

    [Column("SONum", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public string ShopOrderNumber { get; set; }

    [Column("PartDescription", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string PartNumberDescription { get; set; }

    [Column("Customer", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public string Customer { get; set; }

    [Column("RoutingLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string RoutingLink { get; set; }

    [Column("PrintLink", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string PrintLink { get; set; }

    [Column("ReviewDate", TypeName = "datetime2(0)")]
    public DateTime? ReviewDate { get; set; }

    [Column("IsInProgress", TypeName = "bit")]
    public bool IsInProgress { get; set; }

    [Column("SODueDate", TypeName = "datetime2(0)")]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public DateTime ShopOrderNumberDueDate { get; set; }

    [Column("DateEntered", TypeName = "datetime2(7)")]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public DateTime? DateEntered { get; set; }

    [ForeignKey("ShopOrder")]
    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
}