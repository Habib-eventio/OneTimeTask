using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_DrugTestRequests")]
public class DrugTestRequest
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Browsable(false)]
    [Column("RequesterId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectRequesterIdEmployeeId Column). Please refer to CorrectRequesterIdEmployeeId")]
    public long? ObsoleteRequesterId { get; set; }

    [Column("CorrectRequesterIdEmployeeId", TypeName = "bigint")]
    public long RequesterEmployeeId { get; set; }

    [Browsable(false)]
    [Column("PersonBeingTestedId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectPersonBeingTestedIdEmployeeId Column). Please refer to CorrectPersonBeingTestedIdEmployeeId")]
    public long? ObsoletePersonBeingTestedId { get; set; }

    [Column("CorrectPersonBeingTestedIdEmployeeId", TypeName = "bigint")]
    public long PersonBeingTestedEmployeeId { get; set; }

    [Column("DateToTest", TypeName = "datetime")]
    public DateTime DateToTest { get; set; }

    [Column("Description", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string Description { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Browsable(false)]
    [Column("UpdatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
    public long? ObsoleteUpdatedById { get; set; }

    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
    public long? UpdatedByEmployeeId { get; set; }

    [Column("DateUpdated", TypeName = "datetime")]
    public DateTime? DateUpdated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("IsAppointmentCompleted", TypeName = "bit")]
    public bool IsAppointmentCompleted { get; set; }

    [Column("IsTestCompleted", TypeName = "bit")]
    public bool IsTestCompleted { get; set; }

    [Column("DrugTestRequestReasonId", TypeName = "smallint")]
    public short? DrugTestRequestReasonId { get; set; }
}