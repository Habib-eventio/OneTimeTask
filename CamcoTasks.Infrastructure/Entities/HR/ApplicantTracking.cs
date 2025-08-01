//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ERP.Data.Entities.HR;

//[Table("HR_ApplicantTracking")]
//public class ApplicantTracking
//{
//    [Column("Id", TypeName = "bigint")]
//    [Key]
//    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//    public long Id { get; set; }

//    [Column("StatusId", TypeName = "bigint")]
//    public long StatusId { get; set; }

//    [Column("ApplicationMethodId", TypeName = "bigint")]
//    public long? ApplicationMethodId { get; set; }

//    [Column("LastName", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string LastName { get; set; }

//    [Column("FirstName", TypeName = "nvarchar(255)")]
//    [StringLength(255)]
//    [MaxLength(255)]
//    public string FirstName { get; set; }

//    [Column("PhoneNo", TypeName = "varchar(50)")]
//    [StringLength(50)]
//    [MaxLength(50)]
//    public string PhoneNo { get; set; }

//    [Column("AppliedJobsId", TypeName = "varchar(MAX)")]
//    [MaxLength]
//    public string AppliedJobsId { get; set; }

//    [Browsable(false)]
//    [Column("CreatedById", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectCreatedByIdEmployeeId Column). Please refer to CorrectCreatedByIdEmployeeId")]
//    public long? ObsoleteCreatedById { get; set; }

//    [Column("CorrectCreatedByIdEmployeeId", TypeName = "bigint")]
//    public long? CreatedByEmployeeId { get; set; }

//    [Column("IsDeleted", TypeName = "bit")]
//    public bool? IsDeleted { get; set; }

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

//    [Column("DateApplied", TypeName = "datetime2(7)")]
//    public DateTime? DateApplied { get; set; }

//    [Column("PhoneInterviewDate", TypeName = "datetime2(7)")]
//    public DateTime? PhoneInterviewDate { get; set; }

//    [Column("PhoneInterviewStartTime", TypeName = "datetime2(7)")]
//    public DateTime? PhoneInterviewStartTime { get; set; }

//    [Column("PhoneInterviewEndTime", TypeName = "datetime2(7)")]
//    public DateTime? PhoneInterviewEndTime { get; set; }

//    [Column("OnSiteInterviewDate", TypeName = "datetime2(7)")]
//    public DateTime? OnSiteInterviewDate { get; set; }

//    [Column("OnSiteInterviewTime", TypeName = "datetime2(7)")]
//    public DateTime? OnSiteInterviewTime { get; set; }

//    [Browsable(false)]
//    [Column("OnSiteInterviewerEmployeeId", TypeName = "bigint")]
//    [Obsolete(
//        "This is Obsolete And It will be deleted (All Data of this column has already been moved to CorrectOnSiteInterviewerEmployeeIdEmployeeId Column). Please refer to CorrectOnSiteInterviewerEmployeeIdEmployeeId")]
//    public long? ObsoleteOnSiteInterviewerEmployeeId { get; set; }

//    [Column("CorrectOnSiteInterviewerEmployeeIdEmployeeId", TypeName = "bigint")]
//    public long? OnSiteInterviewerEmployeeId { get; set; }

//    [Column("DateOfHired", TypeName = "datetime2(7)")]
//    public DateTime? DateOfHired { get; set; }

//    [Column("StartDate", TypeName = "datetime2(7)")]
//    public DateTime? StartDate { get; set; }

//    [Column("ContactAttempts", TypeName = "int")]
//    public int? ContactAttempts { get; set; }

//    [Column("DateLastContactAttempt", TypeName = "datetime2(7)")]
//    public DateTime? DateLastContactAttempt { get; set; }

//    [Column("ReasonId", TypeName = "smallint")]
//    public short? ReasonId { get; set; }

//    [Column("Notes", TypeName = "varchar(MAX)")]
//    [MaxLength]
//    public string Notes { get; set; }

//    [Column("ApplicationAttachment", TypeName = "varchar(200)")]
//    [StringLength(200)]
//    [MaxLength(200)]
//    public string ApplicationAttachment { get; set; }

//    [Column("ResumeAttachment", TypeName = "varchar(200)")]
//    [StringLength(200)]
//    [MaxLength(200)]
//    public string ResumeAttachment { get; set; }

//    [Column("OthersAttachment", TypeName = "varchar(200)")]
//    [StringLength(200)]
//    [MaxLength(200)]
//    public string OthersAttachment { get; set; }
//}