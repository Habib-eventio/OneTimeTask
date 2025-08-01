using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_EmployeeChangesTracking")]
public class EmployeeChangeTracking
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("OldValue", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string OldValue { get; set; }

    [Column("NewValue", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string NewValue { get; set; }

    [Column("Description", TypeName = "varchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    public string Description { get; set; }

    [Browsable(false)]
    [Column("EmployeeId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
    public long? ObsoleteEmployeeId { get; set; }

    [Column("CorrectEmployeeId", TypeName = "bigint")]
    public long? EmployeeId { get; set; }

    [Column("AttachedFile", TypeName = "varchar(100)")]
    [StringLength(100)]
    [MaxLength(100)]
    public string AttachedFile { get; set; }

    [Column("LogTypeId", TypeName = "bigint")]
    public long? LogTypeId { get; set; }

    [Column("OldLogId", TypeName = "varchar(15)")]
    [StringLength(15)]
    [MaxLength(15)]
    public string OldLogId { get; set; }

    [Browsable(false)]
    [Column("ChangedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectChangedByIdEmployeeId Column). Please refer to CorrectChangedByIdEmployeeId")]
    public long? ObsoleteChangedById { get; set; }

    [Column("CorrectChangedByIdEmployeeId", TypeName = "bigint")]
    public long? ChangedByEmployeeId { get; set; }

    [Column("DateChanged", TypeName = "datetime")]
    public DateTime DateChanged { get; set; }

    [Column("ChangeFormDocument", TypeName = "varchar(5000)")]
    [StringLength(5000)]
    [MaxLength(5000)]
    public string ChangeFormDocument { get; set; }
}