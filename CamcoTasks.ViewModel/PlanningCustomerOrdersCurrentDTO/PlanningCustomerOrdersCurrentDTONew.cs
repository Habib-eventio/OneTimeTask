//using ERP.Data.Entities.Planning;
//using System.Collections.Generic;

//namespace CamcoTasks.ViewModels.PlanningCustomerOrdersCurrentDTO
//{
//    public class HR_PublicEmployeesDTO
//    {
//        public static CustomerOrderCurrent Map(PlanningCustomerOrdersCurrentViewModel viewModel)
//        {
//            if (viewModel == null) { return null; }

//            return new CustomerOrderCurrent
//            {
//                Id = viewModel.Id,
//                Date = viewModel.Date,
//                CustomerOrderNumber = viewModel.CONumber,
//                Customer = viewModel.Customer,
//                DueDate = viewModel.DueDate,
//                EnteredBy = viewModel.EnteredBy,
//                InvoiceNumber = viewModel.InvoiceNumber,
//                LineNumber = viewModel.LineNumber,
//                Notes = viewModel.Notes,
//                OverrideShipmentDate = viewModel.OverrideShipDate,
//                OverrideShipmentMethod = viewModel.OverrideShipMethod,
//                PartNumber = viewModel.PartNumber,
//                PurchaseOrderNumber = viewModel.Po,
//                ShipmentCustomer = viewModel.ShipCust,
//                ShipmentDate = viewModel.ShipDate,
//                TotalPurchaseOrderNumberPriceInDollar = viewModel.TotalPo
//            };
//        }

//        public static PlanningCustomerOrdersCurrentViewModel Map(CustomerOrderCurrent dataEntity)
//        {
//            if (dataEntity == null) { return null; }

//            return new PlanningCustomerOrdersCurrentViewModel
//            {
//                Id = dataEntity.Id,
//                Date = dataEntity.Date,
//                CONumber = dataEntity.CustomerOrderNumber,
//                Customer = dataEntity.Customer,
//                DueDate = dataEntity.DueDate,
//                EnteredBy = dataEntity.EnteredBy,
//                InvoiceNumber = dataEntity.InvoiceNumber,
//                LineNumber = dataEntity.LineNumber,
//                Notes = dataEntity.Notes,
//                OverrideShipDate = dataEntity.OverrideShipmentDate,
//                OverrideShipMethod = dataEntity.OverrideShipmentMethod,
//                PartNumber = dataEntity.PartNumber,
//                Po = dataEntity.PurchaseOrderNumber,
//                ShipCust = dataEntity.ShipmentCustomer,
//                ShipDate = dataEntity.ShipmentDate,
//                TotalPo = dataEntity.TotalPurchaseOrderNumberPriceInDollar
//            };
//        }

//    }
//}
