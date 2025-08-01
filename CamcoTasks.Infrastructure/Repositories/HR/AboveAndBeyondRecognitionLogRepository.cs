// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.IRepositories.HR;

namespace ERP.Repository.Repository.HR;

public class AboveAndBeyondRecognitionLogRepository : Repository<AboveAndBeyondRecognitionLog>,
	IAboveAndBeyondRecognitionLogRepository
{
	public AboveAndBeyondRecognitionLogRepository(DatabaseContext context) : base(context)
	{

	}
}