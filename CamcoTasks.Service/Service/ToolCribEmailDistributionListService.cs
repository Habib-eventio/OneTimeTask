//using ERP.Repository.IRepository.ToolCrib;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.ToolCribEmailDistributionListDTO;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class ToolCribEmailDistributionListService : IToolCribEmailDistributionListService
//    {
//        private readonly IEmailDistributionListRepository _toolCribEmailDistributionListRepository;

//        public ToolCribEmailDistributionListService(
//            IEmailDistributionListRepository toolCribEmailDistributionListRepository)
//        {
//            _toolCribEmailDistributionListRepository = toolCribEmailDistributionListRepository;
//        }

//        public async Task<ToolCribEmailDistributionListViewModel> GetByIdAsync(int id)
//        {
//            return ToolCribEmailDistributionListDtoNew.Map(await _toolCribEmailDistributionListRepository.GetAsync(id));
//        }
//    }
//}
