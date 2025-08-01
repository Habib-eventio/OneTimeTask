using CamcoTasks.Infrastructure.CustomModels.IT;
using CamcoTasks.ViewModels.ItTicket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ITicketService
    {
        Task<List<OpenTicketsModel>> GetOpenTicketHistory();
        Task<List<OpenItCustom>> GetCustomOpenTicketHistory();
        Task<IEnumerable<ItTicketsViewModel>> GetOpenTicketHistoryByDate(DateTime ViewDate);
        Task<int> OpenTicketCount();
        Task<IEnumerable<ItTicketsViewModel>> GetOpenTicket();
        Task<int> GetCountByChangeAsync(DateTime fromDate, DateTime toDate,
            string status, string assingTo);
        Task<int> GetCountByCloseDateAsync(DateTime fromDate, DateTime toDate,
            string status, string assingTo);
    }
}
