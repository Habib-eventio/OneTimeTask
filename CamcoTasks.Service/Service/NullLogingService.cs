using System.Collections.Generic;
using System.Threading.Tasks;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.LoginDTO;

namespace CamcoTasks.Service.Service
{
    /// <summary>
    /// Fallback loging service used when identity is disabled.
    /// </summary>
    public class NullLogingService : ILogingService
    {
        public Task<LoginViewModel> GetUserByEmail(string email)
            => Task.FromResult<LoginViewModel>(null);

        public Task<IEnumerable<LoginViewModel>> GetListAsync()
            => Task.FromResult<IEnumerable<LoginViewModel>>(new List<LoginViewModel>());

        public Task<LoginViewModel> GetUserByUserNameAsync(string userName)
            => Task.FromResult<LoginViewModel>(null);

        public Task<LoginViewModel> GetByIdAsync(long id)
            => Task.FromResult<LoginViewModel>(null);
    }
}
