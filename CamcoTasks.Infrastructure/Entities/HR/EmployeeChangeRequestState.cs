using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EmployeeChangeRequestStates")]
public class EmployeeChangeRequestState
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ChangeRequestStatusId", TypeName = "smallint")]
    public short ChangeRequestStatusId { get; set; }

    [Column("Remarks", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(50)]
    [Required]
    public string Remarks { get; set; }

    [Browsable(false)]
    [Column("CreatedBy", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByEmployeeId Column). Please refer to CorrectCreatedByEmployeeId")]
    public long? ObsoleteCreatedBy { get; set; }

    [Column("CorrectCreatedByEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("EmployeeChangeRequestId", TypeName = "int")]
    public int EmployeeChangeRequestId { get; set; }
}