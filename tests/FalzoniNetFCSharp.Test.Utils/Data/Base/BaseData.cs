using FalzoniNetFCSharp.Domain.Entities.Base;
using System;
using System.Linq;

namespace FalzoniNetFCSharp.Test.Utils.Data.Base
{
    public abstract class BaseData<TEntity> where TEntity : BaseEntity, new()
    {
        public abstract IQueryable<TEntity> GetData();

        public abstract TEntity CreateObject();

        public abstract TEntity UpdateObject(TEntity obj);

        public abstract TEntity GetItem(Guid id);

        public abstract Guid GetGuidFromList();

        public abstract Guid GetGuidWithoutInclude();
    }
}
