// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class EmployeeChangeRequestRepository : Repository<EmployeeChangeRequest>,
	IEmployeeChangeRequestRepository
{
	public EmployeeChangeRequestRepository(DatabaseContext context) : base(context)
	{

	}
}