using CamcoTasks.ViewModels.LoginDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ILogingService
    {
        Task<LoginViewModel> GetUserByEmail(string email);
        Task<IEnumerable<LoginViewModel>> GetListAsync();
        Task<LoginViewModel> GetUserByUserNameAsync(string userName);
        Task<LoginViewModel> GetByIdAsync(long id);
    }
}
