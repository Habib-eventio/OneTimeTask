using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface ICamcoProjectService
    {
        Task<IEnumerable<CamcoProjectsViewModel>> GetListAsync();
        Task<int> GetCountAsync();
        Task<CamcoProjectsViewModel> GetLastEntityAsync();
        Task<int> InsertAsync(CamcoProjectsViewModel entity);
        Task UpdateAsync(CamcoProjectsViewModel entity);
        Task<CamcoProjectsViewModel> GetByIdAsync(int id);
        Task<string> GetProjectCodeId();
        Task<List<CamcoProjectsViewModel>> GetProductListAsync();
        Task<List<ProjectCosting>> GetProjectCostsListAsync();
        Task<List<ProjectCosting>> GetProjectCostsListAsync(int projectId);
		Task<List<int>> GetCamcoProjectIdsAsync();
	}
}
