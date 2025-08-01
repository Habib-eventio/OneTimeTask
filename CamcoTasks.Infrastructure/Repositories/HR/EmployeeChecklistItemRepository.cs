// This File Needs to be reviewed Still. Don't Remove this comment.


using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class EmployeeChecklistItemRepository : Repository<EmployeeChecklistItem>,
	IEmployeeChecklistItemRepository
{
	public EmployeeChecklistItemRepository(DatabaseContext context) : base(context)
	{

	}
}