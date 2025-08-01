//// This File Needs to be reviewed Still. Don't Remove this comment.
//using CamcoTasks.Infrastructure.Entities.Kanban;
//using ERP.Data.Entities.Planning;
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace CamcoTasks.Infrastructure.Entities.Quality;

//[Table("Quality_MaterialReviewBoardLog")]
//public class MaterialReviewBoardLog
//{
//    [Key] [Column("ID", TypeName = "int")] public int Id { get; set; }

//    [Column("MRBInDate", TypeName = "datetime")]
//    public DateTime MrbinDate { get; set; }

//    [Column("MovedBy", TypeName = "nvarchar(50)")]
//    public string MovedBy { get; set; }

//    [Column("PartNumber", TypeName = "nvarchar(50)")]
//    public string PartNumber { get; set; }

//    [Column("EngineeringRevision", TypeName = "nvarchar(50)")]
//    public string EngineeringRevision { get; set; }

//    [Column("OriginalSO#", TypeName = "nvarchar(50)")]
//    [Obsolete(
//        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
//    public string OriginalSo { get; set; }

//    [Column("LastOperationCompleted", TypeName = "nvarchar(50)")]
//    public string LastOperationCompleted { get; set; }

//    [Column("Quantity", TypeName = "int")] public int Quantity { get; set; }

//    [Column("QuantityOut", TypeName = "int")]
//    public int QuantityOut { get; set; }

//    [Column("QuantityCurrent")] public int? QuantityCurrent { get; set; }

//    [Column("Reason", TypeName = "nvarchar(50)")]
//    public string Reason { get; set; }

//    [Column("DescriptionOfNonconformance", TypeName = "nvarchar(MAX)")]
//    public string DescriptionOfNonconformance { get; set; }

//    [Column("ResponsiblePerson", TypeName = "nvarchar(50)")]
//    public string ResponsiblePerson { get; set; }

//    [Column("Location", TypeName = "nvarchar(50)")]
//    public string Location { get; set; }

//    [Column("MRBOutDate", TypeName = "datetime")]
//    public DateTime? MrboutDate { get; set; }

//    [Column("RemovedBy", TypeName = "nvarchar(50)")]
//    public string RemovedBy { get; set; }

//    [Column("RepairSO#", TypeName = "nvarchar(50)")]
//    [Obsolete(
//        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
//    public string RepairSo { get; set; }

//    [Column("FinalDispositionNotes", TypeName = "nvarchar(MAX)")]
//    public string FinalDispositionNotes { get; set; }

//    [Column("CPARIssued", TypeName = "nvarchar(50)")]
//    public string CparIssued { get; set; }

//    [Column("CPAR#", TypeName = "nvarchar(50)")]
//    public string Cpar { get; set; }

//    [Column("TimeDelayShipInterval(InMonths)", TypeName = "int")]
//    public int TimeDelayShipIntervalInMonths { get; set; }

//    [Column("TimeDelayShipReleaseDate")] public DateTime? TimeDelayShipReleaseDate { get; set; }

//    [Column("InputReviewComplete", TypeName = "datetime2(7)")]
//    public DateTime? InputReviewComplete { get; set; }

//    [Column("RMA#", TypeName = "nvarchar(50)")]
//    public string Rma { get; set; }

//    [Column("RMAPart", TypeName = "nvarchar(50)")]
//    public string Rmapart { get; set; }

//    [Column("RemovalNote", TypeName = "nvarchar(MAX)")]
//    public string RemovalNote { get; set; }

//    [ForeignKey("ShopOrder")]
//    [Column("ShopOrderId", TypeName = "int")]
//    public int? ShopOrderId { get; set; }

//    [ForeignKey("RepairShopOrder")]
//    [Column("RepairShopOrderId", TypeName = "int")]
//    public int? RepairShopOrderId { get; set; }


//}