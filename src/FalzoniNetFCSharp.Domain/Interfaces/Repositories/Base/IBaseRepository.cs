using System.Collections.Generic;
using System;

namespace FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T obj);

        void Update(T obj);

        void Delete(Guid Id);

        T Get(Guid id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllPredicate(Func<T, bool> predicate);
    }
}
