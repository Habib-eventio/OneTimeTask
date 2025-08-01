// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface ITestQuestionRepository : IRepository<TestQuestion>
{
    /// <summary>
    /// Caution: This Method is being used in HumanResource Project
    /// </summary>
    Task<bool> GetTestQuestionByTestQuestionIdAsync(long testQuestionId);
}