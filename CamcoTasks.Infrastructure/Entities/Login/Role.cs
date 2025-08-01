using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities.Login;

[Table("Login_Roles")]
public class Role : IdentityRole<long>
{
    [Column("Description", TypeName = "nvarchar(MAX)")]
    [MaxLength]
    public string Description { get; set; }

    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }
}
