using CamcoTasks.ViewModels.TimeTrackingTransactionTimelogDTO;
using System;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ITimeTrackingTransactionTimelogService
    {
        Task<int> InsertAsync(TimeTrackingTransactionTimelogViewModel entity);
        Task UpdateAsync(TimeTrackingTransactionTimelogViewModel entity);
        Task<int> InsertAsync(int categoryId, string transactionBy, DateTime startDate,
            int transactionId, string value);
        Task<TimeTrackingTransactionTimelogViewModel> GetAsync(string transaction, int categoryId, DateTime todate,
             bool isCompleted, bool isCancel);
    }
}
