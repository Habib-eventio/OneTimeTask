/*// This File Needs to be reviewed Still. Don't Remove this comment.

using ERP.Data.Entities.HR;
using ERP.Repository.UnitOfWork;

namespace ERP.Repository.IRepository.HR;

public interface INatureOfViolationRepository : IRepository<NatureOfViolation>
{
    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<string> GetNameFromNatureOfViolationByNatureOfViolationIdAsync(long natureOfViolationId);

	/// <summary>
	/// This Method is being used in HumanResource Project
	/// </summary>
	Task<int> GetIdFromNatureOfViolationByNameAndTypeIdAsync(string natureOfViolation, short enumTypeId);
}*/