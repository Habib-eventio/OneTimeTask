// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_RoutingAttachmentTypes")]
public class RoutingAttachmentType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("Name", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    [Required]
    public string Name { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }
}