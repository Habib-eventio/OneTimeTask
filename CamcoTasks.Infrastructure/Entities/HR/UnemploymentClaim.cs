using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_UnemploymentClaims")]
public class UnemploymentClaim
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("EmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("InitialUnemploymentClaimDate", TypeName = "datetime2(7)")]
    public DateTime InitialUnemploymentClaimDate { get; set; }

    [Column("DateEntered", TypeName = "datetime2(7)")]
    public DateTime DateEntered { get; set; }

    [Column("EnteredByEmployeeId", TypeName = "bigint")]
    public long EnteredByEmployeeId { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}