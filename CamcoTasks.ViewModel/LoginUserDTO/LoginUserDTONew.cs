using CamcoTasks.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.LoginDTO
{
    public static class LoginUserDTONew
    {
        public static User Map(LoginViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new User
            {
                Id = Convert.ToInt64(viewModel.Id),
                UserName = viewModel.UserName,
                NormalizedUserName = viewModel.NormalizedUserName,
                Email = viewModel.Email,
                NormalizedEmail = viewModel.NormalizedEmail,
                EmailConfirmed = viewModel.EmailConfirmed,
                PasswordHash = viewModel.PasswordHash,
                SecurityStamp = viewModel.SecurityStamp,
                ConcurrencyStamp = viewModel.ConcurrencyStamp,
                PhoneNumber = viewModel.PhoneNumber,
                PhoneNumberConfirmed = viewModel.PhoneNumberConfirmed,
                TwoFactorEnabled = viewModel.TwoFactorEnabled,
                LockoutEnd = viewModel.LockoutEnd,
                LockoutEnabled = viewModel.LockoutEnabled,
                AccessFailedCount = viewModel.AccessFailedCount,
                IsPasswordCreated = viewModel.IsPasswordCreated,
                PasswordCreationTime = viewModel.PasswordCreationTime,
                PermissionGroupId = viewModel.PermissionGroupId,
            };
        }

        public static LoginViewModel Map(User entity)
        {
            if (entity == null) { return null; }

            return new LoginViewModel
            {
                Id = entity.Id,
                UserName = entity.UserName,
                NormalizedUserName = entity.NormalizedUserName,
                Email = entity.Email,
                NormalizedEmail = entity.NormalizedEmail,
                EmailConfirmed = entity.EmailConfirmed,
                PasswordHash = entity.PasswordHash,
                SecurityStamp = entity.SecurityStamp,
                ConcurrencyStamp = entity.ConcurrencyStamp,
                PhoneNumber = entity.PhoneNumber,
                PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
                TwoFactorEnabled = entity.TwoFactorEnabled,
                LockoutEnd = entity.LockoutEnd,
                LockoutEnabled = entity.LockoutEnabled,
                AccessFailedCount = entity.AccessFailedCount,
                IsPasswordCreated = entity.IsPasswordCreated,
                PasswordCreationTime = entity.PasswordCreationTime,
                PermissionGroupId = entity.PermissionGroupId,
            };
        }

        public static IEnumerable<LoginViewModel> Map(IEnumerable<User> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
