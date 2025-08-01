using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.HR;

[Table("HR_LogTypes")]
public class LogType
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Type", TypeName = "varchar(150)")]
    [StringLength(150)]
    [MaxLength(150)]
    public string Type { get; set; }

    [Column("EnumTypeId", TypeName = "smallint")]
    public short EnumTypeId { get; set; }
}