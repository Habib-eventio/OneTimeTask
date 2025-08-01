//using ERP.Data.Entities.Metric;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.MetricsFieldsDTO
//{
//    public class MetricsFieldsDTONew
//    {
//        public static Field Map(MetricsFieldsViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new Field
//            {
//                Id = viewModel.Id,
//                DataName = viewModel.DataName,
//                DataValue = viewModel.DataValue,
//                GoalId = viewModel.GoalId,
//                IsGoalRequired= viewModel.IsGoalRequired,
//                IsMetrics = viewModel.IsMetrics
//            };
//        }

//        public static MetricsFieldsViewModel Map(Field dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new MetricsFieldsViewModel
//            {
//                Id = dataEntity.Id,
//                DataName = dataEntity.DataName,
//                DataValue = dataEntity.DataValue,
//                GoalId = dataEntity.GoalId.Value,
//                IsGoalRequired = dataEntity.IsGoalRequired.Value,
//                IsMetrics = dataEntity.IsMetrics.Value
//            };
//        }

//        public static IEnumerable<MetricsFieldsViewModel> Map(IEnumerable<Field> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
