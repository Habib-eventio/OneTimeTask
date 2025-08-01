using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Page;

[Table("PageLoadTime")]
public class LoadTime
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("PageName", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string PageName { get; set; }

    [Column("SectionName", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string SectionName { get; set; }

    [Column("StartTime", TypeName = "datetime2(7)")]
    public DateTime StartTime { get; set; }

    [Column("EndTime", TypeName = "datetime2(7)")]
    public DateTime EndTime { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }
}