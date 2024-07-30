using FalzoniNetFCSharp.Domain.Interfaces.Base;
using FalzoniNetFCSharp.Infra.Data.Context;
using FalzoniNetFCSharp.Infra.Data.Context.MySql;
using FalzoniNetFCSharp.Infra.Data.Context.SqlServer;
using FalzoniNetFCSharp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Base
{
    public abstract class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        protected DbContext context { get; private set; }

        public BaseRepository()
        {
            switch (ConfigurationHelper.ProviderName)
            {
                case "SqlServer":
                    context = new FalzoniSqlServerContext();
                    break;

                case "MySql":
                    context = new FalzoniMySqlContext();
                    break;

                default:
                    throw new System.Exception("Erro ao definir provider");
            }
        }


        public BaseRepository(FalzoniContext falzoniContext)
        {
            context = falzoniContext;
        }
        public virtual void Add(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public virtual void Update(T obj)
        {
            context.Set<T>().Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public virtual void Delete(Guid Id)
        {
            var obj = context.Set<T>().Find(Id);
            if (obj != null)
            {
                context.Set<T>().Remove(obj);
                context.SaveChanges();
            }
        }

        public virtual T Get(Guid id) => context.Set<T>().Find(id);

        public virtual IEnumerable<T> GetAll() => context.Set<T>().ToList();

        public virtual IEnumerable<T> GetAllPredicate(Func<T, bool> predicate)
            => context.Set<T>().Where(predicate);

        //Opcional - utilizar caso o processo precise de um escopo único (kill)
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
