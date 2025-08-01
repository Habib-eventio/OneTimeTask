//// This File Needs to be reviewed Still. Don't Remove this comment.

//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace CamcoTasks.Infrastructure.Entities.Production;

//[Table("Production_MaterialGrades")]
//public class MaterialGrade
//{
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("Grade", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    [Required]
//    public string Grade { get; set; }

//    [Column("GradeCategoryId", TypeName = "int")]
//    public int GradeCategoryId { get; set; }

//    [Column("EnterredBy", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    [Required]
//    public string EnteredBy { get; set; }

//    [Column("DateAdded", TypeName = "datetime")]
//    public DateTime DateAdded { get; set; }

//    [Column("DateLastUpdated", TypeName = "datetime")]
//    public DateTime DateLastUpdated { get; set; }

//    public virtual MaterialGradeCategory GradeCategory { get; set; }
//}