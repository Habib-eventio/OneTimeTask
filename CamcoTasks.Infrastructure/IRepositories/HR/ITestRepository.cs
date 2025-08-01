// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface ITestRepository : IRepository<Test>
{
	/// <summary>
	/// Caution: This Method is being used in HumanResource Project
	/// </summary>
	Task<bool> GetIsExistFromTestByTestIdAsync(long testId);
}