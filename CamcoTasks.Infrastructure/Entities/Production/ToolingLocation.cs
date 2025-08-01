//// This File Needs to be reviewed Still. Don't Remove this comment.

//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Production;

//[Table("Production_ToolingLocations")]
//public class ToolingLocation
//{
//    [Column("Location", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Location { get; set; }

//    [Column("QRN", TypeName = "int")] public int QRN { get; set; }

//    [Column("Cell", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Cell { get; set; }

//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("DateEntered", TypeName = "datetime2(7)")]
//    public DateTime? DateEntered { get; set; }
//}