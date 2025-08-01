// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using ERP.Data.Entities.HR;
using ERP.Repository.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class QualityLevelStatusRepository : Repository<QualityLevelStatus>,
	IQualityLevelStatusRepository
{
	public QualityLevelStatusRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<bool> GetIsExistFromQualityLevelStatusByQualityLevelStatusIdAsync(long qualityLevelStatusId)
	{
		return await (from qualityLevelStatus in DatabaseContext.QualityLevelStatuses
			where qualityLevelStatus.Id == qualityLevelStatusId
			select qualityLevelStatus).AnyAsync();
	}

	public async Task<string> GetNameFromQualityLevelStatusByQualityLevelStatusIdAsync(long qualityLevelStatusId)
	{
		return await (from qualityLevelStatus in DatabaseContext.QualityLevelStatuses
			where qualityLevelStatus.Id == qualityLevelStatusId
			select qualityLevelStatus.QualityLevelStatusName).FirstOrDefaultAsync();
	}
}