using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.HR;

[Table("HR_PublicEmployees")]
public class PublicEmployee
{
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("EmpID", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string EmployeeId { get; set; }

    [Column("EmpFirstName", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string EmployeeFirstName { get; set; }

    [Column("EmpLastName", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string EmployeeLastName { get; set; }

    [Column("FullName", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string FullName { get; set; }

    [Column("Email", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Email { get; set; }

    [Column("Department", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string Department { get; set; }

    [Column("Active", TypeName = "bit")] public bool IsActive { get; set; }

    [Column("ProductionPercent", TypeName = "money")]
    public decimal? ProductionPercent { get; set; }
}