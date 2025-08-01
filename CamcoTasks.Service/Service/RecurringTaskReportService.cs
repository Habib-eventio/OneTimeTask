using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Policy;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.Infrastructure.Defaults;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.Task;

namespace CamcoTasks.Service.Service
{
    public class RecurringTaskReportService : IRecurringTaskReportService
    {
        private ITasksService _tasksService;
        private IEmployeeService _employeeService;
        private IEmailService _emailService;
        private FileManagerService _fileManagerService;
        private ILogger<RecurringTaskReportService> _logger;
        private IJobDescriptionsService _jobDescriptionsService;

        public RecurringTaskReportService(ITasksService tasksService, IEmployeeService employeeService,
            IEmailService emailService, FileManagerService fileManagerService, 
            ILogger<RecurringTaskReportService> logger, IJobDescriptionsService jobDescriptionsService)
        {
            _tasksService = tasksService;
            _employeeService = employeeService;
            _emailService = emailService;
            _fileManagerService = fileManagerService;
            _logger = logger;
            _jobDescriptionsService = jobDescriptionsService;
        }

        public async Task IncressUpcommicDate(string frequency, int incressDay)
        {
            var pastDueTaks = await _tasksService.GetRecurringTasks(
                x => x.IsDeactivated == false
                && x.IsDeleted == false
                && x.TasksFrequency.Frequency == frequency
                && x.UpcomingDate.HasValue
                && x.UpcomingDate < DateTime.Today);

            foreach (var task in pastDueTaks)
            {
                task.UpcomingDate = task.UpcomingDate?.AddDays(incressDay);
                task.UpcomingDate = _tasksService
                    .RecurringUpcommingDate(task.UpcomingDate.HasValue
                    ? task.UpcomingDate.Value : DateTime.Now.AddDays(incressDay));
                await _tasksService.UpdateRecurringTask(task);
            }
        }

