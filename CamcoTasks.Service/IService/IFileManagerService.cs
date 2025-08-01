using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.Task;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using CamcoTasks.ViewModels.LoggingChangeLogDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
    public interface IFileManagerService
    {
        string RecurringTaskHowtoFileExtention {  get; }
        bool IsImage(string file);
        bool IsFile(string file);
        bool IsValidSize(double length);
        string CreateRecurringTaskDirectory(string TaskIdTarget);
        string GetBaseTaskDirectory();
        string CreateOneTimeTaskDirectory(string TaskIdTarget);
        bool WriteToFile(MemoryStream source, string target);
        string GetFileName(string link);
        bool RunningInDebugMode();
        byte[] CreatePdfForUpdateReport(List<UpdateReportViewModel> updateList, string title, DateTime repoartDate);
        byte[] CreatePdfInMemory(List<TasksRecTasksViewModel> TasksList);
        byte[] CreatePdfInMemory(List<TasksRecTasksViewModel> TasksList, string activeFilter);
        byte[] CreateDeactivePdfInMemory(List<TasksRecTasksViewModel> deactiveTasksList);
        byte[] CreateCamcoProjectPdfInMemory(List<CamcoProjectsViewModel> deactiveTasksList);
        byte[] CreateEditHistoryPdfInMemory(List<LoggingChangeLogViewModel> projectList);
        byte[] CreateCamcoProjectPdfInMemory(List<ProjectCosting> projeectCostList);
        byte[] CreatePdfRecurringTaskReport(List<TasksRecTasksViewModel> TasksList, string title, DateTime reportDate);
        string ViewRecurringFiles(int fileId, string filePath);
        Task StartDownloadFile(TasksImagesViewModel file);
        Task StartDownloadFile(string filePath);
        string FilePathForEmail(TasksImagesViewModel file);
        string ConvertImagetoBase64(string path);
        string ConvertImagetoBase64(TasksImagesViewModel file);
    }
}
