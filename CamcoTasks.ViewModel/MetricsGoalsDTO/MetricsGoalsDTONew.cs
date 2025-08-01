//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.MetricsGoalsDTO
//{
//    public class MetricsGoalsDTONew
//    {
//        public static Goal Map(MetricsGoalsViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new Goal
//            {
//                Id = viewModel.Id,
//                GoalDescription = viewModel.GoalDescription,
//                GoalItem = viewModel.GoalItem,
//                GoalAlert = viewModel.GoalAlert,
//                GoalCaution=viewModel.GoalCaution,
//                IsBelowAlert=viewModel.IsBelowAlert,
//                IsBelowCaution=viewModel.IsBelowCaution,
//                IsBelowGoal = viewModel.IsBelowGoal
//            };
//        }

//        public static MetricsGoalsViewModel Map(Goal dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new MetricsGoalsViewModel
//            {
//                Id = dataEntity.Id,
//                GoalDescription = dataEntity.GoalDescription,
//                GoalItem = dataEntity.GoalItem,
//                GoalAlert = dataEntity.GoalAlert,
//                GoalCaution = dataEntity.GoalCaution,
//                IsBelowAlert = dataEntity.IsBelowAlert,
//                IsBelowCaution = dataEntity.IsBelowCaution,
//                IsBelowGoal = dataEntity.IsBelowGoal
//            };
//        }

//        public static IEnumerable<MetricsGoalsViewModel> Map(IEnumerable<Goal> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
