using Microsoft.AspNetCore.Identity;
namespace ERP.Data.Entities.Login;

public class Role : IdentityRole<long>
{
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
}
