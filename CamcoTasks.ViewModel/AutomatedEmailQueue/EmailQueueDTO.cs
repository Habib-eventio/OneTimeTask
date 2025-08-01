//using CamcoTasks.ViewModels.EmailQueue;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.AutomatedEmailQueue
//{
//    public class EmailQueueDTO
//    {
//        public static ERP.Data.Entities.Automated.EmailQueue Map(EmailQueueViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new ERP.Data.Entities.Automated.EmailQueue
//            {
//                Id = viewModel.EmailId,
//                SendTo = viewModel.SendTo,
//                Body = viewModel.Body,
//                HasBeenSent = viewModel.HasBeenSent,
//                Subject = viewModel.Subject,
//                Attachment = viewModel.Attachment,
//                HasError = viewModel.HasError,
//                TimeEntered= viewModel.TimeEntered,
//                TimeSent = viewModel.TimeSent,
//                EmailTypeId = viewModel.EmailTypeId,
//            };
//        }

//        public static EmailQueueViewModel Map(ERP.Data.Entities.Automated.EmailQueue dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new EmailQueueViewModel
//            {
//                EmailId = dataEntity.Id,
//                SendTo = dataEntity.SendTo,
//                Body = dataEntity.Body,
//                HasBeenSent = dataEntity.HasBeenSent,
//                Subject = dataEntity.Subject,
//                Attachment = dataEntity.Attachment,
//                HasError = dataEntity.HasError,
//                TimeEntered = dataEntity.TimeEntered,
//                TimeSent = dataEntity.TimeSent,
//                EmailTypeId = dataEntity.EmailTypeId,
//            };
//        }

//        public static IEnumerable<EmailQueueViewModel> Map(IEnumerable<ERP.Data.Entities.Automated.EmailQueue> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
