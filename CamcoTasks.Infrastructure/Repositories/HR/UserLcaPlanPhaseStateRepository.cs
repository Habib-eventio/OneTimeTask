using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace ERP.Repository.Repository.HR;

public class UserLcaPlanPhaseStateRepository : Repository<UserLcaPlanPhaseState>,
	IUserLcaPlanPhaseStateRepository
{
	public UserLcaPlanPhaseStateRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<UserLcaPlanPhaseState> GetActivePlanDetailsByEmployeeIdAsync(long employeeId)
	{
		return await (from userLcaPlanPhaseState in DatabaseContext.UserLcaPlanPhaseStates
			where userLcaPlanPhaseState.EmployeeId == employeeId && userLcaPlanPhaseState.IsCompleted == false
			select userLcaPlanPhaseState).OrderBy(a => a.DateCreated).FirstOrDefaultAsync();
	}

	public async Task<bool> CheckIfActiveLcaPlanExistByEmployeeIdAsync(long userId)
	{
		return await (from userLcaPlanPhaseState in DatabaseContext.UserLcaPlanPhaseStates
			where userLcaPlanPhaseState.EmployeeId == userId && userLcaPlanPhaseState.IsCompleted == false
			select userLcaPlanPhaseState).AnyAsync();
	}
}