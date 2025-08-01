using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using CamcoTasks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamcoTasks.ViewModels.JobDescriptions;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Defaults;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Service.Service
{
    public class CamcoProjectService : ICamcoProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CamcoProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CamcoProjectsViewModel>> GetListAsync()
        {
            return CamcoProjectsDtoNew.Map(await _unitOfWork.Projects.GetListAsync());
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.Projects.CountAsync();
        }

        public async Task<CamcoProjectsViewModel> GetLastEntityAsync()
        {
            return CamcoProjectsDtoNew.Map(await _unitOfWork.Projects.GetLastProjectEntityAsync());
        }

        public async Task<int> InsertAsync(CamcoProjectsViewModel entity)
        {
            var result = CamcoProjectsDtoNew.Map(entity);
            await _unitOfWork.Projects.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            return (int)result.Id;
        }

        public async Task UpdateAsync(CamcoProjectsViewModel entity)
        {
            var updateEntity = await _unitOfWork.Projects.FindAsync(a => a.Id == entity.Id);

            updateEntity.Id = entity.Id;
            updateEntity.IsActive = entity.IsActive;
            updateEntity.Title = entity.Title;
            updateEntity.EnteredByEmployeeId = entity.EnteredByEmployeeId;
            updateEntity.ChampionEmployeeId = entity.ChampionEmployeeId;
            updateEntity.Description = entity.Description;
            updateEntity.DateCreated = entity.DateCreated;
            updateEntity.ProjectType = entity.ProjectType;
            updateEntity.Notes = entity.Notes;
            updateEntity.DateUpdated = entity.DateUpdated;
            updateEntity.Status = entity.Status;
            updateEntity.IsPostponed = entity.IsPostponed;
            updateEntity.PostponedReason = entity.PostponedReason;
            updateEntity.UpdatedById = entity.UpdatedById;
            await _unitOfWork.CompleteAsync();
        }

        public async Task<CamcoProjectsViewModel> GetByIdAsync(int id)
        {
            return CamcoProjectsDtoNew.Map(await _unitOfWork.Projects.GetAsync(id));
        }

        public async Task<string> GetProjectCodeId()
        {
            var ProjectList = await GetCountAsync();
            string proCodeId = string.Empty;

            if (ProjectList == 0)
            {
                proCodeId = "P" + (1).ToString("0000");
            }
            else
            {
                CamcoProjectsViewModel pid = await GetLastEntityAsync();

                if (pid != null)
                {
                    var x = Math.Floor(Math.Log10(pid.Id + 1) + 1);

                    if (x > 4)
                    {
                        proCodeId = "P" + (pid.Id + 1).ToString();
                    }
                    else
                    {
                        proCodeId = "P" + (pid.Id + 1).ToString("0000");
                    }
                }
            }

            return proCodeId;
        }

        public async Task<List<CamcoProjectsViewModel>> GetProductListAsync()
        {
            List<CamcoProjectsViewModel> projects = new List<CamcoProjectsViewModel>();
            var temp = (await GetListAsync()).ToList();
            var employees = await _unitOfWork.Employees.GetListAsync();
            var departments = await _unitOfWork.DepartmentsAndManagers.GetListAsync();

            for (int i = 0; i < temp.Count; i++)
            {
                CamcoProjectsViewModel obj = new()
                {
                    ProjectCode = "P" + temp[i].Id.ToString("0000"),
                    Title = temp[i].Title,
                    Description = temp[i].Description,
                    DateCreated = temp[i].DateCreated,
                    Id = temp[i].Id,
                    Status = temp[i].Status,
                    IsActive = temp[i].IsActive,
                    IsPostponed = temp[i].IsPostponed
                };

                var emp = employees.Where(x => x.Id == temp[i].ChampionEmployeeId).FirstOrDefault();
                var enterdby = employees.Where(x => x.Id == temp[i].EnteredByEmployeeId).FirstOrDefault();

                if (emp != null)
                {
                    var dept = departments.Where(x => x.Id == emp.DepartmentId).FirstOrDefault();

                    if (dept != null)
                    {
                        obj.Department = dept.DepartmentName;
                    }

                    obj.Champion = emp.FullName;

                }

                if (enterdby != null)
                {
                    obj.EnteredBy = enterdby.FullName;
                }

                if (temp[i].ProjectType.HasValue)
                {
                    if (temp[i].ProjectType == 1)
                    {
                        obj.ProjectTypeName = "ONE TIME PROJECT";
                    }
                    else if (temp[i].ProjectType == 2)
                    {
                        obj.ProjectTypeName = "RECURRING PROJECT";
                    }
                }

                projects.Add(obj);
            }

            return projects;
        }

        public async Task<List<ProjectCosting>> GetProjectCostsListAsync()
        {
            List<ProjectCosting> projectCostings = await _unitOfWork.Projects.GetProjectCostsListAsync();

            projectCostings = projectCostings.Select(projectCost =>
                new ProjectCosting
                {
                    ProjectId = projectCost.ProjectId,
                    SoNumber = projectCost.SoNumber,
                    ProjectTitle = projectCost.ProjectTitle,
                    Description = projectCost.Description,
                    Champion = (projectCost.Employee != null) ? $"{projectCost.Employee.LastName}, {projectCost.Employee.FirstName}" : null,
                    HourlyRate = (projectCost.Employee != null) ? double.Parse(CryptoEngine.Decrypt(projectCost.Employee.BaseHourlyRate)) : 0.0,
                    HoursSpent = projectCost.HoursSpent ?? 0.0,
                    LastActivityDate = projectCost.LastActivityDate?.Date,
                    Hours = Convert.ToDouble(projectCost.HoursSpent ?? 0.0).ToString("0.0"),
                    TotalCost = (projectCost.HoursSpent ?? 0.0) * ((projectCost.Employee != null) ? double.Parse(CryptoEngine.Decrypt(projectCost.Employee.BaseHourlyRate) ?? "0.0") : 0.0),
                    DateCreated = projectCost.DateCreated?.Date,
                    LastWeekHoursSpent = Convert.ToDouble(projectCost.LastWeekHoursSpent.ToString("0.0")),
                    LastWeekCostSpent = projectCost.LastWeekHoursSpent * 30,
                })
                .OrderByDescending(projectCost => projectCost.LastActivityDate)
                .ThenByDescending(projectCost => projectCost.TotalCost)
                .ToList();

            return projectCostings;
        }

        public async Task<List<ProjectCosting>> GetProjectCostsListAsync(int projectId)
        {
            List<ProjectCosting> costList = new();
            var tempProject = await GetByIdAsync(projectId);
            var tempProTimsheetdata = await _unitOfWork.TimeSheetsData.FindAllAsync(p => p.ProjectId == projectId);

            for (int i = 0; i < tempProTimsheetdata.Count; i++)
            {
                ProjectCosting obj = new ProjectCosting();

                if (tempProTimsheetdata == null)
                    continue;

                var x = Math.Floor(Math.Log10((int)tempProTimsheetdata[i].ProjectId) + 1);
                obj.HoursSpent = tempProTimsheetdata[i].BurdenTime == null ? 0 : tempProTimsheetdata[i].BurdenTime;
                obj.ProjectId = (int)tempProTimsheetdata[i].ProjectId;
                obj.SoNumber = x > 4 ? "P" + (obj.ProjectId).ToString() : "P" + (obj.ProjectId).ToString("0000");
                obj.Champion = tempProTimsheetdata[i].Employee;
                obj.LastActivityDate = tempProTimsheetdata[i].DateEntered;

                if (tempProject != null)
                    obj.Description = tempProject.Description;


                var tempEmp = await _unitOfWork.Employees.GetByName(tempProTimsheetdata[i].Employee);

                if (tempEmp != null)
                {
                    var job = await _unitOfWork.JobDescriptions.GetNameFromJobDescriptionByJobDescriptionIdAsync(tempEmp.JobId);
                    if (job != null)
                    {
                        obj.ProjectTitle = job;
                    }
                    obj.HourlyRate = Convert.ToDouble(CryptoEngine.Decrypt(tempEmp.BaseHourlyRate));
                    obj.TotalCost = (double)obj.HoursSpent * obj.HourlyRate;
                }
                costList.Add(obj);
            }

            return costList;
        }
        public async Task<List<int>> GetCamcoProjectIdsAsync()
        {
            // var projects = await _unitOfWork.Projects.GetListAsync();
            //return projects.Select(p => (int)p.Id).OrderBy(id => id).ToList();
            return await _unitOfWork.Projects
        .GetAllAsQueryable()
        .Where(p => p.Id.HasValue) 
        .Select(p => p.Id.Value)   
        .ToListAsync();

        }
    }
}
