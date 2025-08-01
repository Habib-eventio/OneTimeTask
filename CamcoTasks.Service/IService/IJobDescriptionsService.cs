using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.JobDescriptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IJobDescriptionsService
    {
        //repository services
        Task<IEnumerable<JobDescriptionsViewModal>> GetListAsync(bool isDelete);
        Task<IEnumerable<string>> GetNameListAsync(bool isDelete);
        Task<JobDescriptionsViewModal> GetByNameAsyncs(string name);
        Task<JobDescriptionsViewModal> GetByIdAsync(long id);

        //services method
        Task<List<JobDescriptionEmployeeEmail>> GetJobDescriptionWithEmployeeListAsync(List<EmployeeViewModel> employees,
            List<JobDescriptionsViewModal> jobs);

        Task<List<long>> GetJobDescriptionWithEmployeesAsync(long jobId);
        List<JobDescriptionEmployeeEmail> GetSelectedJobDescriptionWithEmployeeLIst(string[] jobIds,
            string[] emails, List<JobDescriptionEmployeeEmail> jobDescriptionEmployeeEmails);
    }
}
