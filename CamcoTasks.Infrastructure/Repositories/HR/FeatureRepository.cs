// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class FeatureRepository : Repository<Feature>
	
{
	public FeatureRepository(DatabaseContext context) : base(context)
	{

	}
}