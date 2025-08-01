//using ERP.Data.CustomModels.IT;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.ItTicket;
//using ERP.Repository.IRepository.IT;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using CamcoTasks.Infrastructure;

//namespace CamcoTasks.Service.Service
//{
//    public class TicketService : ITicketService
//    {
//        private readonly ITicketRepository _ticketRepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public TicketService(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
//        {
//            _ticketRepository = ticketRepository;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<List<OpenItCustom>> GetCustomOpenTicketHistory()
//        {
//            return await _ticketRepository.GetCustomOpenTicketHistory();
//        }

//        public async Task<List<OpenTicketsModel>> GetOpenTicketHistory()
//        {
//            return await _ticketRepository.GetOpenTicketHistory();
//        }

//        public async Task<IEnumerable<ItTicketsViewModel>> GetOpenTicketHistoryByDate(DateTime ViewDate)
//        {
//            return ItTicketsDTONew.Map(await _ticketRepository.GetOpenTicketHistoryByDate(ViewDate));
//        }

//        public async Task<int> OpenTicketCount()
//        {
//            return await _ticketRepository.OpenTicketCount();
//        }
//        public async Task<IEnumerable<ItTicketsViewModel>> GetOpenTicket()
//        {
//            return ItTicketsDTONew.Map(await _ticketRepository.GetOpenTicket());
//        }

//        public async Task<int> GetCountByChangeAsync(DateTime fromDate, DateTime toDate,
//            string status, string assignTo)
//        {
//            return await _unitOfWork.Tickets.CountAsync(x => x.ChangedDate.HasValue
//                                                             && x.ChangedDate.Value.Date >= fromDate
//                                                             && x.ChangedDate.Value.Date < toDate
//                                                             && x.Status == status
//                                                             && x.AssignedTo == assignTo);
//        }

//        public async Task<int> GetCountByCloseDateAsync(DateTime fromDate, DateTime toDate,
//            string status, string assignTo)
//        {
//            return await _unitOfWork.Tickets.CountAsync(x => x.ClosedDate.HasValue
//                                                             && x.ClosedDate.Value.Date >= fromDate
//                                                             && x.ClosedDate.Value.Date < toDate
//                                                             && x.Status == status
//                                                             && x.AssignedTo == assignTo);
//        }
//    }
//}
