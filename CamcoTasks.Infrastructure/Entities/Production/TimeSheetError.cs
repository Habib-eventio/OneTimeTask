// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Production;

[Table("Production_TimeSheetErrors")]
public class TimeSheetError
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ProductionSheetErrorId", TypeName = "int")]
    public int Id { get; set; }

    [Column("EmployeeId", TypeName = "bigint")]
    public long EmployeeId { get; set; }

    [Column("MistakeDate", TypeName = "datetime2(7)")]
    public DateTime MistakeDate { get; set; }

    [Column("IsMistakeFixed", TypeName = "bit")]
    public bool IsMistakeFixed { get; set; }

    [Column("DateModified", TypeName = "datetime2(7)")]
    public DateTime DateModified { get; set; }

    [Column("EnterById", TypeName = "bigint")]
    public long EnterById { get; set; }

    [Column("DateAdded", TypeName = "datetime2(7)")]
    public DateTime DateAdded { get; set; }

    [Column("ModifiedById", TypeName = "bigint")]
    public long? ModifiedById { get; set; }

    [Column("ProductionTimeSheetId", TypeName = "int")]
    public int ProductionTimeSheetId { get; set; }

    [Column("OriginalErrorValue", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    [Required]
    public string OriginalErrorValue { get; set; }

    [Column("FixedErrorValue", TypeName = "varchar(50)")]
    [StringLength(50)]
    [MaxLength(50)]
    public string FixedErrorValue { get; set; }

    [Column("RowColumnName", TypeName = "varchar(25)")]
    [StringLength(25)]
    [MaxLength(25)]
    [Required]
    public string RowColumnName { get; set; }
}