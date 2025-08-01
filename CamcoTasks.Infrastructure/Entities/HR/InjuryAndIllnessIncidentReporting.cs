using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_InjuryAndIllnessIncidentReporting")]
public class InjuryAndIllnessIncidentReporting
{
    [Key]
    [Column("Id", TypeName = "bigint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Browsable(false)]
    [Column("IncidentReporterId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectIncidentReporterIdEmployeeId Column). Please refer to CorrectIncidentReporterIdEmployeeId")]
    public long? ObsoleteIncidentReporterId { get; set; }

    [Column("CorrectIncidentReporterIdEmployeeId", TypeName = "bigint")]
    public long IncidentReporterEmployeeId { get; set; }

    [Browsable(false)]
    [Column("EmployeeWithInjuryOrIllnessId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeWithInjuryOrIllnessIdEmployeeId Column). Please refer to CorrectEmployeeWithInjuryOrIllnessIdEmployeeId")]
    public long? ObsoleteEmployeeWithInjuryOrIllnessId { get; set; }

    [Column("CorrectEmployeeWithInjuryOrIllnessIdEmployeeId", TypeName = "bigint")]
    public long EmployeeWithInjuryOrIllnessEmployeeId { get; set; }

    [Column("DateOfIncident", TypeName = "datetime2(7)")]
    public DateTime DateOfIncident { get; set; }

    [Column("TimeOfIncident", TypeName = "datetime2(7)")]
    public DateTime TimeOfIncident { get; set; }

    [Column("DepartmentWhereIncidentOccuredId", TypeName = "bigint")]
    public long DepartmentWhereIncidentOccuredId { get; set; }

    [Column("ActivityThatCausedIncident", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ActivityThatCausedIncident { get; set; }

    [Column("IncidentThatCausedInjuryOrAccident", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string IncidentThatCausedInjuryOrAccident { get; set; }

    [Column("WhatIsTheIllnessOrInjury", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string WhatIsTheIllnessOrInjury { get; set; }

    [Column("ObjectOrSubstanceCausingIncident", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string ObjectOrSubstanceCausingIncident { get; set; }

    [Browsable(false)]
    [Column("CreatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
    public long? ObsoleteCreatedById { get; set; }

    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Browsable(false)]
    [Column("UpdatedById", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
    public long? ObsoleteUpdatedById { get; set; }

    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
    public long? UpdatedByEmployeeId { get; set; }

    [Column("DateUpdated", TypeName = "datetime2(7)")]
    public DateTime? DateUpdated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("EnumTypeOfIncidentId", TypeName = "smallint")]
    public short? EnumTypeOfIncidentId { get; set; }

    [Column("DateOfClaimRequest", TypeName = "datetime2(7)")]
    public DateTime? DateOfClaimRequest { get; set; }

    [Column("ClaimNumber", TypeName = "int")]
    public int? ClaimNumber { get; set; }

    [Column("DateOfApproval", TypeName = "datetime2(7)")]
    public DateTime? DateOfApproval { get; set; }

    [Column("AmountApproved", TypeName = "decimal(18, 0)")]
    public decimal? AmountApproved { get; set; }

    [Column("EnumTypeOfInsuranceId", TypeName = "smallint")]
    public short? EnumTypeOfInsuranceId { get; set; }
}