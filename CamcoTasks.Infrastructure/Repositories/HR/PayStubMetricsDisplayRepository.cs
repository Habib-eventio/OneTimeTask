// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace ERP.Repository.Repository.HR;

public class PayStubMetricsDisplayRepository : Repository<PayStubMetricsDisplay>,
	IPayStubMetricsDisplayRepository
{
	public PayStubMetricsDisplayRepository(DatabaseContext context) : base(context)
	{

	}
}