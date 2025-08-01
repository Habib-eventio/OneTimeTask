// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.IRepository.HR;
using ERP.Data.Entities.HR;
using ERP.Repository.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class AboveAndBeyondRecognitionRepository : Repository<AboveAndBeyondRecognition>,
	IAboveAndBeyondRecognitionRepository
{
	public AboveAndBeyondRecognitionRepository(DatabaseContext context) : base(context)
	{

	}
}