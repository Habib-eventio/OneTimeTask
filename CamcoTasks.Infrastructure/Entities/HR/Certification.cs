//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//using CamcoTasks.Infrastructure.Entities.HR;

//[Table("HR_Certifications")]
//public class Certification
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("Name", TypeName = "varchar(100)")]
//    [StringLength(100)]
//    [MaxLength(100)]
//    [Required]
//    public string Name { get; set; }

//    [Column("TypeId", TypeName = "int")] public int TypeId { get; set; }

//    [Column("Description", TypeName = "varchar(1000)")]
//    [StringLength(1000)]
//    [MaxLength(1000)]
//    [Required]
//    public string Description { get; set; }

//    [Column("IsCertificationIncludedInReport", TypeName = "bit")]
//    public bool IsCertificationIncludedInReport { get; set; }

//    [Column("IsDeleted", TypeName = "bit")]
//    public bool IsDeleted { get; set; }

//    [Browsable(false)]
//    [Column("CreatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
//    public long? ObsoleteCreatedById { get; set; }

//    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
//    public long? CreatedByEmployeeId { get; set; }

//    [Column("DateCreated", TypeName = "datetime2(7)")]
//    public DateTime? DateCreated { get; set; }

//    [Browsable(false)]
//    [Column("UpdatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
//    public long? ObsoleteUpdatedById { get; set; }

//    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
//    public long? UpdatedByEmployeeId { get; set; }

//    [Column("DateUpdated", TypeName = "datetime2(7)")]
//    public DateTime? DateUpdated { get; set; }
//}