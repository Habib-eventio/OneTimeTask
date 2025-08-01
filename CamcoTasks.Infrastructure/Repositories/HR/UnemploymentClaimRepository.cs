// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace ERP.Repository.Repository.HR;

public class UnemploymentClaimRepository : Repository<UnemploymentClaim>,
	IUnemploymentClaimRepository
{
	public UnemploymentClaimRepository(DatabaseContext context) : base(context)
	{

	}
}