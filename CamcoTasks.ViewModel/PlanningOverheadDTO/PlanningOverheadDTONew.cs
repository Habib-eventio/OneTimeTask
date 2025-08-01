/*using System.Collections.Generic;

namespace CamcoTasks.ViewModels.PlanningOverheadDTO
{
    public class PlanningOverheadDTONew
    {
        public static Overhead Map(PlanningOverheadViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new Overhead
            {
                IsDeleted = viewModel.IsDeleted,
                OverheadDate = viewModel.OverheadDate,
                OverheadId = viewModel.OverheadId,
                OverheadPerHour = viewModel.OverheadPerHour,
                TotalDirectHours = viewModel.TotalDirectHours
            };
        }

        public static PlanningOverheadViewModel Map(Overhead dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new PlanningOverheadViewModel
            {
                IsDeleted = dataEntity.IsDeleted,
                OverheadDate = dataEntity.OverheadDate,
                OverheadId = dataEntity.OverheadId,
                OverheadPerHour = dataEntity.OverheadPerHour,
                TotalDirectHours = dataEntity.TotalDirectHours
            };
        }

        public static IEnumerable<PlanningOverheadViewModel> Map(IEnumerable<Overhead> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
*/