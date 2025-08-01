using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure.Repository.HR
{
    public class DepartmentAndManagerRepository : Repository<DepartmentAndManager>, IDepartmentAndManagerRepository
    {
        public DepartmentAndManagerRepository(DatabaseContext context) : base(context)
        {
        }

        private DatabaseContext DatabaseContext => (DatabaseContext)Context;

        public async Task<string> GetDepartmentNameFromDepartmentAndManagerByDepartmentAndManagerIdAsync(long departmentAndManagerId)
        {
            if (departmentAndManagerId <= 0)
            {
                return "NOT FOUND";
            }

            var departmentName = await (from departmentAndManager in DatabaseContext.DepartmentsAndManagers
                                        where departmentAndManager.Id == departmentAndManagerId
                                        select departmentAndManager.DepartmentName).FirstOrDefaultAsync();
            return departmentName ?? "NOT FOUND";
        }

        public async Task<string> GetDepartmentAbbreviationFromDepartmentAndManagerByDepartmentIdAsync(long departmentAndManagerId)
        {
            if (departmentAndManagerId == 0)
            {
                return "N/A";
            }

            if (departmentAndManagerId < -1)
            {
                return "N/A";
            }

            if (departmentAndManagerId == -1)
            {
                return "CW";
            }

            return await (from departmentAndManager in DatabaseContext.DepartmentsAndManagers
                          where departmentAndManager.Id == departmentAndManagerId
                          select departmentAndManager.DepartmentAbbreviation).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetFilteredDepartmentsAsync(bool isDelete, List<long> departmentIds)
        {
            var query = DatabaseContext.DepartmentsAndManagers.AsQueryable();

            query = query.Where(x => x.IsDeleted == isDelete);

            if (departmentIds != null && departmentIds.Any())
            {
                query = query.Where(x => departmentIds.Contains(x.Id));
            }

            var departmentNames = await query
                .OrderBy(x => x.Id)
                .Select(x => x.DepartmentName)
                .ToListAsync();

            return departmentNames;
        }
        public async Task<IEnumerable<string>> GetListAsync(bool isDelete, List<long> departmentId)
        {
            return await DatabaseContext.DepartmentsAndManagers
                .Where(x => x.IsDeleted == isDelete
                            && departmentId.Contains(x.Id))
                .OrderBy(x => x.Id)
                .Select(x => x.DepartmentName)
                .ToListAsync();
        }
       
    }
}
