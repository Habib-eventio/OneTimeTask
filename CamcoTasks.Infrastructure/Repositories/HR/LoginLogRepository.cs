// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class LoginLogRepository : Repository<LoginLog>,
	ILoginLogRepository
{
	public LoginLogRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<LoginLog> GetLatestLoginLogByEmployeeIdAsync(long employeeId, short applicationId)
	{
		return await (from loginLog in DatabaseContext.LoginLogs
			where loginLog.EmployeeId == employeeId && loginLog.ApplicationId == applicationId
			                                        && loginLog.SignedOutTime == null
			orderby loginLog.SignedInTime descending
			select loginLog).FirstOrDefaultAsync();
	}
}