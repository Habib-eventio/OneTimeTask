using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("HR_LoanBalanceHistory")]
public class LoanBalanceHistory
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("LoanPaymentDate", TypeName = "datetime2(7)")]
    public DateTime LoanPaymentDate { get; set; }

    [Column("LoanPaymentAmount", TypeName = "decimal(8,2)")]
    public decimal LoanPaymentAmount { get; set; }

    [Column("LoanBalance", TypeName = "decimal(8,2)")]
    public decimal LoanBalance { get; set; }

    [Column("LogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string LogId { get; set; }

    [Column("EnumTypeId", TypeName = "smallint")]
    public short EnumTypeId { get; set; }

    [Column("Details", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string Details { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }
}