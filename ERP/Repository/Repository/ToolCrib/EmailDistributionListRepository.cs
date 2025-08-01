using ERP.Data.Entities.ToolCrib;
using ERP.Repository.IRepository.ToolCrib;
using CamcoTasks.Infrastructure;

namespace ERP.Repository.Repository.ToolCrib;

public class EmailDistributionListRepository : Repository<EmailDistributionList>, IEmailDistributionListRepository
{
    public EmailDistributionListRepository(DatabaseContext context) : base(context) {}

    public Task<EmailDistributionList?> GetAsync(int id) => base.GetAsync(id);
}
