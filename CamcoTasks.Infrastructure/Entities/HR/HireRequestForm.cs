//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_HireRequestForms")]
//public class HireRequestForm
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Browsable(false)]
//    [Column("RequestorId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectRequestorIdEmployeeId Column). Please refer to CorrectRequestorIdEmployeeId")]
//    public long? ObsoleteRequestorId { get; set; }

//    [Column("CorrectRequestorIdEmployeeId", TypeName = "bigint")]
//    public long RequestorEmployeeId { get; set; }

//    [Column("DepartmentId", TypeName = "bigint")]
//    public long DepartmentId { get; set; }

//    [Column("IsPastApplicantsReviewed", TypeName = "bit")]
//    public bool IsPastApplicantsReviewed { get; set; }

//    [Column("IsPastEmployeesReviewed", TypeName = "bit")]
//    public bool IsPastEmployeesReviewed { get; set; }

//    [Column("IsApplicantInMind", TypeName = "bit")]
//    public bool IsApplicantInMind { get; set; }

//    [Column("ApplicantNameInMind", TypeName = "varchar(100)")]
//    [StringLength(100)]
//    [MaxLength(100)]
//    public string ApplicantNameInMind { get; set; }

//    [Column("ContactInformationInMind", TypeName = "varchar(100)")]
//    [StringLength(100)]
//    [MaxLength(100)]
//    public string ContactInformationInMind { get; set; }

//    [Column("EnumPriorityId", TypeName = "smallint")]
//    public short EnumPriorityId { get; set; }

//    [Column("JobDescriptionId", TypeName = "bigint")]
//    public long JobDescriptionId { get; set; }

//    [Column("IsDeleted", TypeName = "bit")]
//    public bool IsDeleted { get; set; }

//    [Column("SignedDocument", TypeName = "varchar(250)")]
//    [StringLength(250)]
//    [MaxLength(250)]
//    public string SignedDocument { get; set; }

//    [Browsable(false)]
//    [Column("CreatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
//    public long? ObsoleteCreatedById { get; set; }

//    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
//    public long? CreatedByEmployeeId { get; set; }

//    [Column("DateCreated", TypeName = "datetime2(7)")]
//    public DateTime DateCreated { get; set; }

//    [Browsable(false)]
//    [Column("UpdatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectUpdatedByIdEmployeeId Column). Please refer to CorrectUpdatedByIdEmployeeId")]
//    public long? ObsoleteUpdatedById { get; set; }

//    [Column("CorrectUpdatedByIdEmployeeId", TypeName = "bigint")]
//    public long? UpdatedByEmployeeId { get; set; }

//    [Column("DateUpdated", TypeName = "datetime2(7)")]
//    public DateTime? DateUpdated { get; set; }

//    [Column("RequestStatusId", TypeName = "smallint")]
//    public short RequestStatusId { get; set; }

//    [Obsolete("ShiftId is deprecated, please refer to (HR_HireRequestFormAndShifts) table instead.")]
//    [Column("ShiftId", TypeName = "smallint")]
//    public short? ShiftId { get; set; }

//    [Column("NumberOfHires", TypeName = "int")]
//    public int NumberOfHires { get; set; }
//}