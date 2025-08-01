// This File Needs to be reviewed Still. Don't Remove this comment.

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Purchasing;

[Keyless]
[Table("Purchasing_MaterialUnitsOfMeasurement")]
public class MaterialUnitOfMeasurement
{
    [Column("UOM", TypeName = "nvarchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    public string UnitOfMeasurement { get; set; }
}