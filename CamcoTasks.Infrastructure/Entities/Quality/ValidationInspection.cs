using ERP.Data.Entities.HR;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Quality;

[Table("Quality_ValidationInspection")]
public partial class ValidationInspection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ValidationInspectionId", TypeName = "int")]
    public int ValidationInspectionId { get; set; }


    [Column("InspectionDate", TypeName = "datetime")]
    public DateTime InspectionDate { get; set; }


    [Column("InspectorId", TypeName = "bigint")]
    public long InspectorId { get; set; }


    [Column("OperationNumber", TypeName = "varchar(255)")]
    public string OperationNumber { get; set; }


    [Column("PartNumber", TypeName = "varchar(255)")]
    public string PartNumber { get; set; }


    [Column("BatchQuantity", TypeName = "int")]
    public int BatchQuantity { get; set; } = 0;


    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }


    [Column("CustomerOrderNumber", TypeName = "varchar(255)")]
    public string CustomerOrderNumber { get; set; }


    [Column("AdditionalNotes", TypeName = "varchar(MAX)")]
    public string AdditionalNotes { get; set; }


    [Column("EngineeringDesignImage", TypeName = "varchar(MAX)")]
    public string EngineeringDesignImage { get; set; }


    [Column("CheckSheetImage", TypeName = "varchar(MAX)")]
    public string CheckSheetImage { get; set; }


    [Column("OperationalDesignImage", TypeName = "varchar(MAX)")]
    public string OperationalDesignImage { get; set; }


    //[ForeignKey("InspectorId")] public virtual Employee Inspector { get; set; }


    //[ForeignKey("ShopOrderId")] public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
}