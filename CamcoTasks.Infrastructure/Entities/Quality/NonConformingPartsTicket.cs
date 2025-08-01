// This File Needs to be reviewed Still. Don't Remove this comment.
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_NonConformingParts_Ticket")]
public class NonConformingPartsTicket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("SoNumber", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Obsolete(
        "This is obsolete And it will be deleted. Please use the shop order Id column as defined by the foreign key relationship.")]
    public string SoNumber { get; set; }

    [Column("ReasonCode", TypeName = "int")]
    public int? ReasonCode { get; set; }

    [Column("CauseCode", TypeName = "int")]
    public int? CauseCode { get; set; }

    [Column("ReportedById", TypeName = "int")]
    public int? ReportedById { get; set; }

    [Column("ResponsibleEmpId", TypeName = "int")]
    public int? ResponsibleEmployeeId { get; set; }

    [Column("Shift", TypeName = "int")] public int? Shift { get; set; }

    [Column("MaterialId", TypeName = "int")]
    public int? MaterialId { get; set; }

    [Column("DepartmentOfErrorId", TypeName = "bigint")]
    public long? DepartmentOfErrorId { get; set; }

    [Column("AddedDate", TypeName = "datetime")]
    public DateTime? AddedDate { get; set; }

    [ForeignKey("ShopOrder")]
    [Column("ShopOrderId", TypeName = "int")]
    public int? ShopOrderId { get; set; }

    //public virtual ShopOrderNumberLogCurrent ShopOrder { get; set; }
}