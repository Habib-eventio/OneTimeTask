// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.CAMCO;
using CamcoTasks.Infrastructure.IRepository.CAMCO;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.CAMCO;

public class ProjectRepository : Repository<Project>,
	IProjectRepository
{
	public ProjectRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<Project> GetLastProjectEntityAsync()
	{
		return await DatabaseContext.Projects.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
	}

	public async Task<List<ProjectCosting>> GetProjectCostsListAsync()
	{
		var lastWeekMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6); // Last week's Monday
		var lastWeekSunday = lastWeekMonday.AddDays(7);

		var projectCostsList = await DatabaseContext.Projects
			.Select(p => new ProjectCosting
			{
				ProjectId = (int)p.Id,
                SoNumber = ((int)p.Id.ToString().Length > 4) ? "P" + ((int)p.Id).ToString() : "P" + ((int)p.Id).ToString("D4"),


                Employee = DatabaseContext.Employees.FirstOrDefault(e => e.Id == p.ChampionEmployeeId),
				LastActivityDate = DatabaseContext.TimeSheetsData
					.Where(td => td.ProjectId == p.Id && td.BurdenTime != null)
					.Max(td => td.DateEntered),
				ProjectTitle = p.Title,
				Description = p.Description,
				HoursSpent = DatabaseContext.TimeSheetsData
					.Where(td => td.ProjectId == p.Id && td.BurdenTime != null).Select(td => td.BurdenTime).Sum(),
				LastWeekHoursSpent = DatabaseContext.TimeSheetsData.Where(td =>
						td.ProjectId == p.Id && td.DateEntered >= lastWeekMonday &&
						td.DateEntered <= lastWeekSunday)
					.Sum(td => td.BurdenTime ?? 0),
				DateCreated = p.DateCreated
			})
			.ToListAsync();

		return projectCostsList;
	}
    public IQueryable<Project> GetAllAsQueryable()
    {
        return DatabaseContext.Projects.AsNoTracking();
    }
}