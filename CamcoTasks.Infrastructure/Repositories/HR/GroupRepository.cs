// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.InfrastructureIRepository.HR;

namespace  CamcoTasks.Infrastructure.Repository.HR;

public class GroupRepository : Repository<Group>,
	IGroupRepository
{
	public GroupRepository(DatabaseContext context) : base(context)
	{

	}
}