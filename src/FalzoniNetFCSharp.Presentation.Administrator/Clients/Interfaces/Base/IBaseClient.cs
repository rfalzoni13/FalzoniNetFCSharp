using System.Collections.Generic;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base
{
    public interface IBaseClient<T>
        where T : class
    {
        T Get(string id);
        Task<T> GetAsync(string id);
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        string Add(T obj);
        Task<string> AddAsync(T obj);
        string Update(T obj);
        Task<string> UpdateAsync(T obj);
        string Delete(T obj);
        Task<string> DeleteAsync(T obj);
    }
}
