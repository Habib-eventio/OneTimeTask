// This File Needs to be reviewed Still. Don't Remove this comment.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Quality;

[Table("Quality_CPARUpdates")]
public class CparUpdate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("UpdateID", TypeName = "int")]
    public int UpdateId { get; set; }

    [Column("CPARNumber", TypeName = "int")]
    public int? CparNumber { get; set; }

    [Column("UpdateDate", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("Update", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Update { get; set; }

    [Column("EmployeeID", TypeName = "int")]
    public int? EmployeeId { get; set; }
}