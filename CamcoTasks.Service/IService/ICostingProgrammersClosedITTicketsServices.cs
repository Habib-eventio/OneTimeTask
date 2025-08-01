using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.ViewModels.CostingProgrammersClosedITTicketsDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ICostingProgrammersClosedITTicketsServices
    {
        Task<int> InsertAsync(CostingProgrammersClosedITTicketsViewModel entity);
        Task<IEnumerable<CostingProgrammersClosedITTicketsViewModel>> GetListAsync();
        Task<IEnumerable<CostingProgrammersClosedITTicketsViewModel>> GetListByProgrammerNameAsync(
            string programmerName);
        Task<int> GetCountAsync(DateTime dateTime);
        Task<List<SeriesData>> GetProgrammerItTicketsStatusSeriesData();
        Task UpdateProgrammerItTicketsStatus(DateTime dateTime);
    }
}
