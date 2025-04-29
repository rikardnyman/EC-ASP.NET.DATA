using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Client { get; set; } = null!;
        public bool IsValid => !string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);

        public virtual ICollection<ProjectEntity> Projects { get; set; } = [];

    }
}
