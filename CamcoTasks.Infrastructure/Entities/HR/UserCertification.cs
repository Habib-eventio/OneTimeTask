//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_UserCertifications")]
//public class UserCertification
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("CertificationId", TypeName = "bigint")]
//    public long CertificationId { get; set; }

//    [Column("DateOfExpire", TypeName = "datetime2(7)")]
//    public DateTime? DateOfExpire { get; set; }

//    [Column("DateOfCertification", TypeName = "datetime2(7)")]
//    public DateTime DateOfCertification { get; set; }

//    [Column("Notes", TypeName = "varchar(500)")]
//    [StringLength(500)]
//    [MaxLength(500)]
//    public string Notes { get; set; }

//    [Browsable(false)]
//    [Column("EmployeeId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectEmployeeId Column). Please refer to CorrectEmployeeId")]
//    public long? ObsoleteEmployeeId { get; set; }

//    [Column("CorrectEmployeeId", TypeName = "bigint")]
//    public long EmployeeId { get; set; }

//    [Column("Attachment", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string Attachment { get; set; }

//    [Browsable(false)]
//    [Column("CreatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
//    public long? ObsoleteCreatedById { get; set; }

//    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
//    public long CreatedByEmployeeId { get; set; }

//    [Column("DateCreated", TypeName = "datetime2(7)")]
//    public DateTime DateCreated { get; set; }

//    [Column("IsDeleted", TypeName = "bit")]
//    public bool IsDeleted { get; set; }

//    [Column("IsEmployeeHavingCdlOrCdlPermit", TypeName = "bit")]
//    public bool? IsEmployeeHavingCdlOrCdlPermit { get; set; }

//    [Column("OriginOfCertificationId", TypeName = "smallint")]
//    public short? OriginOfCertificationId { get; set; }

//    [Column("OriginOfCertificationOtherDescription", TypeName = "varchar(200)")]
//    [StringLength(200)]
//    [MaxLength(200)]
//    public string OriginOfCertificationOtherDescription { get; set; }

//    [Column("IsReCertificationRequired", TypeName = "bit")]
//    public bool IsReCertificationRequired { get; set; }

//    [Column("IsMainCertification", TypeName = "bit")]
//    public bool IsMainCertification { get; set; }
//}