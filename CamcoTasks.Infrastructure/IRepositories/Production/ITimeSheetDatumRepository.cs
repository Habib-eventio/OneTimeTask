// This File Needs to be reviewed Still. Don't Remove this comment.


using CamcoTasks.Infrastructure.Entities.Production;

namespace CamcoTasks.Infrastructure.IRepository.Production;

public interface ITimeSheetDatumRepository : IRepository<TimeSheetDatum>
{
    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<double> GetSumOfBurdenTimeAsync(int projectId);
}