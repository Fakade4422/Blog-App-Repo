using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DatabaseAccess.SqlDataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connString = "conn")
        {
            using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connString));
            return await conn.QueryAsync<T>(spName,parameters,commandType:CommandType.StoredProcedure);
        }

        public async Task<int> GetSingleValue<T, P>(string spName, P parameters, string connString = "conn")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connString));
            return await connection.QuerySingleOrDefaultAsync<int>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string spName, T parameters, string connString = "conn")
        {
            using IDbConnection conn = new SqlConnection(_config.GetConnectionString(connString));
            await conn.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
