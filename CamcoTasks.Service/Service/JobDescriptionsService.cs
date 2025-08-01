using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.JobDescriptions;

namespace CamcoTasks.Service.Service;

/// <summary>
/// Simplified job descriptions service used for development and testing.
/// </summary>
public class JobDescriptionsService : IJobDescriptionsService
{
    public Task<IEnumerable<JobDescriptionsViewModal>> GetListAsync(bool isDelete)
        => Task.FromResult<IEnumerable<JobDescriptionsViewModal>>(new List<JobDescriptionsViewModal>());

    public Task<IEnumerable<string>> GetNameListAsync(bool isDelete)
        => Task.FromResult<IEnumerable<string>>(new List<string>());

    public Task<JobDescriptionsViewModal> GetByNameAsyncs(string name)
        => Task.FromResult<JobDescriptionsViewModal>(null);

    public Task<JobDescriptionsViewModal> GetByIdAsync(long id)
        => Task.FromResult<JobDescriptionsViewModal>(null);

    public Task<List<JobDescriptionEmployeeEmail>> GetJobDescriptionWithEmployeeListAsync(List<EmployeeViewModel> employees,
        List<JobDescriptionsViewModal> jobs)
        => Task.FromResult(new List<JobDescriptionEmployeeEmail>());

    public Task<List<long>> GetJobDescriptionWithEmployeesAsync(long jobId)
        => Task.FromResult(new List<long>());

    public List<JobDescriptionEmployeeEmail> GetSelectedJobDescriptionWithEmployeeLIst(string[] jobIds,
        string[] emails, List<JobDescriptionEmployeeEmail> jobDescriptionEmployeeEmails)
    {
        return jobDescriptionEmployeeEmails ?? new List<JobDescriptionEmployeeEmail>();
    }
}
