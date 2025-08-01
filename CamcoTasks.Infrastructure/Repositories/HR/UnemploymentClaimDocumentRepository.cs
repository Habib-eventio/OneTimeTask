// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using ERP.Data.Entities.HR;
using ERP.Repository.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class UnemploymentClaimDocumentRepository : Repository<UnemploymentClaimDocument>,
	IUnemploymentClaimDocumentRepository
{
	public UnemploymentClaimDocumentRepository(DatabaseContext context) : base(context)
	{

	}
}