using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_DepartmentAndManagers")]
public class DepartmentAndManager
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("DepartmentName", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string? DepartmentName { get; set; }

    [Column("DepartmentAbbreviation", TypeName = "varchar(2)")]
    [StringLength(2)]
    [MaxLength(2)]
    public string? DepartmentAbbreviation { get; set; }

    [Column("DepartmentImage", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string? DepartmentImage { get; set; }

    [Browsable(false)]
    [Column("PrimaryManagerId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectPrimaryManagerIdEmployeeId Column). Please refer to CorrectPrimaryManagerIdEmployeeId")]
    public long? ObsoletePrimaryManagerId { get; set; }

    [Column("CorrectPrimaryManagerIdEmployeeId", TypeName = "bigint")]
    public long PrimaryManagerEmployeeId { get; set; }

    [Column("PrimaryManagerCanApproveTimeSheet", TypeName = "bit")]
    public bool PrimaryManagerCanApproveTimeSheet { get; set; }

    [Browsable(false)]
    [Column("LeaderId", TypeName = "bigint")]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectLeaderIdEmployeeId Column). Please refer to CorrectLeaderIdEmployeeId")]
    public long? ObsoleteLeaderId { get; set; }

    [Column("CorrectLeaderIdEmployeeId", TypeName = "bigint")]
    public long? LeaderEmployeeId { get; set; }

    [Column("LeaderCanApproveTimeSheet", TypeName = "bit")]
    public bool LeaderCanApproveTimeSheet { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

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

}