using CamcoTasks.Infrastructure;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Minimal employee service implementation providing in-memory behaviour.
/// </summary>

namespace CamcoTasks.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CountAsync()
        {
            return await _unitOfWork.Employees.CountAsync();
        }

        public async Task Delete(EmployeeViewModel viewModel)
        {
            await _unitOfWork.Employees.RemoveAsync(EmployeeDTONew.Map(viewModel));
        }

        public IEnumerable<EmployeeViewModel> GetList()
        {
            return EmployeeDTONew.Map(_unitOfWork.Employees.GetList());
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetListActive()
        {
            var result = await _unitOfWork.Employees.FindAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            return EmployeeDTONew.Map(result);
        }


        public async Task<IEnumerable<EmployeeViewModel>> GetListActive2()
        {
            var result = await _unitOfWork.Employees.FindAllAsync(x => x.IsDeleted == false
                                                                       && x.IsActive == true
                                                                       && (x.IsAuditor.HasValue && x.IsAuditor == true));
            return EmployeeDTONew.Map(result);
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetListAsync()
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetListAsync());
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetListAsync(bool isActive)
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetListAsync(isActive));
        }

        public async Task<IEnumerable<long>> GetListAsync(bool isActive, bool isDelete, List<string> customEmployeeId)
        {
            return await _unitOfWork.Employees.GetListAsync(isActive, isDelete, customEmployeeId);
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetListWithoutUserAsync(bool isActive)
        {
            var result = await _unitOfWork.Employees.FindAllWithOrderByAscendingAsync(x => x.IsActive == isActive,
                x => x.LastName);
            return EmployeeDTONew.Map(result);

        }

        public IEnumerable<EmployeeViewModel> GetList(bool IsDeleted)
        {
            return EmployeeDTONew.Map(_unitOfWork.Employees.GetList(IsDeleted));

        }

        public async Task<IEnumerable<EmployeeViewModel>> GetListAsync(bool isActive, bool IsDeleted)
        {
            var list = await _unitOfWork.Employees.GetListAsync(isActive, IsDeleted);
            return EmployeeDTONew.Map(list);
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetListAsync(int skip, int limit)
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetListAsync(skip, limit));
        }

        public async Task<EmployeeViewModel> GetByEmployeeIdAsync(long id)
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetByEmployeeIdAsync(id));
        }

        public async Task<EmployeeViewModel> GetByJobIdAsync(long jobId)
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetByJobIdAsync(jobId));
        }

        public async Task<EmployeeViewModel> GetByIdAsync(long id)
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetByIdAsync(id));
        }

        public async Task<EmployeeViewModel> GetEmployee(string Name)
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetByName(Name));
        }

        public async Task<EmployeeViewModel> GetByCustomEmployeeIdAsync(string customEmployeeId)
        {
            return EmployeeDTONew.Map(await _unitOfWork.Employees.GetByCustomEmployeeIdAsync(customEmployeeId));
        }

        public EmployeeViewModel GetEmployeeSync(string Name)
        {
            return EmployeeDTONew.Map(_unitOfWork.Employees.GetByNameSync(Name));
        }

        //public async Task UpdateAsync(EmployeeViewModel viewModel)
        //{
        //    await _unitOfWork.Employees.UpdateAsync(EmployeeDTONew.Map(viewModel));
        //}

        public async Task<int> CountAsync(bool isActive)
        {
            return await _unitOfWork.Employees.CountAsync(x => x.IsActive == isActive);
            //return await _unitOfWork.Employees.CountAsync(isActive);
        }

    }
}

