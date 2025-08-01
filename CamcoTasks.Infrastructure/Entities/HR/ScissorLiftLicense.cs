using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_ScissorLiftLicenses")]
public class ScissorLiftLicense
{
    [Column("EMPLOYEE", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Obsolete(
        "Keep Using this as you are using before. But in parallel, Only Start Adding Data to CorrectEmployeeEmployeeId. The" +
        " Data we are going to add here is Id (Primary Key) of [Hr_Employees] Table. ")]
    public string Employee { get; set; }

    [Column("CorrectEmployeeEmployeeId", TypeName = "bigint")]
    public long? CorrectEmployeeEmployeeId { get; set; }

    [Column("CERT DATE", TypeName = "datetime")]
    public DateTime? CertificationDate { get; set; }

    [Column("EXPIRES", TypeName = "datetime")]
    public DateTime? Expires { get; set; }

    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}