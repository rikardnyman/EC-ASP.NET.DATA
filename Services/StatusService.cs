using Data.Dtos;
using Data.Repositories;

namespace Data.Services
{
    public interface IStatusService
    {
        Task<ServiceResult<Status>> GetStatusesAsync();
    }
    public class StatusService : IStatusService
    {



        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<ServiceResult<Status>> GetStatusesAsync()
        {
            var entities = await _statusRepository.GetAllAsync();

            var dtos = entities.Select(status => new Status
            {
                Id = status.Id,
                StatusName = status.StatusName
            }).ToList();

            return ServiceResult<Status>.Success(dtos);
        }
    }
}
