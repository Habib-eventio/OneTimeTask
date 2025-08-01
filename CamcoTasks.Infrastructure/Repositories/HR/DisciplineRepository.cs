//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using CamcoTasks.Infrastructure.Data;
//using CamcoTasks.Infrastructure.Entities;
//using CamcoTasks.Infrastructure.Entities.HR;
//using CamcoTasks.Infrastructure.IRepositories.HR;
//using Microsoft.Data.SqlClient;
//using System.Data;
//using System.Data.Common;

//namespace CamcoTasks.Infrastructure.Repository.HR;

//public class DisciplineRepository : Repository<Discipline>,
//	IDisciplineRepository
//{
//	public DisciplineRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public SpDisciplinesModel GetDisciplinesByEmployeeActiveStatus(bool isInactiveEmployeesAdded)
//	{
//		object[] parameters =
//		{
//			new SqlParameter
//			{
//				ParameterName = "@IsInactiveEmployeesAdded",
//				SqlDbType = SqlDbType.Bit,
//				Value = isInactiveEmployeesAdded
//			}
//		};
//		return ExecuteReader(InspectionsCountDataMapper, "[dbo].[sp_HR_GetDisciplines]", parameters);
//	}

//	public SpDisciplinesModel GetDisciplinesByEmployeeActiveStatusAndDisciplineId(long disciplineId,
//		bool isInactiveEmployeesAdded)
//	{
//		object[] parameters =
//		{
//			new SqlParameter
//			{
//				ParameterName = "@IsInactiveEmployeesAdded",
//				SqlDbType = SqlDbType.Bit,
//				Value = isInactiveEmployeesAdded
//			},
//			new SqlParameter
//			{
//				ParameterName = "@DisciplineId",
//				SqlDbType = SqlDbType.BigInt,
//				Value = disciplineId
//			}
//		};
//		return ExecuteReader(InspectionsCountDataMapper, "[dbo].[sp_HR_GetDisciplinesByDisciplineId]", parameters);
//	}

//	public SpDisciplinesModel GetDisciplinesByEmployeeActiveStatusAndDisciplineIdAndEmployeeId(long employeeId,
//		long disciplineId, bool isInactiveEmployeesAdded)
//	{
//		object[] parameters =
//		{
//			new SqlParameter
//			{
//				ParameterName = "@IsInactiveEmployeesAdded",
//				SqlDbType = SqlDbType.Bit,
//				Value = isInactiveEmployeesAdded
//			},
//			new SqlParameter
//			{
//				ParameterName = "@DisciplineId",
//				SqlDbType = SqlDbType.BigInt,
//				Value = disciplineId
//			},
//			new SqlParameter
//			{
//				ParameterName = "@UserId",
//				SqlDbType = SqlDbType.BigInt,
//				Value = employeeId
//			}
//		};
//		return ExecuteReader(InspectionsCountDataMapper, "[dbo].[sp_HR_GetDisciplinesByDisciplineIdAndUserId]",
//			parameters);
//	}

//	protected virtual T ExecuteReader<T>(Func<DbDataReader, T> mapEntities, string exec, params object[] parameters)
//	{
//		using var conn = new SqlConnection(DatabaseContext.Database.GetConnectionString());
//		using var command = new SqlCommand(exec, conn);
//		conn.Open();
//		command.Parameters.AddRange(parameters);
//		command.CommandType = CommandType.StoredProcedure;
//		try
//		{
//			using var reader = command.ExecuteReader();
//			T data = mapEntities(reader);
//			return data;
//		}
//		finally
//		{
//			conn.Close();
//		}
//	}

//	private SpDisciplinesModel InspectionsCountDataMapper(DbDataReader reader)
//	{
//		var result = new SpDisciplinesModel
//		{
//			DisciplineDetails = reader.Translate<SpDisciplineDetailsModel>()
//		};

//		return result;
//	}
//}