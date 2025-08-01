using CamcoTasks.ViewModels.AutomatedAutomationsDTO;
using ERP.Data.Entities.Automated;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IAutomatedAutomationsService
    {
        Task<AutomatedAutomationsViewModel> GetById(int id);
        Task<AutomatedAutomationsViewModel> GetAutomationAsync(string automationType);
        Task UpdateAsync(AutomatedAutomationsViewModel entity);
    }
}
