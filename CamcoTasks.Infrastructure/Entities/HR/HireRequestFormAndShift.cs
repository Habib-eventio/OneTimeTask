//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_HireRequestFormAndShifts")]
//public class HireRequestFormAndShift
//{
//    [Column("Id", TypeName = "int")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public int Id { get; set; }

//    [Column("ShiftId", TypeName = "smallint")]
//    public short ShiftId { get; set; }

//    [Column("HireRequestFormId", TypeName = "bigint")]
//    public long HireRequestFormId { get; set; }
//}