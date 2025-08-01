//using ERP.Data.Entities.Production;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.ProductionTimeSheetDataDTO
//{
//    public class ProductionTimeSheetDataDTONew
//    {
//        public static TimeSheetDatum Map(ProductionTimeSheetDataViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new TimeSheetDatum
//            {
//                Id = viewModel.Id,
//                ActualCycleTime = viewModel.ActualCycleTime,
//                BadPieces = viewModel.BadPcs,
//                BurdenTime = viewModel.BurdenTime,
//                CycleTime = viewModel.CycleTime,
//                Date = viewModel.Date,
//                Employee = viewModel.Employee,
//                ExpenseCode = viewModel.ExpenseCode,
//                FinishedSetups = viewModel.FinishedSetups,
//                Note = viewModel.Note,
//                OperationComplete = viewModel.OperationComplete,
//                OperationNumber = viewModel.OpNumber,
//                PartNumber = viewModel.PartNumber,
//                PricePerPiecesInDollars = viewModel.PerPc,
//                SetupComplete = viewModel.SetupComplete,
//                SetupDollars = viewModel.SetupDollars,
//                SetUpTime = viewModel.SetUpTime,
//                ShopOrderNumber = viewModel.SoNumber,
//                TotalPriceInDollars = viewModel.Total,
//                TotalCalculated = viewModel.TotalCalculated,
//                TotalTime = viewModel.TotalTime,
//                HasIssue = viewModel.HasIssue,
//                DateEntered = viewModel.DateEntered,
//                ProjectId = viewModel.ProjectId,
//            };
//        }

//        public static ProductionTimeSheetDataViewModel Map(TimeSheetDatum dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new ProductionTimeSheetDataViewModel
//            {
//                Id = dataEntity.Id,
//                ActualCycleTime = dataEntity.ActualCycleTime,
//                BadPcs = dataEntity.BadPieces,
//                BurdenTime = dataEntity.BurdenTime,
//                CycleTime = dataEntity.CycleTime,
//                Date = dataEntity.Date,
//                Employee = dataEntity.Employee,
//                ExpenseCode = dataEntity.ExpenseCode,
//                FinishedSetups = dataEntity.FinishedSetups,
//                Note = dataEntity.Note,
//                OperationComplete = dataEntity.OperationComplete,
//                OpNumber = dataEntity.OperationNumber,
//                PartNumber = dataEntity.PartNumber,
//                PerPc = dataEntity.PricePerPiecesInDollars,
//                SetupComplete = dataEntity.SetupComplete,
//                SetupDollars = dataEntity.SetupDollars,
//                SetUpTime = dataEntity.SetUpTime,
//                SoNumber = dataEntity.ShopOrderNumber,
//                Total = dataEntity.TotalPriceInDollars,
//                TotalCalculated = dataEntity.TotalCalculated,
//                TotalTime = dataEntity.TotalTime,
//                HasIssue = dataEntity.HasIssue,
//                DateEntered = dataEntity.DateEntered,
//                ProjectId = dataEntity.ProjectId,
//            };
//        }

//        public static IEnumerable<ProductionTimeSheetDataViewModel> Map(IEnumerable<TimeSheetDatum> dataEntityList)
//        {
//            if (dataEntityList == null) { yield break; }
//            foreach (var item in dataEntityList)
//            {
//                yield return Map(item);
//            }
//        }
//    }
//}
