using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.CAMCO;

[Table("CamcoJobHistory")]
public class JobHistory
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Type", TypeName = "int")] public int Type { get; set; }

    [Column("DateCreated", TypeName = "datetime2(7)")]
    public DateTime DateCreated { get; set; }

    [Column("Priority", TypeName = "int")] public int Priority { get; set; }

    [Column("AssignedTo", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string AssignedTo { get; set; }

    [Column("DateCompleted", TypeName = "datetime2(7)")]
    public DateTime DateCompleted { get; set; }

    [Column("DateOriginalAdded", TypeName = "datetime2(7)")]
    public DateTime DateOriginalAdded { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("OrderNumber", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string OrderNumber { get; set; }

    [Column("DueDate", TypeName = "datetime2(7)")]
    public DateTime DueDate { get; set; }
}