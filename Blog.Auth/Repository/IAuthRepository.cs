using Blog.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Auth.Repository
{
    public interface IAuthRepository
    {
        Task<User> ValidateUser(Login login);
        Task<User> GetLoggedInUser(int id);
        Task<User> FindByEmail(string email);
        Task<bool> ResetPassword(string resetPassword, int UserID);
    }
}
