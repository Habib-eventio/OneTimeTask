// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
namespace CamcoTasks.Infrastructure.Repository.HR;

public class PublicEmployeeRepository : Repository<PublicEmployee>,
	IPublicEmployeeRepository
{
	public PublicEmployeeRepository(DatabaseContext context) : base(context)
	{

	}
}