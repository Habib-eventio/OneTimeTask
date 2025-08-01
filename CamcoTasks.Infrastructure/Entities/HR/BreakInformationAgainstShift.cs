//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_BreakInformationAgainstShift")]
//public class BreakInformationAgainstShift
//{
//    [Key]
//    [Column("Id", TypeName = "int")]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public int Id { get; set; }

//    [Column("ShiftId", TypeName = "smallint")]
//    public short ShiftId { get; set; }

//    [Column("StartTime", TypeName = "datetime")]
//    public DateTime StartTime { get; set; }

//    [Column("QuitTime", TypeName = "datetime")]
//    public DateTime QuitTime { get; set; }

//    [Column("FirstBreakStartTime", TypeName = "datetime")]
//    public DateTime FirstBreakStartTime { get; set; }

//    [Column("FirstBreakEndTime", TypeName = "datetime")]
//    public DateTime FirstBreakEndTime { get; set; }

//    [Column("LunchBreakStartTime", TypeName = "datetime")]
//    public DateTime LunchBreakStartTime { get; set; }

//    [Column("LunchBreakEndTime", TypeName = "datetime")]
//    public DateTime LunchBreakEndTime { get; set; }

//    [Browsable(false)]
//    [Column("CreatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
//    public long? ObsoleteCreatedById { get; set; }

//    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
//    public long CreatedByEmployeeId { get; set; }

//    [Column("DateCreated", TypeName = "datetime")]
//    public DateTime DateCreated { get; set; }

//    [Column("IsDeleted", TypeName = "bit")]
//    public bool IsDeleted { get; set; }
//}