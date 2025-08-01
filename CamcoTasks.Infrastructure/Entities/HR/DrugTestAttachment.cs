//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_DrugTestAttachments")]
//public class DrugTestAttachment
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("DrugTestId", TypeName = "bigint")]
//    public long DrugTestId { get; set; }

//    [Column("FileName", TypeName = "varchar(200)")]
//    [StringLength(200)]
//    [MaxLength(200)]
//    [Required]
//    public string FileName { get; set; }

//    [Column("IsDeleted", TypeName = "bit")]
//    public bool IsDeleted { get; set; }
//}