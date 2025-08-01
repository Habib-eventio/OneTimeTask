using CamcoTasks.Infrastructure;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.Department;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<DepartmentViewModel>> GetListAsync()
    {
        return DepartmentDTO.Map(await _unitOfWork.DepartmentsAndManagers.GetListAsync());
    }

    //public async Task<IEnumerable<DepartmentViewModel>> GetListAsync(bool isDelete)
    //{
    //    var departments = await _unitOfWork.DepartmentsAndManagers.GetFilteredDepartmentsAsync(bool isDelete, List<long> departmentIds);
    //    return DepartmentDTO.Map(departments); // Mapping to view model
    //}
    public async Task<IEnumerable<string>> GetListAsync(bool isDelete, List<long> departmentIds)
    {
        // Fetch department names using the repository's filtered method
        var departmentNames = await _unitOfWork.DepartmentsAndManagers.GetFilteredDepartmentsAsync(isDelete, departmentIds);

        // Return the department names
        return departmentNames;
    }

    public IEnumerable<DepartmentViewModel> GetList()
    {
        return DepartmentDTO.Map(_unitOfWork.DepartmentsAndManagers.GetList());
    }

    //public async Task<IEnumerable<string>> GetListAsync(bool isDelete, List<long> departmentId)
    //{
    //    return await _unitOfWork.DepartmentsAndManagers.GetListAsync(isDelete, departmentId);
    //}

    public async Task UpdateAsync(DepartmentViewModel viewModel)
    {
        await _unitOfWork.DepartmentsAndManagers.UpdateAsync(DepartmentDTO.Map(viewModel));
    }



    public async Task InsertAsync(DepartmentViewModel viewModel)
    {
        var result = DepartmentDTO.Map(viewModel);
        await _unitOfWork.DepartmentsAndManagers.AddAsync(result);
        await _unitOfWork.CompleteAsync();
    }
    public async Task<IEnumerable<DepartmentViewModel>> GetFilteredDepartmentsAsync(bool isDelete, List<long> departmentIds)
    {
        // Fetch filtered departments from the repository using UnitOfWork
        var departmentNames = await _unitOfWork.DepartmentsAndManagers.GetFilteredDepartmentsAsync(isDelete, departmentIds);

        // Business Logic:
        // - You might want to add further checks or manipulations on the results.
        // - For example, handling empty department lists or applying additional business rules.
        if (departmentNames == null || !departmentNames.Any())
        {
            // You can log or throw an exception here if no departments are found based on the filter
            // or just return an empty list.
            return Enumerable.Empty<DepartmentViewModel>();
        }

        // Map the department names to DepartmentViewModel, assuming we only need department names
        var departmentViewModels = departmentNames.Select(departmentName => new DepartmentViewModel
        {
            Name = departmentName
            // Map other properties as needed, e.g., DepartmentId, etc.
        }).ToList();

        // Return the mapped department view models
        return departmentViewModels;
    }
}
