// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.Page;
using CamcoTasks.Infrastructure.IRepository.Page;


namespace CamcoTasks.Infrastructure.Repository.Page;

public class LoadTimeRepository : Repository<LoadTime>,
	ILoadTimeRepository
{
	public LoadTimeRepository(DatabaseContext context) : base(context)
	{

	}
}