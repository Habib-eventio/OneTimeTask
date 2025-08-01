// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;
using ERP.Data.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IItemRepository : IRepository<Item>
{
    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<string> GetItemNameFromItemByItemIdAsync(long itemId);
}