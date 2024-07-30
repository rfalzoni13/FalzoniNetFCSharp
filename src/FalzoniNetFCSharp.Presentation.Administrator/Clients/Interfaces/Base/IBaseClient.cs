using System.Collections.Generic;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base
{
    public interface IBaseClient<T, TTable>
        where T : class
        where TTable : class
    {
        T Get(string url, string id);
        Task<T> GetAsync(string url, string id);
        ICollection<T> GetAll(string url);
        Task<ICollection<T>> GetAllAsync(string url);
        TTable GetTable(string url);
        Task<TTable> GetTableAsync(string url);
        string Add(string url, T obj);
        Task<string> AddAsync(string url, T obj);
        string Update(string url, T obj);
        Task<string> UpdateAsync(string url, T obj);
        string Delete(string url, T obj);
        Task<string> DeleteAsync(string url, T obj);
    }
}
