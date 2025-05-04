using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<ClientEntity>> GetAllAsync();
        Task<ClientEntity?> GetByClientNameAsync(string clientName);
        Task<bool> ExistsByClientNameAsync(string clientName);
        Task<bool> AddAsync(ClientEntity entity);
    }

    public class ClientRepository : BaseRepository<ClientEntity>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }

        
        public async Task<ClientEntity?> GetByClientNameAsync(string clientName)
        {
            return await _table.FirstOrDefaultAsync(c => c.ClientName == clientName);
        }

        
        public async Task<bool> ExistsByClientNameAsync(string clientName)
        {
            return await _table.AnyAsync(c => c.ClientName == clientName);
        }
    }
}

