using System;
using System.Collections.Generic;

namespace FalzoniNetFCSharp.Service.Base
{
    public abstract class ServiceBase<T>
        where T : class
    {
        public abstract void Add(T obj);

        public abstract void Delete(Guid Id);

        public abstract void Update(T obj);

        public abstract T Get(Guid id);

        public abstract IEnumerable<T> GetAll();
    }
}
