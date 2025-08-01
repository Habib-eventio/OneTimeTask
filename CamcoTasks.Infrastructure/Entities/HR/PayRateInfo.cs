using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_PayRatesInfo")]
public class PayRateInfo
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("DateEffective", TypeName = "datetime2(7)")]
    public DateTime DateEffective { get; set; }

    [Column("BaseRate", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Required]
    public string BaseRate { get; set; }

    [Column("LastRateOfPay", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string LastRateOfPay { get; set; }

    [Column("Attachment", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Attachment { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Column("LogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string LogId { get; set; }
}