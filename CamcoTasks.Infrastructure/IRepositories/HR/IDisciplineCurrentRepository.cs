// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IDisciplineCurrentRepository : IRepository<DisciplineCurrent>
{
    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<List<DisciplineCurrent>> GetCurrentDisciplinesByCurrentDisciplineLevelIdAsync(
        short enumCurrentDisciplineLevelId);
}