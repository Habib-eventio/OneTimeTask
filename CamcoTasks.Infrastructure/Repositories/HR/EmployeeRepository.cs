// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.Repository.IRepository.HR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace CamcoTasks.Infrastructure.Repository.HR;

public class EmployeeRepository : Repository<Employee>,
    IEmployeeRepository
{
    public EmployeeRepository(DatabaseContext context) : base(context)
    {

    }
    private DateTime CalculateStartOfWeek(DateTime date)
    {
        int difference = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays((-1 * difference) - 1).Date;
    }

    public async Task<float> GetEmployeeRateAsync(string employeeName, int? weekNumber, DateTime? date)
    {
        try
        {
            float rate = 0.0f;
            DateTime startOfDate = CalculateStartOfWeek(date ?? DateTime.Now);
            string[] names = employeeName.Split(",", StringSplitOptions.TrimEntries);
            var customEmployeeId = DatabaseContext.Employees.Where(a => a.FirstName == names[1] && a.LastName == names[0]).Select(a => a.CustomEmployeeId).FirstOrDefault();
            rate = (float)(DatabaseContext.WeeklyWages.Where(a => a.Date.HasValue && a.HourlyWage.HasValue && startOfDate >= a.Date && startOfDate < a.Date.Value.AddDays(7) && a.CustomEmployeeId == customEmployeeId).Select(b => b.HourlyWage).FirstOrDefault() ?? 0.0M);

            //If there is no wage present for the week that time was attributed
            if (rate == 0.0f)
            {
                //Find the closest date where there is a wage for this employee and grab the wage from there
                var foundRates = await DatabaseContext.WeeklyWages.Where(a => a.Date.HasValue && a.HourlyWage.HasValue && a.CustomEmployeeId == customEmployeeId && a.HourlyWage > 0).Select(b => new { Date = b.Date.Value, Wage = b.HourlyWage.Value }).ToListAsync();

                //If there are no wages for that employee
                if (foundRates.Count == 0)
                {
                    //Find the wages of all employees for the original week
                    foundRates = await DatabaseContext.WeeklyWages.Where(a => a.Date.HasValue && a.HourlyWage.HasValue && startOfDate >= a.Date && startOfDate < a.Date.Value.AddDays(7) && a.HourlyWage > 0).Select(b => new { Date = b.Date.Value, Wage = b.HourlyWage.Value }).ToListAsync();
                    //If there are no wages for any employees for that week
                    if (foundRates.Count != 0)
                    {
                        //Find the closest date where there are wages
                        var closestDate = await DatabaseContext.WeeklyWages.Where(a => a.Date.HasValue && a.HourlyWage.HasValue && a.HourlyWage > 0).Select(a => a.Date.Value).ToListAsync();
                        closestDate = closestDate.OrderBy(a => Math.Abs((a - startOfDate).Days)).ToList();

                        //Set the found rates to the wages for that week
                        foundRates = await DatabaseContext.WeeklyWages.Where(a => a.Date.HasValue && a.HourlyWage.HasValue && a.HourlyWage > 0 && a.Date == closestDate.FirstOrDefault()).Select(b => new { Date = b.Date.Value, Wage = b.HourlyWage.Value }).ToListAsync();
                    }

                    //Rate is the average of either the employees of the week or the average of the closest week that has wages
                    rate = (float)foundRates.Average(b => b.Wage);
                }
                else
                {
                    //Rate is the wage found for the closest week to the start date for the employee
                    rate = (float)foundRates.OrderBy(a => Math.Abs((a.Date - startOfDate).Days)).FirstOrDefault().Wage;
                }
            }

            //If we can't find any wages ever, something is very wrong.
            if (rate == 0.0f)
            {
                throw new Exception("There are no wages ever. Please contact the IT and/or Finance manager(s) immediately as this is not good.", new Exception($"No one has wages in the HR Weekly Wage table. Something is very, very wrong."));
            }

            return rate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<Employee>> GetEmployeeHierarchyAsync(long employeeId)
    {
        async Task<IEnumerable<Employee>> GetHierarchyAsync(Employee employee, HashSet<long> visited)
        {
            if (!visited.Add(employee.Id))
                return Enumerable.Empty<Employee>();

            var result = new List<Employee> { employee };

            var children = await DatabaseContext.Employees
                .Where(e =>
                    (e.ManagerEmployeeId == employee.Id || e.LeaderEmployeeId == employee.Id) &&
                    !e.IsDeleted)
                .ToListAsync();

            foreach (var child in children)
            {
                var descendants = await GetHierarchyAsync(child, visited);
                result.AddRange(descendants);
            }

            return result;
        }

        // ✅ Don't filter by EmploymentStatusId here
        var rootEmployee = await DatabaseContext.Employees
            .FirstOrDefaultAsync(e => e.Id == employeeId && !e.IsDeleted);

        if (rootEmployee == null)
            return new List<Employee>();

        var visited = new HashSet<long>();
        var hierarchy = await GetHierarchyAsync(rootEmployee, visited);

        return hierarchy
            .Where(e => !e.IsDeleted) // Optional: additional filter if needed
            .Distinct()
            .ToList();
    }

    private DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public async Task<List<EmployeeInfo>> GetActiveJarvisEmployeesListAsync()
    {
        return await (from employee in DatabaseContext.Employees
                      join user in DatabaseContext.Users on employee.LoginUserId equals user.Id
                      join departmentAndManager in DatabaseContext.DepartmentsAndManagers on employee.DepartmentId equals
                          departmentAndManager.Id
                      join departmentAbbreviationName in DatabaseContext.DepartmentAbbreviatedNames on departmentAndManager
                          .DepartmentName equals departmentAbbreviationName.DepartmentName
                      join employeeSetting in DatabaseContext.EmployeeSettings on employee.CustomEmployeeId equals
                          employeeSetting.EmployeeId
                      where employee.IsActive == true && employeeSetting.IsActive == true
                      orderby employee.Id
                      select new EmployeeInfo
                      {
                          HrEmployeeId = employee.Id,
                          EmployeeId = employee.CustomEmployeeId,
                          FirstName = employee.FirstName,
                          LastName = employee.LastName,
                          DepartmentId = employee.DepartmentId,
                          EmployeeEmail = user.Email,
                          DepartmentAbbreviatedNames = departmentAbbreviationName.DeptAbbreviatedName,
                          Department = departmentAndManager.DepartmentName,
                          FullName = employee.LastName + ", " + employee.FirstName,
                          ShiftId = employee.ShiftId != null ? (int)employee.ShiftId : 0,
                          ExpectedClockInTime = employeeSetting.ExpectedClockInTime,
                          ExpectedClockOutTime = employeeSetting.ExpectedClockOutTime,
                          ShiftStartTime = employee.ShiftStartTime,
                          ShiftEndTime = employee.ShiftEndTime,
                          IsActive = employee.IsActive,
                          IsRemotelyWorking = employeeSetting.IsRemotelyWorking
                      }).ToListAsync();
    }

    public async Task<List<EmployeeInfo>> GetJarvisEmployeesNamesAndIdsByActiveStatusAsync(bool isJarvisActive)
    {
        return await (from employee in DatabaseContext.Employees
                      join employeeSetting in DatabaseContext.EmployeeSettings on employee.CustomEmployeeId equals
                          employeeSetting.EmployeeId
                      where employee.IsActive && (!isJarvisActive || employeeSetting.IsActive == true)
                      orderby employee.Id
                      select new EmployeeInfo
                      {
                          HrEmployeeId = employee.Id,
                          EmployeeId = employee.CustomEmployeeId,
                          FirstName = employee.FirstName,
                          LastName = employee.LastName,
                          FullName = employee.LastName + ", " + employee.FirstName,
                          DepartmentId = employee.DepartmentId
                      }).ToListAsync();
    }

    public async Task<List<EmployeeInfo>> GetInActiveEmployeesNamesAndIdsAsync()
    {
        return await (from employee in DatabaseContext.Employees
                      join employeeSetting in DatabaseContext.EmployeeSettings on employee.CustomEmployeeId equals
                          employeeSetting.EmployeeId
                      where employee.IsActive == false
                      orderby employee.Id
                      select new EmployeeInfo
                      {
                          HrEmployeeId = employee.Id,
                          EmployeeId = employee.CustomEmployeeId,
                          FirstName = employee.FirstName,
                          LastName = employee.LastName,
                          FullName = employee.LastName + ", " + employee.FirstName,
                          DepartmentId = employee.DepartmentId
                      }).ToListAsync();
    }

    public async Task<List<EmployeeInfo>> GetActiveEmailEmployeesAsync()
    {
        var departments = await DatabaseContext.DepartmentsAndManagers.Where(x => !x.IsDeleted).ToListAsync();
        var employeeList = await (from employee in DatabaseContext.Employees
                                  join user in DatabaseContext.Users on employee.LoginUserId equals user.Id
                                  where employee.IsActive == true && !String.IsNullOrEmpty(user.Email)
                                  select new EmployeeInfo
                                  {
                                      HrEmployeeId = employee.Id,
                                      EmployeeId = employee.CustomEmployeeId,
                                      FirstName = employee.FirstName,
                                      LastName = employee.LastName,
                                      FullName = employee.LastName + ", " + employee.FirstName,
                                      EmployeeEmail = user.Email,
                                      DepartmentId = employee.DepartmentId
                                  }).ToListAsync();

        foreach (var employee in employeeList)
        {
            var department = departments.Find(x => x.Id == employee.DepartmentId);
            employee.Department = department.DepartmentName;
            employee.DepartmentAbbreviatedNames = department.DepartmentAbbreviation;
            employee.IsManager = departments.Any(x => x.PrimaryManagerEmployeeId == employee.HrEmployeeId);
        }

        return employeeList;

    }

    public async Task<List<EmployeeInfo>> GetAllEmployeeAbbreviatedDepartmentsWithEmployeeIdsAndEmpNamesAsync()
    {
        return await ((from employee in DatabaseContext.Employees
                       join departmentAndManager in DatabaseContext.DepartmentAbbreviatedNames on employee.DepartmentId equals
                           departmentAndManager.Id
                       select new EmployeeInfo
                       {
                           FirstName = employee.FirstName,
                           LastName = employee.LastName,
                           EmployeeId = employee.CustomEmployeeId,
                           DepartmentAbbreviatedNames = departmentAndManager.DeptAbbreviatedName,
                           DepartmentId = departmentAndManager.Id
                       }).ToListAsync());

    }

    public async Task<List<Employee>> GetActiveEmployeesByEmploymentStatusAsync(long employeeStatusId1,
        long employeeStatusId2,
        long employeeStatusId3, long employeeStatusId4)
    {
        return await (from employee in DatabaseContext.Employees
                      where employee.IsDeleted == false && (employee.EmploymentStatusId == employeeStatusId1 ||
                                                            employee.EmploymentStatusId == employeeStatusId2 ||
                                                            employee.EmploymentStatusId == employeeStatusId3 ||
                                                            employee.EmploymentStatusId == employeeStatusId4)
                      select employee
            ).Distinct().ToListAsync();
    }

    public async Task<List<Employee>> GetActiveEmployeesByEmploymentStatusAndStartDateAsync(DateTime startDate,
        long employeeStatusId1,
        long employeeStatusId2, long employeeStatusId3, long employeeStatusId4)
    {
        var employmentHistories = await DatabaseContext.EmploymentHistories.GroupBy(x => new
        {
            x.EmployeeId,
        }).Select(v => new
        {
            History = (from employmentsHistory in DatabaseContext.EmploymentHistories
                       where v.Key.EmployeeId == employmentsHistory.EmployeeId
                       orderby employmentsHistory.DateCreated descending
                       select employmentsHistory).First()
        }).ToListAsync();

        List<Employee> employees = new List<Employee>();
        foreach (var employmentHistory in employmentHistories)
        {
            var latestStatusStartDate = employmentHistory.History.StartDate;

            if (employmentHistory.History.EnumNewEmploymentStatusId == employeeStatusId1 ||
                employmentHistory.History.EnumNewEmploymentStatusId == employeeStatusId2 ||
                employmentHistory.History.EnumNewEmploymentStatusId == employeeStatusId3 ||
                employmentHistory.History.EnumNewEmploymentStatusId == employeeStatusId4)
            {
                if (latestStatusStartDate < startDate.AddDays(7))
                {
                    Employee employee =
                        await DatabaseContext.Employees
                            .Where(a => a.Id == employmentHistory.History.EmployeeId).FirstOrDefaultAsync();
                    if (employee != null)
                    {
                        employees.Add(employee);
                    }
                }
            }
            else
            {
                if (latestStatusStartDate >= startDate)
                {
                    Employee employee = await DatabaseContext.Employees
                        .Where(a => a.Id == employmentHistory.History.EmployeeId).FirstOrDefaultAsync();
                    if (employee != null)
                    {
                        employees.Add(employee);
                    }
                }
            }
        }

        return employees;
    }

    public List<Employee> GetFilteredEmployeesFromEmployeesUsingUserRoleAndCurrentLoggedInEmployeeIdAsync(
        List<Employee> employees, string currentLoggedInEmployeeRole,
        long currentLoggedInEmployeeId)
    {
        List<Employee> filteredEmployees = new List<Employee>();

        if (currentLoggedInEmployeeRole == "HUMAN RESOURCE MANAGER" ||
            currentLoggedInEmployeeRole == "HUMAN RESOURCE LEADER")
        {
            foreach (var employee in employees)
            {
                var managerId = employee.ManagerEmployeeId ?? 0;
                var leaderId = employee.LeaderEmployeeId ?? 0;
                if (managerId == currentLoggedInEmployeeId || leaderId == currentLoggedInEmployeeId)
                {
                    filteredEmployees.Add(employee);
                }
            }
        }
        else if (currentLoggedInEmployeeRole == "HUMAN RESOURCE NO ACCESS")
        {

        }
        else
        {
            foreach (var employee in employees)
            {
                filteredEmployees.Add(employee);
            }
        }

        return filteredEmployees;

    }

    public async Task<List<Employee>> GetBothActiveAndInactiveEmployeesAsync()
    {
        return await (from employee in DatabaseContext.Employees
                      select employee
            ).Distinct().ToListAsync();
    }

    public async Task<string> GetJobTitleFromEmployeeByEmployeeIdAsync(long? employeeId)
    {
        if (employeeId == null || employeeId.Value <= 0)
        {
            return "Not Found";
        }

        var result = await (from employee in DatabaseContext.Employees
                            join jobDescription in DatabaseContext.JobDescriptions on employee.JobId equals jobDescription.Id
                            where employee.Id == employeeId
                            select jobDescription.Name).FirstOrDefaultAsync();
        return result ?? "Not Found";
    }

    public async Task<string> GetLastAssignedCustomEmployeeIdAsync()
    {
        return await (from employee in DatabaseContext.Employees
                      orderby employee.CustomEmployeeId descending
                      select employee.CustomEmployeeId).FirstOrDefaultAsync();
    }

    public async Task<string> FindEmployeeNameByEmployeeIdAsync(long? loginId)
    {
        if (loginId == null || loginId.Value <= 0)
        {
            return "NOT FOUND";
        }

        var result = await (from employee in DatabaseContext.Employees
                            where employee.Id == loginId
                            select $"{employee.LastName}, {employee.FirstName}").FirstOrDefaultAsync();
        return result ?? "NOT FOUND";
    }

    public async Task<string> FindEmployeeNameByCorrectEmployeeIdAsync(long? employeeId)
    {
        if (employeeId == null || employeeId.Value <= 0)
        {
            return "NOT FOUND";
        }

        var result = await (from employee in DatabaseContext.Employees
                            where employee.Id == employeeId
                            select $"{employee.LastName}, {employee.FirstName}").FirstOrDefaultAsync();
        return result ?? "NOT FOUND";
    }

    public async Task<string> GetFirstInitialAndLastNameFromEmployeeByEmployeeIdAsync(long? employeeId)
    {
        if (employeeId == null || employeeId.Value <= 0)
        {
            return "Not Found";
        }

        var result = await (from employee in DatabaseContext.Employees
                            where employee.Id == employeeId
                            select $"{employee.FirstName[0]}. {employee.LastName}").FirstOrDefaultAsync();
        return result ?? "Not Found";
    }

    public async Task<string> GetFirstInitialAndLastNameFromEmployeeByCorrectEmployeeIdAsync(long? employeeId)
    {
        if (employeeId == null || employeeId.Value <= 0)
        {
            return "Not Found";
        }

        var result = await (from employee in DatabaseContext.Employees
                            where employee.Id == employeeId
                            select $"{employee.FirstName[0]}. {employee.LastName}").FirstOrDefaultAsync();
        return result ?? "Not Found";
    }

    public async Task<bool> GetIsExistUserNameFromEmployeeByFirstNameAndLastNameAsync(string firstName,
        string lastName)
    {
        return await DatabaseContext.Employees.AnyAsync(a => a.FirstName == firstName && a.LastName == lastName);
    }

    public async Task<string> FindEmployeeNameByCustomEmployeeIdAsync(string customEmployeeId)
    {
        var result = await (from employee in DatabaseContext.Employees
                            where employee.CustomEmployeeId == customEmployeeId
                            select employee).FirstOrDefaultAsync();

        if (result != null)
        {
            return (result.LastName + ", " + result.FirstName).ToUpper();
        }

        return "NOT FOUND";
    }

    public async Task<IEnumerable<Employee>> GetListAsync(bool isActive, bool isDeleted)
    {
        return await DatabaseContext.Employees
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetListAsync(bool isActive)
    {
        return await DatabaseContext.Employees.Where(x => x.IsActive == isActive)
            .OrderBy(x => x.LastName).AsSplitQuery().ToListAsync();
    }

    public async Task<List<long>> GetListAsync(bool isActive, bool isDelete, List<string> customEmployeeId)
    {
        return await DatabaseContext.Employees
            .Where(x => x.IsActive == isActive
                        && x.IsDeleted == isDelete
                        && customEmployeeId.Contains(x.CustomEmployeeId))
            .Select(x => x.DepartmentId)
            .OrderBy(departmentId => departmentId)
            .ToListAsync();
    }

    public IEnumerable<Employee> GetList(bool isActive)
    {
        return DatabaseContext.Employees.Where(x => x.IsActive == isActive)
            .OrderBy(x => x.LastName).AsSplitQuery().ToList();
    }

    public async Task<Employee> GetByName(string name)
    {
        return await DatabaseContext.Employees
            .FirstOrDefaultAsync(x => (x.LastName.ToUpper() + ", " + x.FirstName) == name && x.IsActive == true) ?? new Employee();
    }

    public async Task<Employee> GetByCustomEmployeeIdAsync(string customEmployeeId)
    {
        return await DatabaseContext.Employees
            .FirstOrDefaultAsync(x => x.CustomEmployeeId == customEmployeeId) ?? new Employee();
    }

    public Employee GetByNameSync(string name)
    {
        return DatabaseContext.Employees
            .FirstOrDefault(x => (x.LastName + ", " + x.FirstName) == name && x.IsActive == true) ?? new Employee();
    }

    public IEnumerable<Employee> FindSurveillance(Expression<Func<Employee, bool>> predicate)
    {
        return DatabaseContext.Employees.Where(predicate);
    }

    public async Task<Employee> GetByEmployeeIdAsync(long employeeId)
    {
        return await DatabaseContext.Employees
            .FirstOrDefaultAsync(x => x.Id == employeeId && x.IsActive == true) ?? new Employee();
    }

    public async Task<Employee> GetByJobIdAsync(long jobId)
    {
        return await DatabaseContext.Employees
            .FirstOrDefaultAsync(x => x.JobId == jobId && x.IsActive == true)?? new Employee();
    }

    public async Task<Employee> GetByIdAsync(long id)
    {
        return await DatabaseContext.Employees
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true) ?? new Employee();
    }

    public async Task<IEnumerable<Employee>> GetListAsync(int skip, int limit)
    {
        return await DatabaseContext.Employees.Skip(skip).Take(limit).AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> CountSurveillanceAsync()
    {
        return await DatabaseContext.Employees.CountAsync();
    }

    public virtual async Task<int> CountSurveillanceAsync(string search)
    {
        return await DatabaseContext.Employees.CountAsync();
    }

    public virtual async Task<IEnumerable<Employee>> SearchAsync(int skip, int limit)
    {
        return await DatabaseContext.Employees.Skip(skip).Take(limit).AsNoTracking()
            .ToListAsync();
    }

    public IEnumerable<Employee> GetList()
    {
        return DatabaseContext.Employees.AsNoTracking().ToList();
    }

    public IEnumerable<Employee> GetList(int skip, int limit)
    {
        return DatabaseContext.Employees
            .AsNoTracking()
            .Skip(skip)
            .Take(limit).ToList();
    }

    public virtual async Task<IEnumerable<Employee>> SearchAsync(Func<Employee, bool> conditions)
    {
        var result = DatabaseContext.Employees.AsNoTracking().Where(conditions).ToList();
        return await Task.FromResult(result);
    }

    public virtual async Task<IEnumerable<Employee>> SearchSurveillanceAsync(Func<Employee, bool> conditions)
    {
        var result = DatabaseContext.Employees.AsNoTracking()
            .Where(conditions);
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(bool isDeleted, bool withoutSubcontractor = false)
    {
        return await DatabaseContext.Employees
            .Where(x => x.IsActive == true && (withoutSubcontractor == false || x.IsSubContractor != true))
            .OrderBy(x => x.LastName + ", " + x.FirstName).ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(List<long> ids)
    {
        return await DatabaseContext.Employees
            .Where(x => ids.Contains(x.Id) && x.IsActive == true).OrderBy(x => x.LastName + ", " + x.FirstName)
            .AsSplitQuery().ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await DatabaseContext.Employees
            .Where(x => x.IsActive == true).OrderBy(x => x.LastName + ", " + x.FirstName).AsSplitQuery()
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(int skip, int limit)
    {
        return await DatabaseContext.Employees.AsNoTracking()
            .Skip(skip).Take(limit).ToListAsync();
    }

    public IEnumerable<Employee> GetAll()
    {
        return DatabaseContext.Employees.AsNoTracking()
            .ToList();
    }

    public IEnumerable<Employee> GetAll(int skip, int limit)
    {
        return DatabaseContext.Employees.AsNoTracking()
            .Skip(skip).Take(limit).ToList();
    }

    public async Task<List<EmployeeViewModel>> GetAllActiveEmployeesAsync()
    {
        var departments = await DatabaseContext.DepartmentsAndManagers
            .Where(x => !x.IsDeleted)
            .AsNoTracking()
            .ToListAsync();

        var employees = await (from employee in DatabaseContext.Employees.AsNoTracking()
                               join user in DatabaseContext.Users.AsNoTracking()
                                   on employee.LoginUserId equals user.Id
                               where employee.IsDeleted == false && employee.IsActive == true
                               select new EmployeeViewModel
                               {
                                   HrEmployeeId = employee.LoginUserId,
                                   CustomEmployeeId = employee.CustomEmployeeId,
                                   FullName = employee.FullName,
                                   FirstName = employee.FirstName,
                                   LastName = employee.LastName,
                                   DepartmentId = employee.DepartmentId,
                                   EmployeeEmail = user.NormalizedEmail,
                                   IsActive = employee.IsActive,
                               }).ToListAsync();

        foreach (var employee in employees)
        {
            var department = departments.Find(x => x.Id == employee.DepartmentId);
            if (department != null)
            {
                employee.DepartmentName = department.DepartmentName;
                employee.DepartmentAbbreviatedName = department.DepartmentAbbreviation;
                //employee.IsManager = departments.Any(x => x.PrimaryManagerEmployeeId == employee.HrEmployeeId);
            }
        }

        return employees.ToList();
    }
}