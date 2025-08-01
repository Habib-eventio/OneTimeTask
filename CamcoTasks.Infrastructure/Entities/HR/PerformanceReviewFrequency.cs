//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_PerformanceReviewFrequencies")]
//public class PerformanceReviewFrequency
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("FrequencyName", TypeName = "varchar(50)")]
//    [StringLength(50)]
//    [MaxLength(50)]
//    [Required]
//    public string FrequencyName { get; set; }

//    [Column("FrequencyDurationMonthStart", TypeName = "bigint")]
//    public long FrequencyDurationMonthStart { get; set; }

//    [Column("FrequencyDurationMonthEnd", TypeName = "bigint")]
//    public long FrequencyDurationMonthEnd { get; set; }
//}