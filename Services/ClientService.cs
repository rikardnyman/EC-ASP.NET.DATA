using Data.Dtos;
using Data.Repositories;

namespace Data.Services
{
    public interface IClientService
    {
        Task<ServiceResult<Client>> GetClientsAsync();
    }
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ServiceResult<Client>> GetClientsAsync()
        {
            var entities = await _clientRepository.GetAllAsync();

            var dtos = entities.Select(client => new Client
            {
                Id = client.Id,
                ClientName = client.ClientName
            }).ToList();

            return ServiceResult<Client>.Success(dtos);
        }
    }

}
