using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_ItemAllocations")]
public class ItemAllocation
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("ItemId", TypeName = "bigint")]
    public long ItemId { get; set; }

    [Column("QuantityPerItem", TypeName = "bigint")]
    public long QuantityPerItem { get; set; }

    [Column("PricePerEachItem", TypeName = "decimal(7,2)")]
    public decimal PricePerEachItem { get; set; }

    [Column("Notes", TypeName = "nvarchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string Notes { get; set; }

    [Column("SupportingDocument", TypeName = "nvarchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string SupportingDocument { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("IsItemReturned", TypeName = "bit")]
    public bool? IsItemReturned { get; set; }

    [Column("DateReturned", TypeName = "datetime2(7)")]
    public DateTime? DateReturned { get; set; }
}