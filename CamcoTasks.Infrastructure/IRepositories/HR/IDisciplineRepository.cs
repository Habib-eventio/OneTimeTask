// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;


namespace CamcoTasks.Infrastructure.IRepositories.HR;

public interface IDisciplineRepository : IRepository<Discipline>
{
    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    SpDisciplinesModel GetDisciplinesByEmployeeActiveStatusAndDisciplineIdAndEmployeeId(long employeeId,
        long disciplineId, bool isInactiveEmployeesAdded);

	/// <summary>
	/// This Method is being used in HumanResource Project
	/// </summary>
	SpDisciplinesModel GetDisciplinesByEmployeeActiveStatusAndDisciplineId(long disciplineId,
        bool isInactiveEmployeesAdded);

    /// <summary>
    /// This Method is being used in HumanResource & AutomatedSystemService
    /// </summary>
    SpDisciplinesModel GetDisciplinesByEmployeeActiveStatus(bool isInactiveEmployeesAdded);
}