using Microsoft.AspNetCore.Identity;
namespace ERP.Data.Entities.Login;

public class User : IdentityUser<long>
{
    public bool IsPasswordCreated { get; set; }
    public DateTime PasswordCreationTime { get; set; }
    public int? PermissionGroupId { get; set; }
}
