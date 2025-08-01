// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class DrugTestRequestRepository : Repository<DrugTestRequest>,
	IDrugTestRequestRepository
{
	public DrugTestRequestRepository(DatabaseContext context) : base(context)
	{

	}
}