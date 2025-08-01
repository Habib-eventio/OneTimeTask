//using ERP.Data.Entities.Costing;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.CostingProgrammersClosedITTicketsDTO
//{
//    public class CostingProgrammersClosedITTicketsDtoNew
//    {
//        public static ProgrammerClosedITTicket Map(
//            CostingProgrammersClosedITTicketsViewModel viewModel)
//        {
//            if (viewModel == null) return null;

//            return new ProgrammerClosedITTicket
//            {
//                Id = viewModel.Id,
//                ProgrammerName = viewModel.ProgrammerName,
//                WeekCloseDate = viewModel.WeekCloseDate,
//                ClosedITTicketsCount = viewModel.ClosedIttickets,
//                OpenToPendingReviewITTicketsCount = viewModel.OpenToPendingReviewIttickets,
//            };
//        }

//        public static CostingProgrammersClosedITTicketsViewModel Map(ProgrammerClosedITTicket entity)
//        {
//            if (entity == null) return null;

//            return new CostingProgrammersClosedITTicketsViewModel
//            {
//                Id = entity.Id,
//                ProgrammerName = entity.ProgrammerName,
//                WeekCloseDate = entity.WeekCloseDate,
//                ClosedIttickets = entity.ClosedITTicketsCount,
//                OpenToPendingReviewIttickets = entity.OpenToPendingReviewITTicketsCount,
//            };
//        }

//        public static IEnumerable<CostingProgrammersClosedITTicketsViewModel> Map(
//            IEnumerable<ProgrammerClosedITTicket> dataEntityList)
//        {
//            if (dataEntityList == null) yield break;

//            foreach (ProgrammerClosedITTicket item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
