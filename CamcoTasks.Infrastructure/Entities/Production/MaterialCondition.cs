//// This File Needs to be reviewed Still. Don't Remove this comment.

//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.Production;

//[Table("Production_MaterialConditions")]
//public class MaterialCondition
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("Condition", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    [Required]
//    public string Condition { get; set; }

//    [Column("EnteredBy", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    [Required]
//    public string EnteredBy { get; set; }

//    [Column("DateAdded", TypeName = "datetime")]
//    public DateTime? DateAdded { get; set; }

//    [Column("DateLastUpdated", TypeName = "datetime")]
//    public DateTime? DateLastUpdated { get; set; }
//}