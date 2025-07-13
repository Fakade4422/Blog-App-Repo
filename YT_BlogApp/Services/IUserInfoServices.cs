namespace YT_BlogApp.Services
{
    public interface IUserInfoServices
    {
        int GetLoggedInUser();

        Task<string> GetFullName();
        Task<string> GetPicture();

        Task<string> GetRole();
    }
}
