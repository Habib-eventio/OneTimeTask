using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_WeeklyWage")]
public class WeeklyWage
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("HR_Employee", TypeName = "nchar(10)")]
    [StringLength(10)]
    [MaxLength(10)]
    [Obsolete(
        "Keep Using this as you are using before. But in parallel, Only Start Adding Data to CorrectCustomEmployeeIdEmployeeId. The" +
        " Data we are going to add here is Id (Primary Key) of [Hr_Employees] Table. ")]
    public string CustomEmployeeId { get; set; }

    [Column("CorrectCustomEmployeeIdEmployeeId", TypeName = "bigint")]
    public long? CorrectCustomEmployeeIdEmployeeId { get; set; }

    [Column("HR_Hourly Wage", TypeName = "money")]
    public decimal? HourlyWage { get; set; }

    [Column("HR_Date", TypeName = "datetime")]
    public DateTime? Date { get; set; }
}