// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace ERP.Repository.Repository.HR;

public class LoanBalanceHistoryRepository : Repository<LoanBalanceHistory>,
	ILoanBalanceHistoryRepository
{
	public LoanBalanceHistoryRepository(DatabaseContext context) : base(context)
	{

	}
}