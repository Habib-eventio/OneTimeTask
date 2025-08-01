// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;

namespace ERP.Repository.IRepository.HR;

public interface IPermissionGroupRepository : IRepository<PermissionGroup>
{
	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<string> GetPermissionGroupNameByGroupIdAsync(int? permissionGroupId);
}