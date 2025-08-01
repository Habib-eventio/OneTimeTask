//// This File Needs to be reviewed Still. Don't Remove this comment.

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace CamcoTasks.Infrastructure.Entities.Production;

//[Table("Production_MaterialGradeCategories")]
//public class MaterialGradeCategory
//{
//    public MaterialGradeCategory()
//    {
//        ProductionMaterialGrades = new HashSet<MaterialGrade>();
//    }

//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    [Column("ID", TypeName = "int")]
//    public int Id { get; set; }

//    [Column("GradeCategory", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    [Required]
//    public string GradeCategory { get; set; }

//    [Column("EnterredBy", TypeName = "nvarchar(MAX)")]
//    [MaxLength]
//    [Required]
//    public string EnteredBy { get; set; }

//    [Column("DateAdded", TypeName = "datetime")]
//    public DateTime DateAdded { get; set; }

//    [Column("DateLastUpdated", TypeName = "datetime")]
//    public DateTime DateLastUpdated { get; set; }

//    public virtual ICollection<MaterialGrade> ProductionMaterialGrades { get; set; }
//}