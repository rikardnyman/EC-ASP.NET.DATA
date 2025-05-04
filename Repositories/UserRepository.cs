using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using System.Linq.Expressions;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllAsync();
    Task<UserEntity?> GetAsync(Expression<Func<UserEntity, bool>> predicate);
    Task<bool?> ExistsAsync(Expression<Func<UserEntity, bool>> predicate);
    Task<bool> AddAsync(UserEntity entity);
}

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }
}
