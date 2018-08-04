using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MySQLRepository.Repository
{
    public interface IMySQLRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string sqlText);
        Task<DataTable> GetByIDUsingSP(long ID);
        Task<T> GetByID(string sqlText);
        Task<bool> Execute(string sqlText);
    }
}
