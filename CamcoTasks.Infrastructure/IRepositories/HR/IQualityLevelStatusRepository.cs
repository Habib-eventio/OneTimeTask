// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IQualityLevelStatusRepository : IRepository<QualityLevelStatus>
{
    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<bool> GetIsExistFromQualityLevelStatusByQualityLevelStatusIdAsync(long qualityLevelStatusId);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<string> GetNameFromQualityLevelStatusByQualityLevelStatusIdAsync(long qualityLevelStatusId);
}