// This File Needs to be reviewed Still. Don't Remove this comment.
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.IRepositories;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure.Repositories.TaskInfo
{
    public class TaskTaskRepository : Repository<TaskTask>, ITaskTaskRepository
    {
        public TaskTaskRepository(DatabaseContext context) : base(context)
        {
        }

        private DatabaseContext DatabaseContext => (DatabaseContext)Context;

        public async Task<List<IGrouping<string, TaskTask>>> GetActiveTasks()
        {
            var tasks = await DatabaseContext.TaskTasks.Where(x => x.DateCompleted == null).ToListAsync();
            return tasks.GroupBy(x => x.TaskType).Where(x => x.Count() > 5).ToList();
        }

        public Task<TaskTask?> GetAsync(int id) => base.GetAsync(id);

        public async Task<int> AddTask(TaskTask task)
        {
            if (task.Priority < 1)
            {
                task.Priority = 1;
            }

            var taskSamePriority = DatabaseContext.TaskTasks.FirstOrDefault(a => a.TaskType == task.TaskType
                                                                                 && !a.DateCompleted.HasValue &&
                                                                                 a.Priority == task.Priority);

            if (taskSamePriority != null)
            {
                var previousPriority = DatabaseContext.TaskTasks.Max(a => a.Priority) + 1;

                var dbTaskTemp = await DatabaseContext.TaskTasks.SingleOrDefaultAsync(a => a.Id == taskSamePriority.Id);

                dbTaskTemp.DateAdded = taskSamePriority.DateAdded;
                dbTaskTemp.DateCompleted = taskSamePriority.DateCompleted;
                dbTaskTemp.Description = taskSamePriority.Description;
                dbTaskTemp.Initiator = taskSamePriority.Initiator;
                dbTaskTemp.Priority = previousPriority;
                dbTaskTemp.TaskType = taskSamePriority.TaskType;
                dbTaskTemp.Update = taskSamePriority.Update;
                dbTaskTemp.Progress = task.Progress;
                dbTaskTemp.PictureLink = task.PictureLink;
                dbTaskTemp.FileLink = task.FileLink;
                dbTaskTemp.FileLink = task.FileLink;
                dbTaskTemp.NudgeCount = task.NudgeCount;
                dbTaskTemp.Department = task.Department;

            }

            task.DateAdded ??= DateTime.Now;

            DatabaseContext.TaskTasks.Add(task);
            await DatabaseContext.SaveChangesAsync();
            return task.Id;
        }

        private async Task UpdateTaskUpdate(TaskUpdate update)
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

        }

        public async Task<bool> UpdateOneTask(TaskTask task)
        {
            if (task.Priority < 1)
            {
                task.Priority = 1;
            }

            var entityLookUp = await DatabaseContext.TaskTasks.SingleOrDefaultAsync(a => a.Id == task.Id);

            if (entityLookUp != null)
            {
                DatabaseContext.Entry(entityLookUp).State = EntityState.Detached;

                DatabaseContext.Entry(task).State = EntityState.Modified;

                await DatabaseContext.SaveChangesAsync();
                await DatabaseContext.Entry(task).ReloadAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveOneTask(TaskTask task)
        {
            var dbTask = await DatabaseContext.TaskTasks.SingleOrDefaultAsync(a => a.Id == task.Id);

            dbTask.IsDeleted = task.IsDeleted;

            await DatabaseContext.SaveChangesAsync();

            foreach (var update in task.TasksTaskUpdates)
            {
                update.IsDeleted = true;
                await UpdateTaskUpdate(update);
            }

            return true;
        }

        public async Task<bool> RemoveTask(TaskTask task)
        {
            DatabaseContext.TaskUpdates.RemoveRange(task.TasksTaskUpdates);
            DatabaseContext.TaskTasks.Remove(task);
            await DatabaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTask(TaskTask task, int? previousPriority)
        {
            if (task.Priority < 1)
                task.Priority = 1;

            var taskSamePriority = DatabaseContext.TaskTasks.FirstOrDefault(a => a.Id != task.Id &&
                                                                                 a.TaskType == task.TaskType &&
                                                                                 a.Priority == task.Priority);

            var dbTask = await DatabaseContext.TaskTasks.SingleOrDefaultAsync(a => a.Id == task.Id);

            if (taskSamePriority != null)
            {
                var dbTaskTemp = await DatabaseContext.TaskTasks.SingleOrDefaultAsync(a => a.Id == taskSamePriority.Id);

                dbTaskTemp.DateAdded = taskSamePriority.DateAdded;
                dbTaskTemp.DateCompleted = taskSamePriority.DateCompleted;
                dbTaskTemp.Description = taskSamePriority.Description;
                dbTaskTemp.Initiator = taskSamePriority.Initiator;
                dbTaskTemp.Priority = previousPriority;
                dbTaskTemp.TaskType = taskSamePriority.TaskType;
                dbTaskTemp.Update = taskSamePriority.Update;
                dbTaskTemp.Progress = task.Progress;
                dbTaskTemp.PictureLink = task.PictureLink;
                dbTaskTemp.FileLink = task.FileLink;

                dbTask.DateAdded = task.DateAdded;
                dbTask.DateCompleted = task.DateCompleted;
                dbTask.Description = task.Description;
                dbTask.Initiator = task.Initiator;
                dbTask.Priority = task.Priority;
                dbTask.TaskType = task.TaskType;
                dbTask.Update = task.Update;
                dbTask.Progress = task.Progress;
                dbTask.FileLink = task.FileLink;
                dbTask.IsDeleted = task.IsDeleted;
                dbTask.IsReviewed = task.IsReviewed;
                dbTask.PictureLink = task.PictureLink;
                dbTask.NudgeCount = task.NudgeCount;
                dbTask.Department = task.Department;
                dbTask.EmailCount = dbTask.EmailCount;

                await DatabaseContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    var entityLookUp = (await DatabaseContext.TaskTasks.FindAsync(task.Id));
                    DatabaseContext.Entry(entityLookUp).State = EntityState.Detached;

                    DatabaseContext.Entry(task).State = EntityState.Modified;

                    await DatabaseContext.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return true;
        }
       
    }
}
