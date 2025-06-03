using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DatabaseAccess.SqlDataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connString = "conn");
        Task SaveData<T>(string spName, T parameters, string connString = "conn");
        Task<int> GetSingleValue<T, P>(string spName, P parameters, string connString = "conn");
    }
}
