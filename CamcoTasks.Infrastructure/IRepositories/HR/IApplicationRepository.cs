// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using ERP.Data.Entities.HR;

namespace ERP.Repository.IRepository.HR;

public interface IApplicationRepository : IRepository<Application>
{
	/// <summary>
	/// This Method is being used in HumanResource Project
	/// </summary>
	Task<short> GetApplicationByApplicationNameAsync(string applicationName);
}