// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.IRepository.Automated;
using Microsoft.EntityFrameworkCore;

namespace ERP.Repository.Repository.Automated;

public class EmailQueueRepository : Repository<EmailQueue>,
	IEmailQueueRepository
{
	public EmailQueueRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<List<EmailQueue>> GetLatestEmailQueuesSentByAndEmailTypeIdAsync(int emailTypeId)
	{
		var latestData = await (from email in DatabaseContext.EmailQueues
			where email.EmailTypeId == emailTypeId
			orderby email.TimeSent
			select email.TimeSent).FirstOrDefaultAsync();

		return await (from emailQueue in DatabaseContext.EmailQueues
			where emailQueue.EmailTypeId == emailTypeId && emailQueue.TimeSent == latestData
			select emailQueue).ToListAsync();
	}
}