using Data.Dtos;
using Data.Entities;


namespace Data.Services
{
    public interface IUserService
    {
        Task<ServiceResult<User>> GetUsersAsync();
        Task<ServiceResult<User>> RegisterUserAsync(SignUpFormData dto);
        Task<ServiceResult<User>> LoginUserAsync(SignInFormData dto);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<User>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var dtos = users.Select(user => new User
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            }).ToList();

            return ServiceResult<User>.Success(dtos);
        }

        public async Task<ServiceResult<User>> RegisterUserAsync(SignUpFormData dto)
        {
            var exists = await _userRepository.ExistsAsync(u => u.Email == dto.Email);
            if (exists == true)
                return ServiceResult<User>.Fail("Email already exists", 409);

            var userEntity = new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password
            };

            var success = await _userRepository.AddAsync(userEntity);
            if (!success)
                return ServiceResult<User>.Fail("Failed to create user", 500);

            return ServiceResult<User>.Success(new List<User>
        {
            new User
            {
                Id = userEntity.Id,
                FullName = userEntity.FullName,
                Email = userEntity.Email
            }
        });
        }

        public async Task<ServiceResult<User>> LoginUserAsync(SignInFormData dto)
        {
            var user = await _userRepository.GetAsync(u => u.Email == dto.Email);

            if (user == null || user.Password != dto.Password)
                return ServiceResult<User>.Fail("Invalid email or password", 401);

            return ServiceResult<User>.Success(new List<User>
        {
            new User
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            }
        });
        }
    }

}
