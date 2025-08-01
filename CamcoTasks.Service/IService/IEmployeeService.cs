using CamcoTasks.ViewModels.EmployeeDTO;
using ERP.Data.Entities.HR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IEmployeeService
    {
        Task<EmployeeViewModel> GetByIdAsync(long id);
        Task<EmployeeViewModel> GetByEmployeeIdAsync(long id);
        Task<EmployeeViewModel> GetByJobIdAsync(long jobId);
        IEnumerable<EmployeeViewModel> GetList();
        Task<IEnumerable<EmployeeViewModel>> GetListAsync();
        Task<IEnumerable<EmployeeViewModel>> GetListAsync(bool isActive);
        Task<IEnumerable<long>> GetListAsync(bool isActive, bool isDelete, List<string> customEmployeeId);
        Task<IEnumerable<EmployeeViewModel>> GetListWithoutUserAsync(bool isActive);
        IEnumerable<EmployeeViewModel> GetList(bool IsDeleted);
        Task<IEnumerable<EmployeeViewModel>> GetListAsync(bool isActive, bool IsDeleted);
        //Task UpdateAsync(EmployeeViewModel entity);
        Task<int> CountAsync();
        Task<int> CountAsync(bool isActive);
        Task<EmployeeViewModel> GetEmployee(string Name);
        Task<EmployeeViewModel> GetByCustomEmployeeIdAsync(string customEmployeeId);
        EmployeeViewModel GetEmployeeSync(string Name);

        Task<IEnumerable<EmployeeViewModel>> GetListActive();
        Task<IEnumerable<EmployeeViewModel>> GetListActive2();
    }
}
