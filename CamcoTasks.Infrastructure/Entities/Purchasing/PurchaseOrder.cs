//// This File Needs to be reviewed Still. Don't Remove this comment.
//using ERP.Data.Entities.Kanban;

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


//namespace CamcoTasks.Infrastructure.Entities.Kanban;

//[Table("Purchasing_PurchaseOrders")]
//public class PurchaseOrder
//{
//    [Key] [Column("ID", TypeName = "int")] public int Id { get; set; }

//    [Column("PO DATE", TypeName = "datetime")]
//    public DateTime? PurchaseOrderNumberDate { get; set; }

//    [Column("Due Date", TypeName = "datetime")]
//    public DateTime? DueDate { get; set; }

//    [Column("PO Num", TypeName = "int")] public int? PurchaseOrderNumber { get; set; }

//    [Column("Acc Code", TypeName = "nvarchar(255)")]
//    public string AccCode { get; set; }

//    [Column("QRN", TypeName = "nvarchar(255)")]
//    public string QRN { get; set; }

//    [Column("Manufacturer", TypeName = "nvarchar(MAX)")]
//    public string Manufacturer { get; set; }

//    [Column("ManufacturerPartNumber", TypeName = "nvarchar(MAX)")]
//    public string ManufacturerPartNumber { get; set; }

//    [Column("ITEM TYPE", TypeName = "nvarchar(255)")]
//    public string ItemType { get; set; }

//    [Column("QTY", TypeName = "nvarchar(255)")]
//    public string Quantity { get; set; }

//    [Column("UNIT COST", TypeName = "money")]
//    public decimal? UnitCost { get; set; }

//    [Column("TOTAL", TypeName = "money")] public decimal? Total { get; set; }

//    [Column("DESCRIPTION", TypeName = "nvarchar(MAX)")]
//    public string Description { get; set; }

//    [Column("PartNumber", TypeName = "nvarchar(255)")]
//    public string PartNumber { get; set; }

//    [Column("SUPPLIER", TypeName = "nvarchar(255)")]
//    public string Supplier { get; set; }

//    [Column("INITIATOR", TypeName = "nvarchar(255)")]
//    public string Initiator { get; set; }

//    [Column("ORDER PLACED WITH", TypeName = "nvarchar(255)")]
//    public string OrderPlacedWith { get; set; }

//    [Column("SONumber", TypeName = "nvarchar(255)")]
//    [Obsolete(
//        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
//    public string ShopOrderNumber { get; set; }

//    [Column("STATUS", TypeName = "nvarchar(255)")]
//    public string Status { get; set; }

//    [Column("DATE RECEIVED", TypeName = "datetime2(0)")]
//    public DateTime? DateReceived { get; set; }

//    [Column("NOTES", TypeName = "nvarchar(255)")]
//    public string Notes { get; set; }

//    [Column("DATE ENTERED", TypeName = "datetime")]
//    public DateTime? DateEntered { get; set; }

//    [Column("LOCATION", TypeName = "float")]
//    public double? Location { get; set; }

//    [Column("Street", TypeName = "nvarchar(255)")]
//    public string Street { get; set; }

//    [Column("line 2", TypeName = "nvarchar(255)")]
//    public string Line2 { get; set; }

//    [Column("City", TypeName = "nvarchar(255)")]
//    public string City { get; set; }

//    [Column("State", TypeName = "nvarchar(255)")]
//    public string State { get; set; }

//    [Column("Zip", TypeName = "nvarchar(255)")]
//    public string Zip { get; set; }

//    [Column("Phone", TypeName = "nvarchar(255)")]
//    public string Phone { get; set; }

//    [Column("Shipping Method", TypeName = "nvarchar(255)")]
//    public string ShippingMethod { get; set; }

//    [Column("Certs Required?", TypeName = "nvarchar(255)")]
//    public string CertsRequired { get; set; }

//    [Column("Quote Number", TypeName = "nvarchar(255)")]
//    public string QuoteNumber { get; set; }

//    [Column("PO Notes", TypeName = "nvarchar(255)")]
//    public string PurchaseOrderNumberNotes { get; set; }

//    [Column("Units of Measurement", TypeName = "nvarchar(255)")]
//    public string UnitOfMeasurement { get; set; }

//    [Column("Field1", TypeName = "nvarchar(255)")]
//    public string Field1 { get; set; }

//    [Column("Field2", TypeName = "nvarchar(255)")]
//    public string Field2 { get; set; }

//    [Column("Field3", TypeName = "nvarchar(255)")]
//    public string Field3 { get; set; }

