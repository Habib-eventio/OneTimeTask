using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("HR_States")]
public class State
{
    [Column("Id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("Name", TypeName = "varchar(30)")]
    [StringLength(30)]
    [MaxLength(30)]
    [Required]
    public string Name { get; set; }

    [Column("Acronym", TypeName = "varchar(3)")]
    [StringLength(30)]
    [MaxLength(30)]
    [Required]
    public string Acronym { get; set; }
}