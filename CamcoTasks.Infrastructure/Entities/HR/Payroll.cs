using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_Payrolls")]
public class Payroll
{
    public Payroll()
    {
        HrPayrollDetails = new HashSet<PayrollDetail>();
    }

    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("PayrollStartDate", TypeName = "datetime2(7)")]
    public DateTime PayrollStartDate { get; set; }

    [Column("BatchNumber", TypeName = "bigint")]
    public long BatchNumber { get; set; }

    [Column("IsPayrollLocked", TypeName = "bit")]
    public bool? IsPayrollLocked { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Column("DateSubmitted", TypeName = "datetime2(7)")]
    public DateTime? DateSubmitted { get; set; }

    public virtual ICollection<PayrollDetail> HrPayrollDetails { get; set; }
}