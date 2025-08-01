// This File Needs to be reviewed Still. Don't Remove this comment.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Production;

[Table("Production_TimeSheetColumnsRowsData")]
public class TimeSheetColumnRowData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("DataFiledName", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string DataFiledName { get; set; }

    [Column("DataFieldValue", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string DataFieldValue { get; set; }

    [Column("IsErrorWithFiled", TypeName = "bit")]
    public bool IsErrorWithFiled { get; set; }

    [Column("ProductionTimeSheetId", TypeName = "int")]
    public int ProductionTimeSheetId { get; set; }

    [Column("IsMarkedDisabled", TypeName = "bit")]
    public bool IsMarkedDisabled { get; set; }
}