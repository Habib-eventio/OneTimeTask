// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using ERP.Repository.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class EmploymentHistoryRepository : Repository<EmploymentHistory>,
	IEmploymentHistoryRepository
{
	public EmploymentHistoryRepository(DatabaseContext context) : base(context)
	{

	}
}