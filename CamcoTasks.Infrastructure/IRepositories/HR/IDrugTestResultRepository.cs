//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.UnitOfWork;

//namespace ERP.Repository.IRepository.HR;

//public interface IDrugTestResultRepository : IRepository<DrugTestResult>
//{
//    /// <summary>
//    /// This Method is being used in HumanResource Project
//    /// </summary>
//    Task<DateTime?> GetLastTestDateFromDrugTestResultByEmployeeIdAsync(long employeeId);

//    /// <summary>
//    /// This Method is being used in Human Resource Project
//    /// </summary>
//    Task<DrugTestResult> GetLatestDrugTestResultByEmployeeIdAsync(long employeeId);

//	/// <summary>
//	/// This Method is being used in HumanResource Project
//	/// </summary>
//	Task<List<DrugTestResult>> GetDrugTestResultsByActiveEmployees(bool isOnlyActiveEmployees);
//}