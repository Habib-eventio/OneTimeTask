using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_ChangeRequests")]
public class ChangeRequest
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("EnumTypeId", TypeName = "smallint")]
    public short EnumTypeId { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("DateEffective", TypeName = "datetime2(7)")]
    public DateTime DateEffective { get; set; }

    [Column("Details", TypeName = "varchar(1000)")]
    [StringLength(1000)]
    [MaxLength(1000)]
    [Required]
    public string Details { get; set; }

    [Column("SeparationAndTerminationDocument", TypeName = "varchar(1000)")]
    [StringLength(1000)]
    [MaxLength(1000)]
    public string SeparationAndTerminationDocument { get; set; }

    [Column("IsRequestCompleted", TypeName = "bit")]
    public bool IsRequestCompleted { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}