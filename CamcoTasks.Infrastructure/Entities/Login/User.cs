using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamcoTasks.Infrastructure.Entities;

[Table("Login_Users")]
public class User : IdentityUser<long>
{
    [NotMapped]
    public bool IsPasswordCreated { get; set; }

    [NotMapped]
    public DateTime PasswordCreationTime { get; set; }

    [NotMapped]
    public int? PermissionGroupId { get; set; }

    [NotMapped]
    public bool CanViewAllEmployees { get; set; }

    [NotMapped]
    public bool ShouldOverrideJobTitlePermissions { get; set; }
}