        public async Task CheckPastDueTasksAsync(string appUrl)
        {
            var PastDueTasks = await _tasksService.GetRecurringTasks(
                x => x.IsDeactivated == false
                && x.IsDeleted == false
                && x.UpcomingDate.HasValue
                && x.UpcomingDate < DateTime.Today);

            foreach (var DueTask in PastDueTasks)
            {
                if (!string.IsNullOrEmpty(DueTask.PersonResponsible))
                {
                    StringBuilder customBody = new();
                    customBody.Append($"<p><b>INITIATOR: </b>{DueTask.Initiator.ToUpper()}</p>");
                    customBody.Append($"<p><b>DESCRIPTION: </b>{DueTask.Description.ToUpper()}</p>");
                    customBody.Append($"<p><b>UPCOMING DATE: </b>{DueTask.UpcomingDate?.Date.ToString("MM/dd/yyyy")}</p>");
                    customBody.Append($"<p><b>FREQUENCY: </b>{DueTask.Frequency}</p>");
                    customBody.Append(
                    $@"<p>
                        <b>CLICK TO MARK COMPLETED: </b>
                        <a href=""{appUrl}viewrecurringtasks/completeTask/{DueTask.Id}/1""
                            style=""display:inline-block; background-color:#008CBA; color:#FFFFFF; padding:10px 20px;
                            text-align:center; text-decoration:none; font-size:20px; border-radius:5px;"">
                            YES
                        </a> 
                        <a href=""{appUrl}viewrecurringtasks/completeTask/{DueTask.Id}/0""
                            style=""display:inline-block; background-color:#008CBA; color:#FFFFFF; padding:10px 20px;
                            text-align:center; text-decoration:none; font-size:20px; border-radius:5px;"">
                            NO
                        </a> 
                    </p>");


                    string Subject = "MISSED RECURRING TASK";

                    string[] splitPersonRes = DueTask.PersonResponsible.Split(";");
                    string emailSendTo = string.Empty;

                    if (splitPersonRes.Any())
                    {
                        foreach (string emp in splitPersonRes)
                        {
                            var singleEmployee = await _employeeService.GetEmployee(emp);

                            if (singleEmployee != null && !string.IsNullOrEmpty(singleEmployee.Email))
                            {
                                emailSendTo += singleEmployee.Email + ";";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(emailSendTo))
                        emailSendTo = emailSendTo.Remove(emailSendTo.Length - 1);

                    if (string.IsNullOrEmpty(emailSendTo))
                        continue;

                    string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
                    await _emailService.SendEmailForAutomationAsync(833, EmailTypes.AutomatedMissedRecurringTask, Array.Empty<string>(),
                        Subject, body, string.Empty, emailSendTo.Split(';').ToArray());
                }
            }

        }

        public async Task CheckPastDueTasksAsync(DateTime reportDate)
        {
            var employees = await _employeeService.GetListActive();

            if (employees.Any())
            {
                foreach (var item in employees)
                {
                    var PastDueTasks = await _tasksService.GetRecurringTasks(
                    x => x.IsDeactivated == false
                    && x.IsDeleted == false
                    && x.PersonResponsible == item.FullName
                    && x.UpcomingDate.HasValue
                    && x.UpcomingDate < reportDate);

                    if (PastDueTasks.Any())
                    {
                        try
                        {
                            string Subject = "Recurring Tasks TWICE/MONTH REPORT";
                            string title = $@"RECURRING TASK PAST DUE DATE TWICE/MONTH REPORT OF {item.FullName}\n" +
                                            $"TOTAL NUMBER OF PAST DUE DATE TASK: {PastDueTasks.Count()}";

                            // create pdf using isharptext
                            var bytes = _fileManagerService.CreatePdfRecurringTaskReport(PastDueTasks.ToList(), title, reportDate);
                            MemoryStream stream = new MemoryStream(bytes);
                            string filePath = _fileManagerService.CreateRecurringTaskDirectory("Reports");
                            filePath = filePath + Guid.NewGuid() + ".pdf";
                            _fileManagerService.WriteToFile(stream, filePath);

                            string[] recipients = new string[]
                            {
                                "rarnold@camcomfginc.com",
                                "trinity.purdy@camcomfginc.com"
                            };

                            string body = EmailDefaults.GenerateEmailTemplate("Tasks", string.Empty);
                            await _emailService.SendEmailForAutomationAsync(834, EmailTypes.AutomatedRecurringTaskPastDueDateReport, Array.Empty<string>(),
                                Subject, body, filePath, recipients);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Recurring Task Audit Exception Information:", ex);
                        }
                    }
                }
            }
        }

        public async Task SendWarningMailBeforeDeactiveTaskAsync(string appUrl)
        {
            // tasks which are past due for between 83 and 90 days and not deactive 
            var PastDueTasks = _tasksService.GetRecurringTasksSync(
                x => x.IsDeactivated == false
                && x.IsDeleted == false
                && x.UpcomingDate.HasValue
                && x.UpcomingDate <= DateTime.Today.AddDays(-83)
                && x.UpcomingDate > DateTime.Today.AddDays(-90));

            // send mail for each task with certain date
            foreach (var DueTask in PastDueTasks)
            {
                StringBuilder customBody = new();
                customBody.Append($"<p><b>INITIATOR: </b>{DueTask.Initiator.ToUpper()}</p>");
                customBody.Append($"<p><b>PERSON RESPONSIBLE: </b>{DueTask.PersonResponsible.ToUpper()}</p>");
                customBody.Append($"<p><b>DESCRIPTION: </b>{DueTask.Description.ToUpper()}</p>");
                customBody.Append($"<p><b>UPCOMING DATE: </b>{DueTask.UpcomingDate?.Date.ToString("MM/dd/yyyy")}</p>");
                customBody.Append($"<p><b>FREQUENCY: </b>{DueTask.Frequency}</p>");
                customBody.Append($"<p><b>LINK TO MARK COMPLETED: </b>{appUrl + "viewrecurringtasks/OpenTask/" + DueTask.Id.ToString()}</p>");

                var updates = (await _tasksService.GetTaskUpdates1(DueTask.Id))
                    .OrderByDescending(x => x.UpdateId)
                    .Take(10);

                foreach(var update in updates)
                {
                    customBody.Append($"<p><b>UPDATE DATE: </b>{update.UpdateDate.ToString("M/dd/yyyy")}</p>");
                }

                var pastDue = (int)(DateTime.Now - (DateTime)DueTask.UpcomingDate).TotalDays;
                string estimatedDate = DateTime.Now.AddDays(90 - pastDue).ToString("MM/dd/yyyy");
                string Subject = "RECURRING TASK DEACTIVATED";

                string[] recipients = new string[]
                {
                    "rarnold@camcomfginc.com",
                    "trinity.purdy@camcomfginc.com"
                };

                string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
                await _emailService.SendEmailForAutomationAsync(835, EmailTypes.AutomatedRecurringTaskDeactivated, Array.Empty<string>(),
                    Subject, body, string.Empty, recipients);
            }
        }

        public async Task DeactivateTasksAsync()
        {
            // get tasks which are past due for 90 days and not deactive
            var PastDueTasks = _tasksService.GetRecurringTasksSync(
                x => x.IsDeactivated == false
                && x.IsDeleted == false
                && x.UpcomingDate.HasValue
                && x.UpcomingDate <= DateTime.Today.AddDays(-90));

            // make them deactive
            foreach (var item in PastDueTasks)
            {
                item.IsDeactivated = true;
                await _tasksService.UpdateRecurringTask(item);
            }
        }

        public async Task<bool> SendRecurringTaskAuditReportAsync(DateTime reportingDate)
        {
            bool isGeneratRepoart = false;
            var auditors = await _employeeService.GetListActive2();

            if (auditors.Any())
            {
                foreach (var item in auditors)
                {
                    double timeSpentMonth = TotalSpentTime(await _tasksService.GetUpdateReport(
                        item.LastName + ", " + item.FirstName, DateTime.Today, DateTime.Today.AddMonths(-1)), 60);

                    double timeSpentQuarter = TotalSpentTime(await _tasksService.GetUpdateReport(
                        item.LastName + ", " + item.FirstName, DateTime.Today, DateTime.Today.AddDays(-90)), 60);

                    var tasksToSend = await _tasksService.GetUpdateReport(
                        item.LastName + ", " + item.FirstName, reportingDate);

                    await RecTaskPastAverageTime(tasksToSend);

                    double timeSpentDaly = TotalSpentTime(tasksToSend, 60);

                    int pastDueDate = await _tasksService.CountRecurringTasks(false, false, reportingDate, item.FullName);

                    if (tasksToSend.Any())
                    {
                        try
                        {
                            string jobTitle = (await _jobDescriptionsService.GetByIdAsync(item.JobId)).Name;
                            string Subject = $"{jobTitle}";
                            string title = "RECURRING TASK UPDATE TIME REPORT OF " + item.LastName + ", "
                                + item.FirstName
                                + "\n" + "1 MONTH AVEREAGE: " + timeSpentMonth.ToString("F1") + " hrs" + ", "
                                + "3 MONTH AVERAGE: " + timeSpentQuarter.ToString("F1") + " hrs" + "\n"
                                + "TOTAL NUMBER OF PAST DUE TASKS: " + pastDueDate + "\n"
                                + "# OF COMPLETED TASKS: " + tasksToSend.Count() + "\n"
                                + "DAILY SPENT : "
                                + timeSpentDaly.ToString("F3") + " hrs";

                            // create pdf using isharptext
                            var bytes = _fileManagerService.CreatePdfForUpdateReport(tasksToSend.ToList(), title, reportingDate);
                            Stream stream = new MemoryStream(bytes);
                            string filePath = _fileManagerService.CreateRecurringTaskDirectory($@"Reports\{Guid.NewGuid()}");
                            filePath = $@"{filePath}COMPLETED RECURRING TASK LIST {item.FullName} {DateTime.Now.ToString("m_d_yy")}.pdf";
                            _fileManagerService.WriteToFile((MemoryStream)stream, filePath);

                            List<ColumnForTableModel> columns = new List<ColumnForTableModel>
                            {
                                new()
                                {
                                    Name = "Sl"
                                },
                                new()
                                {
                                    Name = "Task ID"
                                },
                                new()
                                {
                                    Name = "Start Time"
                                },
                                new()
                                {
                                    Name = "End Time"
                                },
                                new()
                                {
                                    Name = "Time Spent"
                                },
                                new()
                                {
                                    Name = "Past Average Time"
                                },
                                new()
                                {
                                    Name = "Task Description"
                                }
                            };

                            int sl = 1;
                            List<MainRowForTableModel> mainRows = new();
                            foreach (var task in tasksToSend)
                            {
                                List<RowDataForTableModel> rowsData = new()
                                {
                                    new()
                                    {
                                        Data = $"{sl++}"
                                    },
                                    new()
                                    {
                                        Data = $"{task.TaskId}"
                                    },
                                    new()
                                    {
                                        Data = task.StartTime
                                    },
                                    new()
                                    {
                                        Data = task.EndTime
                                    },
                                    new()
                                    {
                                        Data = $"{task.TimeSpent}"
                                    },
                                    new()
                                    {
                                        Data = $"{task.PastAverageTime}"
                                    },
                                    new()
                                    {
                                        Data = task.TaskDescription
                                    }
                                };

                                MainRowForTableModel mainRow = new MainRowForTableModel()
                                {
                                    RowsData = rowsData
                                };

                                mainRows.Add(mainRow);
                            }

                            StringBuilder tableHead = EmailDefaults.EmailTableHead(columns.Count.ToString(), "TIME REPORT", columns, new StringBuilder());

                            StringBuilder tableBody = EmailDefaults.EmailTableBody(mainRows);
                            string table = EmailDefaults.GenerateTableTemplate("100%", tableHead + tableBody.ToString());

                            StringBuilder customBody = new ();
                            customBody.Append($@"<p style=""display: flex; justify-content: space-between; font-weight: bold;"">
                                                <span>Name: {item.FullName}</span>
                                                <span># of Completed Tasks: {tasksToSend.Count()}</span>
                                                <span>Date: {reportingDate.Date.ToString("MM/dd/yyyy")}</span>
                                            </p>");
                            customBody.Append(table);

                            string[] recipients = new string[]
                            {
                                "rarnold@camcomfginc.com",
                                "trinity.purdy@camcomfginc.com"
                            };

                            string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
                            await _emailService.SendEmailForAutomationAsync(719, EmailTypes.AutomatedRecurringTaskReport, Array.Empty<string>(),
                                Subject, body, filePath, recipients);

                            isGeneratRepoart = true;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Recurring Task Audit Exception Information:", ex);
                        }
                    }
                }
            }

            return isGeneratRepoart;
        }

        public double TotalSpentTime(IEnumerable<UpdateReportViewModel> updateReportList, int timeFormat)
        {
            double totalSpentTime = 0;

            if (updateReportList == null)
            {
                return totalSpentTime;
            }

            totalSpentTime = updateReportList.Sum(e => e.TimeSpent);

            if (totalSpentTime > 0)
            {
                totalSpentTime = totalSpentTime / timeFormat;
            }

            return totalSpentTime;
        }

        public async Task<IEnumerable<UpdateReportViewModel>> RecTaskPastAverageTime(IEnumerable<UpdateReportViewModel> updateReportList)
        {
            if (!updateReportList.Any()) return updateReportList;

            foreach (var item in updateReportList)
            {
                var updateList = await _tasksService.GetUpdateReport(item.TaskId);

                if (!updateList.Any()) continue;

                int totalSpentTime = updateReportList.Sum(e => e.TimeSpent);

                if (totalSpentTime > 0)
                {
                    item.PastAverageTime = (totalSpentTime / updateList.Count());
                }
            }

            return updateReportList;
        }

        public async Task DeactiveRecurringTasksReportAsync()
        {
            var deactiveTasksList = (await _tasksService.GetRecurringTasks(
                x => !x.IsDeleted 
                && x.IsDeactivated))
                .OrderBy(x => x.PersonResponsible)
                .ToList();

            if (deactiveTasksList.Any())
            {
                List<ColumnForTableModel> columns = new List<ColumnForTableModel>
                {
                    new()
                    {
                        Name = "SL"
                    },
                    new()
                    {
                        Name = "TASK ID"
                    },
                    new()
                    {
                        Name = "TASK SUBJECT"
                    },
                    new()
                    {
                        Name = "DESCRIPTION"
                    },
                    new()
                    {
                        Name = "DUE DATE"
                    },
                    new()
                    {
                        Name = "INITIATOR"
                    },
                    new()
                    {
                        Name = "PERSON RESPONSIBLE"
                    },
                    new()
                    {
                        Name = "JOB TITLE"
                    },
                    new()
                    {
                        Name = "DATE CREATED"
                    },
                    new()
                    {
                        Name = "LAST DATE COMPLETED"
                    },
                    new()
                    {
                        Name = "REQUIRED"
                    },
                    new()
                    {
                        Name = "FREQUENCY"
                    }
                };

                int serialNumber = 1;
                List<MainRowForTableModel> mainRows = new();

                foreach (var task in deactiveTasksList)
                {
                    List<RowDataForTableModel> rowsData = new()
                    {
                        new()
                        {
                            Data = $"{serialNumber++}"
                        },
                        new()
                        {
                            Data = $"{task.Id}"
                        },
                        new()
                        {
                            Data = task.TaskDescriptionSubject
                        },
                        new()
                        {
                            Data = task.Description
                        },
                        new()
                        {
                            Data = task.UpcomingDate?.ToString("M/dd/yyyy")
                        },
                        new()
                        {
                            Data = task.Initiator
                        },
                        new()
                        {
                            Data = task.PersonResponsible
                        },
                        new()
                        {
                            Data = task.JobTitle
                        },
                        new()
                        {
                            Data = task.DateCreated?.ToString("M/dd/yyyy")
                        },
                        new()
                        {
                            Data = task.DateCompleted?.ToString("M/dd/yyyy")
                        },
                        new()
                        {
                            Data = $"{task.IsPassOrFail} {task.IsGraphRequired} {task.IsPicRequired} {task.IsQuestionRequired} {task.IsAuditRequired}"
                        },
                        new()
                        {
                            Data = task.TasksFreq.Frequency
                        }
                    };

                    MainRowForTableModel mainRow = new MainRowForTableModel()
                    {
                        RowsData = rowsData
                    };

                    mainRows.Add(mainRow);
                }

                StringBuilder tableHead = EmailDefaults.EmailTableHead(columns.Count.ToString(), "RECURRING TASK LIST", columns, new StringBuilder());

                StringBuilder tableBody = EmailDefaults.EmailTableBody(mainRows);
                string table = EmailDefaults.GenerateTableTemplate("100%", tableHead + tableBody.ToString());

                string mailBody = $@"<p style='text-align: center;font-weight: bold;'>
                                          WAITING TO BE RE-ASSIGNED RECURRING TASK LIST
                                        </p>
                                        <p style='text-align: center;font-weight: bold;'>
                                          TOTAL NUMBER OF RE-ASSIGNED TASK: {deactiveTasksList.Count()}
                                        </p>" + table;

                string emailSubject = "WAITING TO BE RE-ASSIGNED";

                string[] recipients = new string[]
                {
                    "rarnold@camcomfginc.com",
                    "trinity.purdy@camcomfginc.com"
                };

                mailBody = EmailDefaults.GenerateEmailTemplate("Tasks", mailBody);
                await _emailService.SendEmailForAutomationAsync(836, EmailTypes.AutomatedRecurringTaskWaitingToReAssigned, Array.Empty<string>(),
                    emailSubject, mailBody, string.Empty, recipients);
            }
        }
    }
}
