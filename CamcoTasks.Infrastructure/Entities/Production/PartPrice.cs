//// This File Needs to be reviewed Still. Don't Remove this comment.

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Production;

//[Table("Production_PartPrice")]
//public class PartPrice
//{
//    [Column("PM_PART", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string PmPart { get; set; }

//    [Column("PART_PRICE", TypeName = "money")]
//    public decimal? PartPriceValue { get; set; }

//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ID", TypeName = "int")]
//    public int Id { get; set; }
//}