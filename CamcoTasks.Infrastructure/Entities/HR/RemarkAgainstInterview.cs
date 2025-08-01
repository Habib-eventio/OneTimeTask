//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_RemarkAgainstInterviews")]
//public class RemarkAgainstInterview
//{
//    [Column("RemarksAgainstInterviewId", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("InterviewScoreCardId", TypeName = "bigint")]
//    public long InterviewScoreCardId { get; set; }

//    [Column("EnumTypeOfEvaluationId", TypeName = "smallint")]
//    public short EnumTypeOfEvaluationId { get; set; }

//    [Column("Remarks", TypeName = "varchar(500)")]
//    [StringLength(500)]
//    [MaxLength(500)]
//    public string Remarks { get; set; }
//}