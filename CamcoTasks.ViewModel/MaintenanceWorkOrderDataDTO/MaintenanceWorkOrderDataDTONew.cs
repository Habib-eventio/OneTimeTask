using CamcoTasks.Infrastructure.Entities.Maintenance;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.MaintenanceWorkOrderDataDTO
{
    public class MaintenanceWorkOrderDataDTONew
    {
        public static WorkOrderData Map(MaintenanceWorkOrderDataViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new WorkOrderData
            {
                AssignedTo = viewModel.AssignedTo,
                CompletedBy = viewModel.CompletedBy,
                DateTimeComplete = viewModel.DateTimeComplete,
                DateTimeOpen = viewModel.DateTimeOpen,
                EquipmentId = viewModel.EquipmentId,
                EstimatedLabor = viewModel.EstimatedLabor,
                IsActive = viewModel.IsActive,
                JobType = viewModel.JobType,
                Location = viewModel.Location,
                ManualEntryDowntime = viewModel.ManualEntryDowntime,
                Priority= viewModel.Priority,
                ProblemProjectDescriptionAndOrSymptoms= viewModel.ProblemProjectDescriptionAndOrSymptoms,
                RemarksActionsTaken= viewModel.RemarksActionsTaken,
                Requestor= viewModel.Requestor,
                WorkOrderStatus= viewModel.WorkOrderStatus
            };
        }

        public static MaintenanceWorkOrderDataViewModel Map(WorkOrderData dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new MaintenanceWorkOrderDataViewModel
            {
                AssignedTo = dataEntity.AssignedTo,
                CompletedBy = dataEntity.CompletedBy,
                DateTimeComplete = dataEntity.DateTimeComplete,
                DateTimeOpen = dataEntity.DateTimeOpen,
                EquipmentId = dataEntity.EquipmentId,
                EstimatedLabor = dataEntity.EstimatedLabor,
                IsActive = dataEntity.IsActive,
                JobType = dataEntity.JobType,
                Location = dataEntity.Location,
                ManualEntryDowntime = dataEntity.ManualEntryDowntime,
                Priority = dataEntity.Priority,
                ProblemProjectDescriptionAndOrSymptoms = dataEntity.ProblemProjectDescriptionAndOrSymptoms,
                RemarksActionsTaken = dataEntity.RemarksActionsTaken,
                Requestor = dataEntity.Requestor,
                WorkOrderStatus = dataEntity.WorkOrderStatus
            };
        }

        public static IEnumerable<MaintenanceWorkOrderDataViewModel> Map(IEnumerable<WorkOrderData> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
