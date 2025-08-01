//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_PerformanceReviewFrequencyAllocation")]
//public class PerformanceReviewFrequencyAllocation
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Browsable(false)]
//    [Column("EmployeeId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
//    public long? ObsoleteEmployeeId { get; set; }

//    [Column("CorrectEmployeeId", TypeName = "bigint")]
//    public long EmployeeId { get; set; }

//    [Column("FrequencyId", TypeName = "bigint")]
//    public long? FrequencyId { get; set; }
//}