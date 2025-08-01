// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_LineInspectionsGoal")]
public class LineInspectionGoal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("ColumnName", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ColumnName { get; set; }

    [Column("ColumnValue", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string ColumnValue { get; set; }
}