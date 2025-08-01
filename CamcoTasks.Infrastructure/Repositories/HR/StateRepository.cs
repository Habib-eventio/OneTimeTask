// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class StateRepository : Repository<State>,
	IStateRepository
{
	public StateRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<string> GetNameFromStateByIdAsync(long stateId)
	{
		var stateName = await (from state in DatabaseContext.States
			where state.Id == stateId
			select state.Name).FirstOrDefaultAsync();
		return stateName ?? "NOT FOUND";
	}
}