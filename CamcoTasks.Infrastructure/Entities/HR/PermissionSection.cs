//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Repository.Repository.HR;

//[Table("HR_PermissionSections")]
//public class PermissionSection
//{
//    [Column("Id", TypeName = "int")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public int Id { get; set; }

//    [Column("ApplicationId", TypeName = "smallint")]
//    public short ApplicationId { get; set; }

//    [Column("SectionName", TypeName = "nvarchar(50)")]
//    [Required]
//    [MaxLength(50)]
//    public string SectionName { get; set; }
//}