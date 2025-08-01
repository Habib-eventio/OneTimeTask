using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_NewFeatures")]
public class Feature
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("FeatureName", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string FeatureName { get; set; }

    [Column("ApplicationId", TypeName = "smallint")]
    public short ApplicationId { get; set; }

    [Column("RoleNameForFeature", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string RoleNameForFeature { get; set; }
}