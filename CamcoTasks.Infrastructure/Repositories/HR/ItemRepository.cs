// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class ItemRepository : Repository<Item>,
	IItemRepository
{
	public ItemRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<string> GetItemNameFromItemByItemIdAsync(long itemId)
	{
		return await (from item in DatabaseContext.Items
			where item.Id == itemId
			select item.Name).FirstOrDefaultAsync();
	}
}