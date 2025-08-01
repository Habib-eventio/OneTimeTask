using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_TrainingAndCertificationRequests")]
public class TrainingAndCertificationRequest
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

    [Column("TrainingAndCertificationId", TypeName = "smallint")]
    public short? TrainingAndCertificationId { get; set; }

    [Column("RequestedTrainingAndCertificationId", TypeName = "smallint")]
    public short? RequestedTrainingAndCertificationId { get; set; }

    [Browsable(false)]
    [Column("EmployeeToBeTrainedId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeToBeTrainedIdEmployeeId Column). Please refer to CorrectEmployeeToBeTrainedIdEmployeeId")]
    public long? ObsoleteEmployeeToBeTrainedId { get; set; }

    [Column("CorrectEmployeeToBeTrainedIdEmployeeId", TypeName = "bigint")]
    public long EmployeeToBeTrainedEmployeeId { get; set; }

    [Column("Description", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string Description { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

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

    [Browsable(false)]
    [Column("TrainingAndCertificationAdministratorId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectTrainingAndCertificationAdministratorIdEmployeeId Column). Please refer to CorrectTrainingAndCertificationAdministratorIdEmployeeId")]
    public long? ObsoleteTrainingAndCertificationAdministratorId { get; set; }

    [Column("CorrectTrainingAndCertificationAdministratorIdEmployeeId", TypeName = "bigint")]
    public long? TrainingAndCertificationAdministratorEmployeeId { get; set; }

    [Column("ScheduledRequestDate", TypeName = "datetime")]
    public DateTime? ScheduledRequestDate { get; set; }

    [Column("ScheduledRequestTime", TypeName = "datetime2(7)")]
    public DateTime? ScheduledRequestTime { get; set; }

    [Column("AdministratorComment", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string AdministratorComment { get; set; }

    [Column("IsAppointmentCompleted", TypeName = "bit")]
    public bool IsAppointmentCompleted { get; set; }

    [Column("IsRequestCompleted", TypeName = "bit")]
    public bool IsRequestCompleted { get; set; }

    [Column("RequestDate", TypeName = "datetime")]
    public DateTime? RequestDate { get; set; }

    [Browsable(false)]
    [Column("RequestedEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectRequestedEmployeesId Column). Please refer to CorrectRequestedEmployeesId")]
    public string ObsoleteRequestedEmployeesId { get; set; }

    [Column("CorrectRequestedEmployeesId", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string RequestedEmployeesId { get; set; }

    [Column("NotifiedDepartmentId", TypeName = "bigint")]
    public long NotifiedDepartmentId { get; set; }
}