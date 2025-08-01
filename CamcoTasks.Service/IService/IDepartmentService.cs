using CamcoTasks.ViewModels.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentViewModel>> GetListAsync();

        //Task<IEnumerable<DepartmentViewModel>> GetListAsync(bool isDelete);
        //IEnumerable<DepartmentViewModel> GetList();
        Task<IEnumerable<string>> GetListAsync(bool isDelete, List<long> departmentId);
        //Task UpdateAsync(DepartmentViewModel viewModel);
        Task InsertAsync(DepartmentViewModel viewModel);

        Task<IEnumerable<DepartmentViewModel>> GetFilteredDepartmentsAsync(bool isDelete, List<long> departmentIds);
    }
}
