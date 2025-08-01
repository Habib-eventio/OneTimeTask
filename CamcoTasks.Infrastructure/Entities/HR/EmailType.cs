using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("HR_EmailTypes")]
public class EmailType
{
    [Column("Id", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ApplicationId", TypeName = "smallint")]
    public short ApplicationId { get; set; }

    [Column("EmailName", TypeName = "varchar(255)")]
    [StringLength(255)]
    [MaxLength(255)]
    [Required]
    public string EmailName { get; set; }

    [Column("EmailTypeDescription", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string EmailTypeDescription { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

    [Column("EmailCategoryId", TypeName = "smallint")]
    public short? EmailCategoryId { get; set; }

    [Column("DateEntered", TypeName = "datetime2(7)")]
    public DateTime DateEntered { get; set; }
}