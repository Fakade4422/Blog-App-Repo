using Blog.Auth.Model;
using Blog.Auth.Repository;
using System.Security.Claims;

namespace YT_BlogApp.Services
{
    public class UserInfoServices:IUserInfoServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthRepository _repo;

        public UserInfoServices(IHttpContextAccessor httpContextAccessor, IAuthRepository repo)
        {
            _httpContextAccessor = httpContextAccessor;
            _repo = repo;
        }

        public int GetLoggedInUser()
        {
            var val = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(val);
        }

        public async Task<string> GetPicture()
        {
            User user = await _repo.GetLoggedInUser(GetLoggedInUser());
            string imageBase64 = Convert.ToBase64String(user.Picture);
            return imageBase64;
        }

        public async Task<string> GetRole()
        {
            User user = await _repo.GetLoggedInUser(GetLoggedInUser());
            string role = user.Role;
            return role;
        }

        public async Task<string> GetFullName()
        {
            User user = await _repo.GetLoggedInUser(GetLoggedInUser());
            string userFullName = $"{user.Name} {user.Surname}";
            return userFullName;
        }

    }
}
