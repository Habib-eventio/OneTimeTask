// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class ItemAllocationRepository : Repository<ItemAllocation>,
	IItemAllocationRepository
{
	public ItemAllocationRepository(DatabaseContext context) : base(context)
	{

	}
}