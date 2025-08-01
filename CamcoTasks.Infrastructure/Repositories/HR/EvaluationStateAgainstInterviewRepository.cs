// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class EvaluationStateAgainstInterviewRepository : Repository<EvaluationStateAgainstInterview>,
	IEvaluationStateAgainstInterviewRepository
{
	public EvaluationStateAgainstInterviewRepository(DatabaseContext context) : base(context)
	{

	}
}