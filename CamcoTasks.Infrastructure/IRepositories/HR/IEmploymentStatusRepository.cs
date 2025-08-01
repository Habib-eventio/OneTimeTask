// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IEmploymentStatusRepository : IRepository<EmploymentStatus>
{
	/// <summary>
	/// This Method is being used in HumanResource Project
	/// </summary>
	string GetStatusFromEmploymentStatusByEmploymentStatusId(short employmentStatusId);
}