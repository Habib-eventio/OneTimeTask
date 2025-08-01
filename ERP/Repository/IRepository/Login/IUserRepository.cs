using ERP.Data.Entities.Login;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ERP.Repository.IRepository.Login;

public interface IUserRepository
{
    Task<User?> FindAsync(System.Linq.Expressions.Expression<System.Func<User, bool>> predicate);
    Task<List<User>> GetListAsync();
    Task<User?> GetAsync(long id);
}
