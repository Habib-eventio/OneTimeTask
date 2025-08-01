//// This File Needs to be reviewed Still. Don't Remove this comment.

//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Production;

//[Table("Production_OverheadData")]
//public class OverheadDatum
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("BurdenRate", TypeName = "money")]
//    public decimal BurdenRate { get; set; }

//    [Column("BurdenDate", TypeName = "datetime")]
//    public DateTime BurdenDate { get; set; }
//}