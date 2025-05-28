//using Data.Dtos;
//using Data.Entities;
//using Microsoft.AspNetCore.Identity;

//namespace Data.Services
//{
//    public interface IAuthService
//    {
//        Task<ServiceResult<bool>> SignInAsync(SignInFormData formData);
//        Task<ServiceResult<User>> SignUpAsync(SignUpFormData formData);
//    }
//    public class AuthService : IAuthService
//    {
//        private readonly IUserService _userService;
//        private readonly SignInManager<UserEntity> _signInManager;

//        public AuthService(IUserService userService, SignInManager<UserEntity> signInManager)
//        {
//            _userService = userService;
//            _signInManager = signInManager;
//        }

//        public async Task<ServiceResult<bool>> SignInAsync(SignInFormData formData)
//        {
//            if (formData == null)
//                return ServiceResult<bool>.Fail("Invalid form data");

//            var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, false);

//            return result.Succeeded
//                ? ServiceResult<bool>.Success(new List<bool> { true })
//                : ServiceResult<bool>.Fail("Invalid login credentials");
//        }

//        public async Task<ServiceResult<User>> SignUpAsync(SignUpFormData formData)
//        {
//            if (formData is null || string.IsNullOrWhiteSpace(formData.Email) || string.IsNullOrWhiteSpace(formData.Password) || string.IsNullOrWhiteSpace(formData.FullName))
//                return ServiceResult<User>.Fail("Not all required fields are filled");

//            var result = await _userService.RegisterUserAsync(formData);

//            if (result.Succeeded)
//                return ServiceResult<User>.Success(result.Result!);
//            else
//                return ServiceResult<User>.Fail(result.Error ?? "Failed to register user");
//        }
//    }


//}
