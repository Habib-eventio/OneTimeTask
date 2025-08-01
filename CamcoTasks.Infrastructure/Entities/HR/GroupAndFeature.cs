using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_NewGroupsAndFeatures")]
public class GroupAndFeature
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("FeatureId", TypeName = "bigint")]
    public long FeatureId { get; set; }

    [Column("GroupId", TypeName = "bigint")]
    public long GroupId { get; set; }
}