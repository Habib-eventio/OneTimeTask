//// This File Needs to be reviewed Still. Don't Remove this comment.
//using CamcoTasks.Infrastructure.Entities.Kanban;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Quality;

//[Table("Quality_MRBRequisitions")]
//public class MrbRequisition
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("MRBRequisitionID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("RequisitionDate", TypeName = "datetime2(7)")]
//    public DateTime? RequisitionDate { get; set; }

//    [Column("PartNumber", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string PartNumber { get; set; }

//    [Column("SO#", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    [Obsolete(
//        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
//    public string So { get; set; }

//    [Column("QuantityRequested", TypeName = "int")]
//    public double? QuantityRequested { get; set; }

//    [Column("RequestedBy", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string RequestedBy { get; set; }

//    [Column("CompletedBy", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string CompletedBy { get; set; }

//    [Column("CompletedDate", TypeName = "datetime2(7)")]
//    public DateTime? CompletedDate { get; set; }

//    [Column("DueByDate", TypeName = "datetime2(7)")]
//    public DateTime? DueByDate { get; set; }

//    [Column("QuantityPulled", TypeName = "float")]
//    public double? QuantityPulled { get; set; }

//    [Column("MRBID", TypeName = "int")] public int? MRBID { get; set; }

//    [ForeignKey("ShopOrder")]
//    [Column("ShopOrderId", TypeName = "int")]
//    public int? ShopOrderId { get; set; }

//    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
//}