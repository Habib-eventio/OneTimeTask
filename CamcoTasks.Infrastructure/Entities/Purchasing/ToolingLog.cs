//// This File Needs to be reviewed Still. Don't Remove this comment.
//using CamcoTasks.Infrastructure.Entities.Kanban;
//using ERP.Data.Entities.Planning;
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Purchasing;

//[Table("Purchasing_ToolingLog")]
//public class ToolingLog
//{
//    [Column("Fin_Item", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    public string FinItem { get; set; }

//    [Column("Fin_SO Number", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Obsolete(
//        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
//    public string FinSoNumber { get; set; }

//    [Column("Fin_Cost", TypeName = "money")]
//    public decimal? FinCost { get; set; }

//    [Column("Fin_Date", TypeName = "datetime")]
//    public DateTime? FinDate { get; set; }

//    [Column("STATUS", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Status { get; set; }

//    [Column("ITEM TYPE", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string ItemType { get; set; }

//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("PONum", TypeName = "int")] public int? PoNum { get; set; }

//    [ForeignKey("ShopOrder")]
//    [Column("ShopOrderId", TypeName = "int")]
//    public int? ShopOrderId { get; set; }

//    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
//}