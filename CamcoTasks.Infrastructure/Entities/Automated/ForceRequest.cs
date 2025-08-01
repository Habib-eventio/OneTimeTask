using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Automated;

[Table("Automated_ForceRequests")]
public class ForceRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [MaxLength]
    [Column("AutomationType", TypeName = "nvarchar(MAX)")]
    [Obsolete("This is Now Obsolete. Instead Refer to EmailTypeId")]
    public string Type { get; set; }

    [Column("EmailTypeId", TypeName = "int")]
    public int EmailTypeId { get; set; }

    [Column("RequestForce", TypeName = "bit")]
    public bool IsForceRequest { get; set; }
}