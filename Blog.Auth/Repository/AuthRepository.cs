using Blog.Auth.Model;
using Blog_DatabaseAccess.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Auth.Repository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly ISqlDataAccess _db;
        public AuthRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<User> ValidateUser(Login login)
        {
            try
            {
                IEnumerable<User> user = await _db.GetData<User, dynamic>("sp_ValidateUser", new
                {
                    login.Email,
                    login.Password

                });

                return user.FirstOrDefault();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<User> GetLoggedInUser(int id)
        {
            try
            {
                IEnumerable<User> user = await _db.GetData<User, dynamic>("sp_GetLoggedInUser", new
                {
                    EmployeeID = id
                });


                return user.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }


        public async Task<User> FindByEmail(string email)
        {
            try
            {
                IEnumerable<User> user = await _db.GetData<User, dynamic>("sp_FindByEmail", new
                {
                    EmailAddress = email
                });


                return user.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> ResetPassword(string resetPassword, int UserID)
        {
            try
            {
                await _db.SaveData("sp_ResetPassword", new
                {
                    resetPassword,
                    UserID
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserPassword(User user)
        {
            try
            {
                await _db.SaveData("sp_UpdatePasswordHASH", new 
                {   user.UserID, 
                    user.Password 
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
