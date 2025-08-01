// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TimeClock;
using CamcoTasks.Infrastructure.IRepositories.TimeClock;


namespace CamcoTasks.Infrastructure.Repository.TimeClock;

public class BusinessTimeRepository : Repository<BusinessTime>,
	IBusinessTimeRepository
{
	public BusinessTimeRepository(DatabaseContext context) : base(context)
	{

	}
}