//    [Column("Field4", TypeName = "nvarchar(255)")]
//    public string Field4 { get; set; }

//    [Column("Line Number", TypeName = "int")]
//    public int? LineNumber { get; set; }

//    [Column("MATERIAL TYPE", TypeName = "nvarchar(255)")]
//    public string MaterialType { get; set; }

//    [Column("DATA COMBINE")] public string DataCombine { get; set; }

//    [Column("PRNum", TypeName = "int")] public int? PrNumber { get; set; }

//    [Column("Field6", TypeName = "int")] public int? Field6 { get; set; }

//    [Column("Vendor", TypeName = "nvarchar(255)")]
//    public string Vendor { get; set; }

//    [Column("mstrID", TypeName = "int")] public int? MstrId { get; set; }

//    [Column("Requester", TypeName = "nvarchar(255)")]
//    public string Requester { get; set; }

//    [Column("ApprovedForExpenses", TypeName = "bit")]
//    public bool? ApprovedForExpenses { get; set; }

//    [Column("DatePaid", TypeName = "datetime2(0)")]
//    public DateTime? DatePaid { get; set; }

//    [Column("ExpectedDeliveryDate", TypeName = "datetime")]
//    public DateTime? ExpectedDeliveryDate { get; set; }

//    [Column("SelectedYN", TypeName = "bit")]
//    public bool? HasSelected { get; set; }

//    [Column("DayOfWeek", TypeName = "nvarchar(3)")]
//    public string DayOfWeek { get; set; }

//    [Column("EstimateOrActual", TypeName = "nvarchar(10)")]
//    public string EstimateOrActual { get; set; }

//    [Column("RunningTotal", TypeName = "money")]
//    public decimal? RunningTotal { get; set; }

//    [Column("DateMovedToHistory", TypeName = "datetime2(0)")]
//    public DateTime? DateMovedToHistory { get; set; }

//    [Column("ExpenseCategory", TypeName = "int")]
//    public int? ExpenseCategory { get; set; }

//    [Column("QTYReceived", TypeName = "float")]
//    public double? QuantityReceived { get; set; }

//    [Column("IsTrackable", TypeName = "bit")]
//    public bool IsTrackable { get; set; }

//    [Column("ExpectedPaymentDate", TypeName = "datetime")]
//    public DateTime? ExpectedPaymentDate { get; set; }

//    [Column("Terms", TypeName = "nvarchar(MAX)")]
//    public string Terms { get; set; }

//    [Column("FilePath", TypeName = "nvarchar(MAX)")]
//    public string FilePath { get; set; }

//    [ForeignKey("ReceiveLog")]
//    [Column("ReceiveID", TypeName = "int")]
//    public int? ReceiveId { get; set; }

//    //public virtual ReceivingLog ReceiveLog { get; set; }

//    [ForeignKey("ShopOrder")]
//    [Column("ShopOrderId", TypeName = "int")]
//    public int? ShopOrderId { get; set; }

//    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
//    public virtual ICollection<KanbanNote> KanbanNotes { get; set; } = new List<KanbanNote>();

//    public virtual ICollection<KanbanPurchaseOrderStatus> KanbanOrderStatus { get; set; } =
//        new List<KanbanPurchaseOrderStatus>();

//    public virtual ICollection<KanbanTransitionStatus> KanbanTransitionStatusFirstLotProductionPurchaseOrders { get; set; }
//    public virtual ICollection<KanbanTransitionStatus> KanbanTransitionStatusPurchaseOrderIdForSamplesNavigations { get; set; }
//    public virtual ICollection<KanbanTransitionStatus> KanbanTransitionStatusPurchaseOrderIdForSecondSamplesNavigations { get; set; }
//    public virtual ICollection<KanbanTransitionStatus> KanbanTransitionStatusSecondLotProductionPurchaseOrders { get; set; }
//    public virtual ICollection<KanbanTransitionStatus> KanbanTransitionStatusThirdLotProductionPurchaseOrders { get; set; }

//    public PurchaseOrder()
//    {
//        KanbanTransitionStatusFirstLotProductionPurchaseOrders = new List<KanbanTransitionStatus>();
//        KanbanTransitionStatusPurchaseOrderIdForSamplesNavigations = new List<KanbanTransitionStatus>();
//        KanbanTransitionStatusPurchaseOrderIdForSecondSamplesNavigations = new List<KanbanTransitionStatus>();
//        KanbanTransitionStatusSecondLotProductionPurchaseOrders = new List<KanbanTransitionStatus>();
//        KanbanTransitionStatusThirdLotProductionPurchaseOrders = new List<KanbanTransitionStatus>();
//    }
//}