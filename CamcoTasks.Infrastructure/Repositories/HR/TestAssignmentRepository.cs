// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class TestAssignmentRepository : Repository<TestAssignment>,
	ITestAssignmentRepository
{
	public TestAssignmentRepository(DatabaseContext context) : base(context)
	{

	}
}