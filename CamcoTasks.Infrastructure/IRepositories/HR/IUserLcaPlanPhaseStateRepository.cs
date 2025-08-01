// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IUserLcaPlanPhaseStateRepository : IRepository<UserLcaPlanPhaseState>
{
    /// <summary>
    /// Caution: This Method is being used in HumanResource Project
    /// </summary>
    Task<UserLcaPlanPhaseState> GetActivePlanDetailsByEmployeeIdAsync(long employeeId);

    /// <summary>
    /// Caution: This Method is being used in HumanResource Project
    /// </summary>
    Task<bool> CheckIfActiveLcaPlanExistByEmployeeIdAsync(long userId);
}