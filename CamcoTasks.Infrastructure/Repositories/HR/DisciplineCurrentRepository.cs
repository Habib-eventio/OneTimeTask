//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class DisciplineCurrentRepository : Repository<DisciplineCurrent>,
//	IDisciplineCurrentRepository
//{
//	public DisciplineCurrentRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<List<DisciplineCurrent>> GetCurrentDisciplinesByCurrentDisciplineLevelIdAsync(
//		short enumCurrentDisciplineLevelId)
//	{
//		return await (from disciplineCurrent in DatabaseContext.DisciplineCurrents
//			join employees in DatabaseContext.Employees on disciplineCurrent.EmployeeId equals employees.Id
//			where employees.IsActive && disciplineCurrent.DateOfDownGrade < DateTime.Today &&
//			      disciplineCurrent.EnumCurrentDisciplineLevelId != enumCurrentDisciplineLevelId
//			select disciplineCurrent).ToListAsync();
//	}
//}