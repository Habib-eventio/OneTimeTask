// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class TestQuestionRepository : Repository<TestQuestion>,
	ITestQuestionRepository
{
	public TestQuestionRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<bool> GetTestQuestionByTestQuestionIdAsync(long testQuestionId)
	{
		return await (from testQuestion in DatabaseContext.TestQuestions
			where testQuestion.Id == testQuestionId
			select testQuestion).AnyAsync();
	}
}