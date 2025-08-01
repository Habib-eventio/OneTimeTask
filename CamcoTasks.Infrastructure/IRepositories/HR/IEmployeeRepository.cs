// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.Repository.HR;
using System.Linq.Expressions;

namespace CamcoTasks.Infrastructure.Repository.IRepository.HR;

public interface IEmployeeRepository : IRepository<Employee>
{
    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<List<Employee>> GetEmployeeHierarchyAsync(long employeeId);

    /// <summary>
    /// This Method is being used in Jarvis Project
    /// </summary>
    Task<List<EmployeeInfo>> GetActiveJarvisEmployeesListAsync();

    /// <summary>
    /// This Method is being used in Jarvis Project
    /// </summary>
    Task<List<EmployeeInfo>> GetJarvisEmployeesNamesAndIdsByActiveStatusAsync(bool isJarvisActive);

    /// <summary>
    /// This Method is being used in Jarvis Project
    /// </summary>
    Task<List<EmployeeInfo>> GetInActiveEmployeesNamesAndIdsAsync();

    /// <summary>
    /// This Method is being used in Jarvis Project
    /// </summary>
    Task<List<EmployeeInfo>> GetActiveEmailEmployeesAsync();

    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<List<Employee>> GetBothActiveAndInactiveEmployeesAsync();

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<List<EmployeeInfo>> GetAllEmployeeAbbreviatedDepartmentsWithEmployeeIdsAndEmpNamesAsync();

    /// <summary>
    /// This Method is being used in Metrics, HumanResource, AutomatedSystemService, FirstArticleDb, ITBlazor Project
    /// </summary>
    Task<List<Employee>> GetActiveEmployeesByEmploymentStatusAsync(long employeeStatusId1, long employeeStatusId2,
        long employeeStatusId3, long employeeStatusId4);

    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<string> GetJobTitleFromEmployeeByEmployeeIdAsync(long? employeeId);

    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<List<Employee>> GetActiveEmployeesByEmploymentStatusAndStartDateAsync(DateTime startDate,
        long employeeStatusId1, long employeeStatusId2, long employeeStatusId3, long employeeStatusId4);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    List<Employee> GetFilteredEmployeesFromEmployeesUsingUserRoleAndCurrentLoggedInEmployeeIdAsync(
        List<Employee> employees, string currentLoggedInEmployeeRole, long currentLoggedInEmployeeId);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<string> GetLastAssignedCustomEmployeeIdAsync();

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<bool> GetIsExistUserNameFromEmployeeByFirstNameAndLastNameAsync(string firstName, string lastName);

    /// <summary>
    /// This Method is being used in Metrics, Production, Stockroom, HumanResource Project
    /// </summary>
    Task<string> FindEmployeeNameByEmployeeIdAsync(long? loginId);

    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<string> FindEmployeeNameByCorrectEmployeeIdAsync(long? employeeId);

    /// <summary>
    /// This Method is being used in Stockroom Project
    /// </summary>
    Task<string> GetFirstInitialAndLastNameFromEmployeeByEmployeeIdAsync(long? employeeId);

    /// <summary>
    /// This Method is being used in Metrics Project
    /// </summary>
    Task<string> GetFirstInitialAndLastNameFromEmployeeByCorrectEmployeeIdAsync(long? employeeId);

    /// <summary>
    /// This Method is being used in AutomatedSystemService, FirstArticleDb Project
    /// </summary>
    Task<string> FindEmployeeNameByCustomEmployeeIdAsync(string customEmployeeId);

    /// <summary>
    /// This Method is being used in Task, ITBlazor, AutomatedSystemService Project
    /// </summary>
    Task<Employee> GetByIdAsync(long employeeId);

    /// <summary>
    /// This Method is being used in Task, Stockroom Project
    /// </summary>
    Task<Employee> GetByEmployeeIdAsync(long employeeId);

    /// <summary>
    /// This Method is being used in Task, ITBlazor Project
    /// </summary>
    Task<Employee> GetByJobIdAsync(long jobId);

    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    IEnumerable<Employee> GetList();

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    IEnumerable<Employee> GetList(int skip, int limit);

    /// <summary>
    /// This Method is being used in Task, ITBlazor Project
    /// </summary>
    Task<IEnumerable<Employee>> GetListAsync(bool isActive);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<List<long>> GetListAsync(bool isActive, bool isDelete, List<string> customEmployeeId);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    IEnumerable<Employee> GetList(bool isActive);

    /// <summary>
    /// This Method is being used in AutomatedSystemService Project
    /// </summary>
    Task<IEnumerable<Employee>> GetListAsync(bool isActive, bool isDeleted);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<IEnumerable<Employee>> GetListAsync(int skip, int limit);

    /// <summary>
    /// This Method is being used in Surveillance Project
    /// </summary>
    Task<IEnumerable<Employee>> SearchAsync(Func<Employee, bool> p);


    /// <summary>
    /// This Method is being used in Task, ITBlazor, AutomatedSystemService Project
    /// </summary>
    Task<Employee> GetByName(string name);

    /// <summary>
    /// This Method is being used in Task, ITBlazor Project
    /// </summary>
    Task<Employee> GetByCustomEmployeeIdAsync(string customEmployeeId);

    /// <summary>
    /// This Method is being used in Task, ITBlazor Project
    /// </summary>
    Employee GetByNameSync(string name);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<IEnumerable<Employee>> GetAllAsync(bool isDeleted, bool withoutSubcontractor = false);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<IEnumerable<Employee>> GetAllAsync(List<long> ids);

    /// <summary>
    /// This Method is being used in Surveillance Project
    /// </summary>
    IEnumerable<Employee> GetAll();

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    IEnumerable<Employee> GetAll(int skip, int limit);

    /// <summary>
    /// This Method is being used in Costing, Surveillance, AutomatedSystemService Project
    /// </summary>
    Task<IEnumerable<Employee>> GetAllAsync();

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    Task<IEnumerable<Employee>> GetAllAsync(int skip, int limit);

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
    IEnumerable<Employee> FindSurveillance(Expression<Func<Employee, bool>> predicate);

    /// <summary>
    /// This Method is being used in Quality Project
    /// </summary>
    Task<List<EmployeeViewModel>> GetAllActiveEmployeesAsync();

    /// <summary>
    /// This Method is being used in Costing Project
    /// </summary>
    Task<float> GetEmployeeRateAsync(string employeeName, int? weekNumber, DateTime? date);
}