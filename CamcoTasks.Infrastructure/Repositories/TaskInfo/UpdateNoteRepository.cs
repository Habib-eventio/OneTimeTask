using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories;


namespace CamcoTasks.Infrastructure.Repository.TaskInfo;

public class UpdateNoteRepository : Repository<UpdateNote>,
	IUpdateNoteRepository
{
	public UpdateNoteRepository(DatabaseContext context) : base(context)
	{

	}
}