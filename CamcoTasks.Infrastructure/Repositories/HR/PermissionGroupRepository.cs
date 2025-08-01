// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using ERP.Repository.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class PermissionGroupRepository : Repository<PermissionGroup>,
	IPermissionGroupRepository
{
	public PermissionGroupRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<string> GetPermissionGroupNameByGroupIdAsync(int? permissionGroupId)
	{
		var group = await (from permissionGroup in DatabaseContext.PermissionGroups
			where permissionGroup.Id == permissionGroupId
			select permissionGroup).FirstOrDefaultAsync();

		return group != null ? group.GroupName : "Not Assigned";
	}
}