// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using ERP.Data.Entities.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class ApplicantStatusRepository : Repository<ApplicantStatus>,
	IApplicantStatusRepository
{
	public ApplicantStatusRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<string> GetApplicationStatusFromStatusByStatusIdAsync(long id)
	{
		return await (from status in DatabaseContext.ApplicantStatuses
			where status.Id == id
			select status.StatusName).FirstOrDefaultAsync();
	}
}