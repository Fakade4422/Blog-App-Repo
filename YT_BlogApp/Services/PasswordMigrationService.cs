using BCrypt.Net;
using Blog.Administrator.Models;
using Blog.Auth.Model;
using Blog_DatabaseAccess.SqlDataAccess;
using Dapper;
using System.Data;
using System.Data.Common;

namespace YT_BlogApp.Services
{
    public class PasswordMigrationService
    {
        private readonly ISqlDataAccess _db;
        private readonly ILogger<PasswordMigrationService> _logger;
        public PasswordMigrationService(ISqlDataAccess db, ILogger<PasswordMigrationService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task HashExistingPasswords()
        {
            _logger.LogInformation("Starting password migration...");

            

            int updatedCounts = 0;
            IEnumerable<User> users = await _db.GetData<User, dynamic>("sp_GetUsers_forHash", new { });

            foreach (var user in users)
            {
                try
                {
                    if ((!string.IsNullOrEmpty(user.Password)) &&
                    !user.Password.StartsWith("$2a$") &&
                    !user.Password.StartsWith("$2b$") &&
                    !user.Password.StartsWith("$2y$"))
                    {
                        string hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                        const string updatePassword = "UPDATE User_tbl SET Password = @Password WHERE UserID = @Id";
                        await _db.SaveData(updatePassword, new { UserID = user.UserID, Password = hashPassword });
                        //const string updateQuery = "UPDATE Users SET Password = @Password WHERE Id = @Id";
                        //await _dbConnection.ExecuteAsync(updateQuery, new { Id = user.Id, Password = hashedPassword });
                    }//end of If
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error hashing password for UserID: {user.UserID}");
                }


            }//end of foreach

            _logger.LogInformation($"Password migration completed. Updated {updatedCounts} passwords.");

        }


    }
}
