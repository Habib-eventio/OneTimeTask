// This File Needs to be reviewed Still. Don't Remove this comment.


using CamcoTasks.Infrastructure.Entities.TimeClock;
using CamcoTasks.Infrastructure.IRepository.TimeClock;

namespace CamcoTasks.Infrastructure.Repository.TimeClock;

public class BusinessTimeRequestRepository : Repository<BusinessTimeRequest>,
	IBusinessTimeRequestRepository
{
	public BusinessTimeRequestRepository(DatabaseContext context) : base(context)
	{

	}
}