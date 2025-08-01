//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.LoginDTO;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class LoginService : ILogingService
//    {
//        private readonly IUserRepository _loginRepository;

//        public LoginService(IUserRepository loginRepository)
//        {
//            _loginRepository = loginRepository;
//        }

//        public async Task<LoginViewModel> GetUserByEmail(string email)
//        {
//            return LoginUserDTONew.Map(await _loginRepository.FindAsync(e => e.Email == email));
//        }

//        public async Task<LoginViewModel> GetUserByUserNameAsync(string userName)
//        {
//            return LoginUserDTONew.Map(await _loginRepository.FindAsync(e => e.UserName == userName));
//        }

//        public async Task<IEnumerable<LoginViewModel>> GetListAsync()
//        {
//            return LoginUserDTONew.Map(await _loginRepository.GetListAsync());
//        }

//        public async Task<LoginViewModel> GetByIdAsync(long id)
//        {
//            return LoginUserDTONew.Map(await _loginRepository.GetAsync(id));
//        }
//    }
//}
