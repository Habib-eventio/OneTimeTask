using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_Applications")]
public class Application
{
    [Column("Id", TypeName = "smallint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }

    [Column("ApplicationName", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string ApplicationName { get; set; }
}