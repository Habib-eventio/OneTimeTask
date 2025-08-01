// This File Needs to be reviewed Still. Don't Remove this comment.
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure.Repositories.TaskInfo
{
    public class TaskUpdateRepository : Repository<TaskUpdate>, ITaskUpdateRepository
    {
        public TaskUpdateRepository(DatabaseContext context) : base(context)
        {
        }

        private DatabaseContext DatabaseContext => (DatabaseContext)Context;

        public async Task<List<TaskUpdate>> GetLatestUpdates()
        {
            List<TaskUpdate> result = new List<TaskUpdate>();

            try
            {
                var items = DatabaseContext.TaskUpdates
                    .Where(x => x.IsDeleted == false && x.TaskId != null)
                    .GroupBy(x => x.TaskId)
                    .Select(group => new TaskUpdate
                    {
                        TaskId = group.Key,
                        UpdateDate = group.OrderByDescending(x => x.UpdateDate).FirstOrDefault().UpdateDate
                    });

                result = await Task.FromResult(items.ToList());

                return result;
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }

        public async Task<IEnumerable<TaskUpdate>> GetTaskUpdatesAsync(string ignoreSystemGenerated,
            string ignoreNudgedUpdate, string ignoreEmailUpdate, bool hasRecurringTaskValue, bool hasDueDateValue,
            int dueDateDuration, bool isDelete)
        {
            DateTime fromDate = DateTime.Now.Date;
            DateTime toDate = DateTime.Today.AddMonths(dueDateDuration).Date;

            var items = DatabaseContext.TaskUpdates.Where(x => x.IsDeleted == isDelete
                                                               && !x.Update.Contains(ignoreNudgedUpdate)
                                                               && !x.Update.Contains(ignoreSystemGenerated)
                                                               && !x.Update.Contains(ignoreEmailUpdate)
                                                               && x.RecurringId.HasValue == hasRecurringTaskValue
                                                               && x.DueDate.HasValue == hasDueDateValue &&
                                                               (x.DueDate <= fromDate && x.DueDate >= toDate));

            return await items.OrderBy(x => x.UpdateId).ToListAsync();
        }

        public async Task<List<TaskUpdate>> GetTaskUpdatesAsync(int recurringTaskId, bool isDeleted)
        {
            return await DatabaseContext.TaskUpdates
                .AsNoTracking()
                .OrderByDescending(x => x.UpdateDate)
                .Where(x => x.RecurringId == recurringTaskId && x.IsDeleted == isDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskUpdate>> GetTaskUpdatesForPercentageAsync(string ignoreSystemGenerated,
            string ignoreNudgedUpdate, string ignoreEmailUpdate, bool hasRecurringTaskValue, bool hasDueDateValue,
            int dueDateDuration, bool isDelete)
        {
            DateTime fromDate = DateTime.Now.Date;
            DateTime toDate = DateTime.Today.AddMonths(dueDateDuration).Date;

            return await DatabaseContext.TaskUpdates.Where(x => x.IsDeleted == isDelete
                                                                && !x.Update.Contains(ignoreNudgedUpdate)
                                                                && !x.Update.Contains(ignoreSystemGenerated)
                                                                && !x.Update.Contains(ignoreEmailUpdate)
                                                                && x.RecurringId.HasValue == hasRecurringTaskValue
                                                                && x.DueDate.HasValue == hasDueDateValue &&
                                                                (x.DueDate <= fromDate && x.DueDate >= toDate))
                .Select(x => new TaskUpdate()
                {
                    UpdateId = x.UpdateId,
                    RecurringId = x.RecurringId,
                    UpdateDate = x.UpdateDate,
                    DueDate = x.DueDate
                })
                .OrderBy(x => x.UpdateId)
                .ToListAsync();
        }

        public async Task<List<TaskUpdate>> GetTaskUpdatesAsync(int recurringId, string ignoreNudgedUpdate,
            string ignoreEmailUpdate, bool isDeleted, bool hasDueDateValue, int dueDateDuration, bool isTaskCompleted)
        {
            DateTime fromDate = DateTime.Now.Date;
            DateTime toDate = DateTime.Today.AddMonths(dueDateDuration).Date;

            var items = DatabaseContext.TaskUpdates.Where(x => x.IsDeleted == isDeleted
                                                               && !x.Update.Contains(ignoreNudgedUpdate)
                                                               && !x.Update.Contains(ignoreEmailUpdate)
                                                               && (x.RecurringId != null &&
                                                                   x.RecurringId == recurringId)
                                                               && x.DueDate.HasValue == hasDueDateValue
                                                               && (x.DueDate <= fromDate && x.DueDate >= toDate)
                                                               && x.TaskCompleted != null &&
                                                               (x.TaskCompleted == isTaskCompleted));

            return await items.OrderBy(x => x.UpdateId).ToListAsync();
        }

        public async Task<int> AddTaskUpdate(TaskUpdate update)
        {
            if (string.IsNullOrEmpty(update.Update))
            {
                update.Update = "";
            }

            DatabaseContext.TaskUpdates.Add(update);
            await DatabaseContext.SaveChangesAsync();
            return update.UpdateId;
        }

        public int AddTaskUpdateSync(TaskUpdate update)
        {
            if (string.IsNullOrEmpty(update.Update))
            {
                update.Update = "";
            }

            DatabaseContext.TaskUpdates.Add(update);
            DatabaseContext.SaveChanges();
            return update.UpdateId;
        }

        public async Task<bool> UpdateTaskUpdateSync(TaskUpdate update)
        {
            if (string.IsNullOrEmpty(update.Update)) update.Update = "";

            var local = DatabaseContext.Set<TaskUpdate>().Local
                .FirstOrDefault(entry => entry.UpdateId.Equals(update.UpdateId));

            if (local != null)
            {
                // detach
                DatabaseContext.Entry(local).State = EntityState.Detached;
            }

            DatabaseContext.Entry(update).State = EntityState.Modified;

            DatabaseContext.TaskUpdates.Update(update);
            await DatabaseContext.SaveChangesAsync();
            await DatabaseContext.Entry(update).ReloadAsync();

            return true;
        }

        public async Task<bool> RemoveTaskUpdate(TaskUpdate update)
        {
            DatabaseContext.TaskUpdates.Remove(update);
            await DatabaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<TaskUpdate> GetRecurringTaskLatestUpdateAsync(int recurringTaskId)
        {
            return await DatabaseContext.TaskUpdates
                .OrderByDescending(x => x.UpdateId)
                .FirstOrDefaultAsync(x => x.RecurringId == recurringTaskId && x.IsDeleted == false);
        }

        public async Task<bool> UpdateTaskUpdate(TaskUpdate update)
        {
            if (string.IsNullOrEmpty(update.Update)) update.Update = "";

            var local = DatabaseContext.Set<TaskUpdate>().Local
                .FirstOrDefault(entry => entry.UpdateId.Equals(update.UpdateId));

            if (local != null)
            {
                // detach
                DatabaseContext.Entry(local).State = EntityState.Detached;
            }

            DatabaseContext.Entry(update).State = EntityState.Modified;

            DatabaseContext.TaskUpdates.Update(update);
            await DatabaseContext.SaveChangesAsync();
            await DatabaseContext.Entry(update).ReloadAsync();

            return true;
        }
    }
}
