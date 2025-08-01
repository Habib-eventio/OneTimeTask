// This File Needs to be reviewed Still. Don't Remove this comment.
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using Microsoft.EntityFrameworkCore;
using System;
using CamcoTasks.Infrastructure.Entities.Task;
using CamcoTasks.Infrastructure.IRepositories;

namespace CamcoTasks.Infrastructure.Repositories.TaskInfo
{
    public class RecurringTaskRepository : Repository<RecurringTask>,
        IRecurringTaskRepository
    {
        public RecurringTaskRepository(DatabaseContext context) : base(context)
        {

        }

        private DatabaseContext DatabaseContext => (DatabaseContext)Context;

        public async Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAsync(
            bool isDeleted, bool isDeactivated, int? parentTaskId)
        {
            return await DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(x =>
                x.IsDeleted == isDeleted && x.IsDeactivated == isDeactivated && x.IsApproved &&
                x.ParentTaskId.Equals(parentTaskId)).ToListAsync();
        }

        public async Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdOrderByIdAsync(
            bool isDeleted, bool isDeactivated, int? parentTaskId)
        {
            return await DatabaseContext.RecurringTasks
                .Include(x => x.TasksFrequency)
                .Where(x => x.IsDeleted == isDeleted
                            && x.IsDeactivated == isDeactivated
                            && x.ParentTaskId.Equals(parentTaskId))
                .Select(x => new RecurringTask()
                {
                    Id = x.Id,
                    PersonResponsible = x.PersonResponsible,
                    UpcomingDate = x.UpcomingDate,
                    DateCompleted = x.DateCompleted,
                    TasksFrequency = x.TasksFrequency,
                    IsApproved = x.IsApproved,
                    TaskArea = x.TaskArea,
                })
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndSearchValueAsync(
            bool isDeleted, bool isDeactivated, string searchPattern)
        {
            IQueryable<RecurringTask> items;

            if (DateTime.TryParse(searchPattern, out var dateTime))
            {
                dateTime = DateTime.Parse(searchPattern);

                items = DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(x =>
                    x.UpcomingDate == dateTime
                    || x.DateCompleted == dateTime
                    || x.DateCreated == dateTime
                    || x.DateCompleted == dateTime
                    && x.IsDeleted == isDeleted
                    && x.IsDeactivated == isDeactivated);
            }
            else
            {
                items = DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(
                    x => (Convert.ToString(x.Id).Contains(searchPattern)
                          || Convert.ToString(x.TaskDescriptionSubject)
                              .Contains(searchPattern)
                          || Convert.ToString(x.Location).Contains(searchPattern)
                          || Convert.ToString(x.Description).Contains(searchPattern)
                          || Convert.ToString(x.Initiator).Contains(searchPattern)
                          || Convert.ToString(x.PersonResponsible).Contains(searchPattern)
                          || Convert.ToString(x.JobTitle).Contains(searchPattern)
                          || Convert.ToString(x.AuditPerson).Contains(searchPattern)
                          || Convert.ToString(x.TaskArea).Contains(searchPattern)
                          || Convert.ToString(x.TasksFrequency.Frequency).Contains(searchPattern)
                          || (x.LatestGraphValue.HasValue
                              && Convert.ToString(x.LatestGraphValue.Value).Contains(searchPattern))
                          || (x.IsPassOrFail.HasValue
                              && Convert.ToString(x.IsPassOrFail.Value).Contains(searchPattern))
                          || (x.IsPicRequired.HasValue
                              && Convert.ToString(x.IsPicRequired.Value).Contains(searchPattern))
                          || (x.IsQuestionRequired.HasValue
                              && Convert.ToString(x.IsQuestionRequired.Value).Contains(searchPattern))
                          || (x.IsAuditRequired.HasValue
                              && Convert.ToString(x.IsAuditRequired.Value).Contains(searchPattern)))
                         && x.IsDeleted == isDeleted
                         && x.IsDeactivated == isDeactivated);
            }

            return await items.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<List<RecurringTask>>
            GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndSkipAndTakeAsync(bool isDeleted,
                bool isDeactivated, int? parentTaskId, int skip, int take)
        {
            return await DatabaseContext.RecurringTasks
                .Include(x => x.TasksFrequency)
                .Where(x => x.IsDeleted == isDeleted && x.IsDeactivated == isDeactivated && x.IsApproved &&
                            x.ParentTaskId.Equals(parentTaskId))
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<List<RecurringTask>>
            GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndPersonResponsibleAsync(bool isDeleted,
                bool isDeactivated,
                int? parentTaskId, string personRep)
        {
            return await DatabaseContext.RecurringTasks
                .Include(x => x.TasksFrequency)
                .Where(x => x.IsDeleted == isDeleted
                            && x.IsDeactivated == isDeactivated
                            && x.IsApproved
                            && x.ParentTaskId.Equals(parentTaskId)
                            && x.PersonResponsible.Contains(personRep)).ToListAsync();
        }

        public async Task<List<RecurringTask>>
            GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndTaskAreaAsync(bool isDeleted,
                bool isDeactivated, bool isApproved, int? parentTaskId,
                string taskArea)
        {
            return await DatabaseContext.RecurringTasks
                .Include(x => x.TasksFrequency)
                .Where(x => x.IsDeleted == isDeleted
                            && x.IsDeactivated == isDeactivated
                            && x.IsApproved == isApproved
                            && x.ParentTaskId.Equals(parentTaskId)
                            && x.TaskArea.Equals(taskArea))
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public List<RecurringTask> GetRecurringTasksSync(Func<RecurringTask, bool> p)
        {
            return DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(p).ToList();
        }

        public List<RecurringTask> GetRecurringTasks(int skip, int take, Func<RecurringTask, bool> p)
        {
            return DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(p).Skip(skip).Take(take)
                .ToList();
        }

        public async Task<List<RecurringTask>> GetRecurringTasks()
        {
            return await DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency)
                .Where(x => !x.IsDeleted && x.IsDeactivated == false && x.IsApproved).ToListAsync();
        }

        public async Task<List<RecurringTask>> GetRecurringTasksAsync(DateTime fromDateCompleted,
            DateTime toDateCompleted,
            bool isDelete, bool isDeactivated)
        {
            return await DatabaseContext.RecurringTasks
                .Include(x => x.TasksFrequency)
                .Where(x => x.IsDeleted == isDelete
                            && x.IsDeactivated == isDeactivated
                            && x.IsApproved
                            && (x.DateCompleted <= fromDateCompleted && x.DateCompleted >= toDateCompleted))
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<RecurringTask>> GetSubTasks(int taskId)
        {
            return await DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(x =>
                !x.IsDeleted && x.IsDeactivated == false && x.IsApproved && x.ParentTaskId == taskId).ToListAsync();
        }

        public async Task<List<RecurringTask>> GetOverdueRecurringTasksAsync()
        {
            return await DatabaseContext.RecurringTasks
                .Where(a => a.IsDeleted == false && a.IsDeactivated == false && a.IsApproved &&
                            a.UpcomingDate.HasValue && a.UpcomingDate.Value.Date < DateTime.Today)
                .OrderBy(a => a.PersonResponsible).ThenBy(b => b.Id).ToListAsync();
        }

        public async Task<List<RecurringTask>> GetRecurringTasks(Func<RecurringTask, bool> p)
        {
            var items = DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(p);
            return await Task.FromResult(items.ToList());
        }

        public async Task<List<UpdateReportViewModel>> GetUpdateReport(string auditPerson, DateTime reportingDate)
        {
            List<UpdateReportViewModel> result = new List<UpdateReportViewModel>();

            try
            {
                var items = await (from tu in DatabaseContext.TaskUpdates
                                   where tu.IsDeleted != true && tu.CreatedDate != null
                                                              && tu.CreatedDate.Value.Date == reportingDate &&
                                                              tu.RecurringId != null
                                   join rec in DatabaseContext.RecurringTasks on tu.RecurringId equals rec.Id
                                   where rec.AuditPerson == auditPerson
                                   select new UpdateReportViewModel
                                   {
                                       CreatedDate = tu.CreatedDate,
                                       TaskId = rec.Id,
                                       EndTime = tu.CreatedDate.Value.ToString("hh:mm tt"),
                                       TaskDescription = rec.Description
                                   })
                    .OrderBy(x => x.CreatedDate).ToListAsync();
                result = items;
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }

        public async Task<List<UpdateReportViewModel>> GetUpdateReport(int recTaskId)
        {
            List<UpdateReportViewModel> result = new List<UpdateReportViewModel>();

            try
            {
                var items = await (from tu in DatabaseContext.TaskUpdates
                                   where tu.IsDeleted != true && tu.CreatedDate != null
                                                              && tu.RecurringId != null && tu.RecurringId == recTaskId
                                   join rec in DatabaseContext.RecurringTasks on tu.RecurringId equals rec.Id
                                   select new UpdateReportViewModel
                                   {
                                       CreatedDate = tu.CreatedDate,
                                       TaskId = tu.UpdateId,
                                       EndTime = tu.CreatedDate.Value.ToString("hh:mm tt"),
                                       TaskDescription = rec.Description
                                   })
                    .OrderBy(x => x.CreatedDate).ToListAsync();
                result = items;
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }

        public async Task<List<UpdateReportViewModel>> GetUpdateReport(string auditPerson, DateTime reportingFromDate,
            DateTime reportingToDate)
        {
            List<UpdateReportViewModel> result = new List<UpdateReportViewModel>();

            try
            {
                var items = await (from tu in DatabaseContext.TaskUpdates
                                   where tu.IsDeleted != true && tu.CreatedDate != null
                                                              && tu.CreatedDate.Value.Date <= reportingFromDate
                                                              && tu.CreatedDate.Value.Date >= reportingToDate
                                                              && tu.RecurringId != null
                                   join rec in DatabaseContext.RecurringTasks on tu.RecurringId equals rec.Id
                                   where rec.AuditPerson == auditPerson
                                   select new UpdateReportViewModel
                                   {
                                       CreatedDate = tu.CreatedDate,
                                       TaskId = rec.Id,
                                       EndTime = tu.CreatedDate.Value.ToString("hh:mm tt"),
                                       TaskDescription = rec.Description
                                   })
                    .OrderBy(x => x.CreatedDate).ToListAsync();

                result = items;
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }

        public async Task<int> CountRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId)
        {
            var items = DatabaseContext.RecurringTasks
                .Include(x => x.TasksFrequency)
                .Where(x => x.IsDeleted == isDeleted && x.IsDeactivated == isDeactivated &&
                            x.ParentTaskId.Equals(parentTaskId));
            return await items.CountAsync();
        }

        public async Task<RecurringTask> GetRecurringTaskById(int id)
        {
            var item = await DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            return item;
        }

        public RecurringTask GetRecurringTaskByIdSync(int id)
        {
            return DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<int> GetRecurringTasksCountAsync()
        {
            return await DatabaseContext.RecurringTasks
                .Include(x => x.TasksFrequency)
                .Where(x => !x.IsDeleted && x.IsDeactivated == false)
                .CountAsync();
        }

        public async Task<int> GetRecurringTasksCountAsync(bool isDeleted, bool isDeActivate)
        {
            return await DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency)
                .Where(x => x.IsDeleted == isDeleted && x.IsDeactivated == isDeActivate).CountAsync();
        }

        public List<RecurringTask> GetRecurringTasksSync()
        {
            return DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency)
                .Where(x => !x.IsDeleted && x.IsDeactivated == false).ToList();
        }

        public async Task<bool> ClearTaskType(string taskType)
        {
            //Update it later *IMPORTANT
            //await DatabaseContext.Database.ExecuteSqlRawAsync($"ClearTaskType '{taskType}'");
            return true;
        }

        public async Task<int> GetMaxRecurringId()
        {
            int maxId = 0;
            int recTaskCount = await GetRecurringTasksCountAsync();

            if (recTaskCount > 0)
                maxId = await DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).MaxAsync(x => x.Id);

            return maxId;
        }

        public async Task<int> GetRecurringTaskCount(Func<RecurringTask, bool> p)
        {
            var items = DatabaseContext.RecurringTasks.Include(x => x.TasksFrequency).Where(p);
            return await Task.FromResult(items.Count());
        }
    }
}
