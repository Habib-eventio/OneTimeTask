//using ERP.Data.Entities.IT;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.ItTicket
//{
//    public class ItTicketsDTONew
//    {
//        public static Ticket Map(ItTicketsViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new Ticket
//            {
//                Id = viewModel.ItemNum,
//                InitialDescription = viewModel.InitialDesc,
//                InitialDate = viewModel.InitDate,
//                SubmittedBy = viewModel.SubBy,
//                ClosedDate = viewModel.CloseDate,
//                Status = viewModel.Status,
//                Urgency = viewModel.Urgency,
//                Type = viewModel.Type,
//                Image = viewModel.Image,
//                TicketDatabaseId = viewModel.TicketDatabaseId,
//                PendingReviewCount = viewModel.PendingReviewCount,
//                AttachedFile = viewModel.AttachedFile,
//                IsActive = viewModel.Active,
//                PendingReviewDate = viewModel.PendingReviewDate,
//                IsUserInterfaceRelated = viewModel.IsUi,
//                ChangedDate = viewModel.ChangedDate,
//                PriorityNumber = viewModel.PriorityNumber,
//                IsManagerHelpNeeded = viewModel.IanHelpNeeded,
//                ContactNumber = viewModel.ContactNumber,
//                ComputerReporting = viewModel.ComputerReporting,
//            };
//        }

//        public static ItTicketsViewModel Map(Ticket entity)
//        {
//            if (entity == null) { return null; }

//            return new ItTicketsViewModel
//            {
//                ItemNum = entity.Id,
//                InitialDesc = entity.InitialDescription,
//                InitDate = entity.InitialDate,
//                SubBy = entity.SubmittedBy,
//                CloseDate = entity.ClosedDate,
//                Status = entity.Status,
//                Urgency = entity.Urgency,
//                Type = entity.Type,
//                Image = entity.Image,
//                TicketDatabaseId = entity.TicketDatabaseId,
//                PendingReviewCount = entity.PendingReviewCount,
//                AttachedFile = entity.AttachedFile,
//                Active = entity.IsActive,
//                PendingReviewDate = entity.PendingReviewDate,
//                IsUi = entity.IsUserInterfaceRelated,
//                ChangedDate = entity.ChangedDate,
//                PriorityNumber = entity.PriorityNumber,
//                IanHelpNeeded = entity.IsManagerHelpNeeded,
//                ContactNumber = entity.ContactNumber,
//                ComputerReporting = entity.ComputerReporting,
//            };
//        }

//        public static IEnumerable<ItTicketsViewModel> Map(IEnumerable<Ticket> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
