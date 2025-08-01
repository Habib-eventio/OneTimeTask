// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class TestResultRepository : Repository<TestResult>,
	ITestResultRepository
{
	public TestResultRepository(DatabaseContext context) : base(context)
	{

	}
}