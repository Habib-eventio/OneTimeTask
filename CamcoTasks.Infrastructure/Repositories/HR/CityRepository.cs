// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace CamcoTasks.Infrastructure.Repository;

public class CityRepository : Repository<City>,
	ICityRepository
{
	public CityRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<bool> GetIsExistFromCityByCityIdAsync(long cityId)
	{
		return await (from jobCity in DatabaseContext.Cities
			where jobCity.Id == cityId
			select jobCity).AnyAsync();
	}

	public async Task<string> GetNameFromCityByCityIdAsync(long cityId)
	{
		var cityName = await (from city in DatabaseContext.Cities
			where city.Id == cityId
			select city.Name).FirstOrDefaultAsync();
		return cityName ?? "NOT FOUND";
	}
}