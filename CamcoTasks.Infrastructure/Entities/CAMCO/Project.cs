using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.CAMCO;

[Table("Camco_Projects")]
public class Project
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    [Column("Title", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Title { get; set; }

    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Description { get; set; }

    [Column("IsActive", TypeName = "bit")] public bool? IsActive { get; set; }

    [Column("IsPostponed", TypeName = "bit")]
    public bool IsPostponed { get; set; } = false;

    [Column("DateCreated", TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [Browsable(false)]
    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEnteredByEmployeeId Column). Please refer to CorrectEnteredByEmployeeId")]
    public string ObsoleteEnteredBy { get; set; }

    [Column("CorrectEnteredByEmployeeId", TypeName = "bigint")]
    public long? EnteredByEmployeeId { get; set; }

    [Browsable(false)]
    [Column("Champion", TypeName = "nvarchar(255)")]
    [MaxLength(255)]
    [StringLength(255)]
    [Obsolete(
        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectChampionEmployeeId Column). Please refer to CorrectChampionEmployeeId")]
    public string ObsoleteChampion { get; set; }

    [Column("CorrectChampionEmployeeId", TypeName = "bigint")]
    public long? ChampionEmployeeId { get; set; }

    [Column("ProjectType", TypeName = "int")]
    public int? ProjectType { get; set; }

    [Column("DateUpdated", TypeName = "datetime")]
    public DateTime? DateUpdated { get; set; }

    [Column("Notes", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string? Notes { get; set; }

    [Column("Status", TypeName = "nvarchar(255)")]
    [MaxLength(255)]
    [StringLength(255)]
    public string? Status { get; set; }

    [Column("PostponedReason", TypeName = "nvarchar(255)")]
    [MaxLength(255)]
    [StringLength(255)]
    public string? PostponedReason { get; set; }

    [Column("UpdatedById", TypeName = "bigint")]
    public long? UpdatedById { get; set; }
}