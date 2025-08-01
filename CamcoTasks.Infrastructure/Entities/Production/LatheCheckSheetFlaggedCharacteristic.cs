// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_LatheCheckSheet_FlaggedCharacteristics")]
public class LatheCheckSheetFlaggedCharacteristic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public int Id { get; set; }

    [Column("FlaggedOptionId", TypeName = "int")]
    public int? FlaggedOptionId { get; set; }

    [Column("CharacteristicId", TypeName = "int")]
    public int? CharacteristicId { get; set; }

    [Column("FlaggedBy", TypeName = "bigint")]
    public long? FlaggedBy { get; set; }

    [Column("FlaggedDate", TypeName = "datetime")]
    public DateTime? FlaggedDate { get; set; }

    [Column("CharacteristicField", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string CharacteristicField { get; set; }
}