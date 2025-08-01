using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_EvaluationStateAgainstInterviews")]
public class EvaluationStateAgainstInterview
{
    [Column("ScoresAgainstInterviewId", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("InterviewScoreCardId", TypeName = "bigint")]
    public long InterviewScoreCardId { get; set; }

    [Column("EnumTypeOfEvaluationId", TypeName = "smallint")]
    public short EnumTypeOfEvaluationId { get; set; }

    [Column("EnumEvaluationLevelId", TypeName = "smallint")]
    public short EnumEvaluationLevelId { get; set; }

    [Column("EvaluationState", TypeName = "bit")]
    public bool EvaluationState { get; set; }
}