//using CamcoTasks.Data.ModelsViewModel;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.CostingProgrammersClosedITTicketsDTO;
//using ERP.Repository.IRepository.Costing;
//using CamcoTasks.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CamcoTasks.Service.Service
//{
//    public class CostingProgrammersClosedITTicketsServices : ICostingProgrammersClosedITTicketsServices
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        private readonly ITicketService _ticketService;

//        private List<string> _programmers = new List<string>()
//        {
//            "MIRZA, ALISHBA", "ALI, ADNAN", "NISA, MEHR UN", "AHTASHAM, KAMAL","SHUJA, SYED","ALL SAFAET, NOOR","MUNIR, SHAN","TORAB, WARDA"
//        };

//        public CostingProgrammersClosedITTicketsServices(
//            ITicketService ticketService, IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//            _ticketService = ticketService;
//        }

//        public async Task<int> InsertAsync(CostingProgrammersClosedITTicketsViewModel entity)
//        {
//            var result = CostingProgrammersClosedITTicketsDtoNew.Map(entity);
//            await _unitOfWork.ProgrammerClosedITTickets.AddAsync(result);
//            await _unitOfWork.CompleteAsync();
//            return result.Id;
//        }

//        public async Task<IEnumerable<CostingProgrammersClosedITTicketsViewModel>> GetListAsync()
//        {
//            return CostingProgrammersClosedITTicketsDtoNew.Map(
//                await _unitOfWork.ProgrammerClosedITTickets.GetListAsync());
//        }

//        public async Task<IEnumerable<CostingProgrammersClosedITTicketsViewModel>> GetListByProgrammerNameAsync(
//            string programmerName)
//        {
//            return CostingProgrammersClosedITTicketsDtoNew.Map(
//                await _unitOfWork.ProgrammerClosedITTickets.FindAllAsync(x => x.ProgrammerName == programmerName));
//        }

//        public async Task<int> GetCountAsync(DateTime dateTime)
//        {
//            return await _unitOfWork.ProgrammerClosedITTickets.CountAsync(x =>
//                x.WeekCloseDate == dateTime.ToString("MM/dd/yyyy"));
//        }

//        public async Task<List<SeriesData>> GetProgrammerItTicketsStatusSeriesData()
//        {
//            List<SeriesData> SeriesCollection = new List<SeriesData>();

//            for (int i = 0; i < _programmers.Count; i++)
//            {
//                var obj = new SeriesData
//                {
//                    XName = nameof(LineChartData.XValue),
//                    YName = nameof(LineChartData.YValue),
//                    EmployeeName = _programmers[i],
//                    Data = await GetProgrammerItTicketsStatusChartData(_programmers[i])
//                };

//                if (obj.Data.Sum(x => x.YValue) > 0)
//                {
//                    SeriesCollection.Add(obj);
//                }
//            }

//            return SeriesCollection;
//        }

//        private async Task<List<LineChartData>> GetProgrammerItTicketsStatusChartData(string programmerName)
//        {
//            List<LineChartData> data = new List<LineChartData>();
//            var temp = (await GetListByProgrammerNameAsync(programmerName)).ToList();

//            for (int i = 0; i < temp.Count; i++)
//            {
//                data.Add(new LineChartData
//                {
//                    XValue = temp[i].WeekCloseDate,
//                    YValue = Convert.ToDouble(temp[i].ClosedIttickets)
//                });
//            }
//            return data;
//        }

//        public async Task UpdateProgrammerItTicketsStatus(DateTime dateTime)
//        {
//            var temp = await GetCountAsync(dateTime);

//            if (temp == 0)
//            {
//                for (int i = 0; i < _programmers.Count; i++)
//                {
//                    var pending = await _ticketService.GetCountByChangeAsync(dateTime.AddDays(-6), dateTime.AddDays(1),
//                    "PENDING REVIEW", _programmers[i]);
//                    var closed = await _ticketService.GetCountByCloseDateAsync(dateTime.AddDays(-6),
//                    dateTime.AddDays(1), "CLOSED", _programmers[i]);

//                    CostingProgrammersClosedITTicketsViewModel obj = new CostingProgrammersClosedITTicketsViewModel
//                    {
//                        WeekCloseDate = dateTime.ToString("MM/dd/yyyy"),
//                        ProgrammerName = _programmers[i],
//                        ClosedIttickets = closed,
//                        OpenToPendingReviewIttickets = pending
//                    };
//                    await InsertAsync(obj);
//                }
//            }
//        }
//    }
//}
