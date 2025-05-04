using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<StatusEntity>> GetAllAsync();
        Task<bool> AddAsync(StatusEntity entity);
        Task<bool> UpdateAsync(StatusEntity entity);
        Task<StatusEntity?> GetByProjectStatusAsync(string statusName);
    }
    public class StatusRepository : BaseRepository<StatusEntity>, IStatusRepository
    {
        
        public StatusRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<StatusEntity?> GetByProjectStatusAsync(string statusName)
        {
            return await _table.FirstOrDefaultAsync(c => c.StatusName == statusName);
        }
    }
}
