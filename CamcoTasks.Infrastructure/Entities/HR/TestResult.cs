using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_TestResults")]
public class TestResult
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("TestAssignmentId", TypeName = "bigint")]
    public long TestAssignmentId { get; set; }

    [Column("Question", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    [Required]
    public string Question { get; set; }

    [Column("QuestionTypeId", TypeName = "bigint")]
    public long QuestionTypeId { get; set; }

    [Column("QuestionImage", TypeName = "varchar(150)")]
    [StringLength(150)]
    [MaxLength(150)]
    public string QuestionImage { get; set; }

    [Column("Options", TypeName = "varchar(200)")]
    [StringLength(200)]
    [MaxLength(200)]
    public string Options { get; set; }

    [Column("CorrectAnswer", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    [Required]
    public string CorrectAnswer { get; set; }

    [Column("AnswerSupplied", TypeName = "varchar(500)")]
    [StringLength(500)]
    [MaxLength(500)]
    public string AnswerSupplied { get; set; }

    [Column("MarkAsCorrectAnswer", TypeName = "bit")]
    public bool? MarkAsCorrectAnswer { get; set; }
}