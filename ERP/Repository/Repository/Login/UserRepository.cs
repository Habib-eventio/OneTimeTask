using ERP.Data.Entities.Login;
using ERP.Repository.IRepository.Login;
using CamcoTasks.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repository.Repository.Login;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context) { }

    public Task<User?> FindAsync(System.Linq.Expressions.Expression<System.Func<User, bool>> predicate) =>
        base.FindAsync(predicate);

    public Task<List<User>> GetListAsync() => base.GetListAsync();

    public Task<User?> GetAsync(long id) => base.GetAsync(id);
}
