//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_EmployeeRelatedDocuments")]
//public class EmployeeRelatedDocument
//{
//    [Key]
//    [Column("Id", TypeName = "bigint")]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("Title", TypeName = "varchar(100)")]
//    [StringLength(100)]
//    [MaxLength(100)]
//    [Required]
//    public string Title { get; set; }

//    [Column("OtherAttachment", TypeName = "nvarchar(500)")]
//    [StringLength(500)]
//    [MaxLength(500)]
//    [Required]
//    public string OtherAttachment { get; set; }

//    [Browsable(false)]
//    [Column("EmployeeId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
//    public long? ObsoleteEmployeeId { get; set; }

//    [Column("CorrectEmployeeId", TypeName = "bigint")]
//    public long EmployeeId { get; set; }

//    [Column("DateUploaded", TypeName = "datetime2(7)")]
//    public DateTime? DateUploaded { get; set; }
//}