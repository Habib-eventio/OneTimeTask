using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_LeaveOfAbsences")]
public class LeaveOfAbsence
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("EmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("TypeOfLeaveId", TypeName = "smallint")]
    public short TypeOfLeaveId { get; set; }

    [Column("LeaveOfAbsenceStartDate", TypeName = "datetime2(7)")]
    public DateTime LeaveOfAbsenceStartDate { get; set; }

    [Column("LeaveOfAbsenceEndDate", TypeName = "datetime2(7)")]
    public DateTime LeaveOfAbsenceEndDate { get; set; }

    [Column("DateOfExpectedReturn", TypeName = "datetime2(7)")]
    public DateTime DateOfExpectedReturn { get; set; }

    [Column("Detail", TypeName = "nvarchar(500)")]
    [Required]
    public string Detail { get; set; }

    [Column("Attachment", TypeName = "nvarchar(MAX)")]
    public string Attachment { get; set; }

    [Column("CreatedByEmployeeId", TypeName = "bigint")]
    public long? CreatedByEmployeeId { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime? DateCreated { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}