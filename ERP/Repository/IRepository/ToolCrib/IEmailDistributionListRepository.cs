using System.Threading.Tasks;
using ERP.Data.Entities.ToolCrib;

namespace ERP.Repository.IRepository.ToolCrib;

public interface IEmailDistributionListRepository
{
    Task<EmailDistributionList?> GetAsync(int id);
}
