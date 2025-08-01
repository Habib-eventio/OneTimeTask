using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("HR_AboveAndBeyondRecognitions")]
public class AboveAndBeyondRecognition
{
    [Key]
    [Column("Id", TypeName = "bigint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("DateOfOccurrence", TypeName = "datetime2(7)")]
    public DateTime DateOfOccurrence { get; set; }

    [Column("EnumNoteTypeId", TypeName = "smallint")]
    public short EnumNoteTypeId { get; set; }

    [Column("Details", TypeName = "varchar(1500)")]
    [Required]
    [MaxLength(1500)]
    [StringLength(1500)]
    public string Details { get; set; }

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
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("IsEmployeeNotified", TypeName = "bit")]
    public bool? IsEmployeeNotified { get; set; }

    [Column("IsEmployeeManagerNotified", TypeName = "bit")]
    public bool? IsEmployeeManagerNotified { get; set; }
}