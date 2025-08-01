// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.IRepositories.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class EmailTypeRepository : Repository<EmailType>,
	IEmailTypeRepository
{
	public EmailTypeRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<int> GetEmailTypeIdByEmailNameAsync(string name)
	{
		return await (from emailType in DatabaseContext.EmailTypes
			where emailType.EmailName == name
			select emailType.Id).FirstOrDefaultAsync();
	}
}