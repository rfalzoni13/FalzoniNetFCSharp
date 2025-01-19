using FalzoniNetFCSharp.Domain.Entities.Base;
using FalzoniNetFCSharp.Infra.Data.Context;
using FalzoniNetFCSharp.Test.Utils.Data.Base;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FalzoniNetFCSharp.Infra.Data.Test.Mock.Context
{
    public class MockContextBuilder<TEntity> where TEntity : BaseEntity, new()
    {
        private static Mock<FalzoniContext> _mockContext;

        public static Mock<FalzoniContext> GetContext(BaseData<TEntity> data)
        {
            _mockContext = _mockContext ?? new Mock<FalzoniContext>() 
            {
                CallBase = true
            };

            Mock<MockableDbSet<TEntity>> mockSet = new Mock<MockableDbSet<TEntity>>();
            IQueryable<TEntity> queryData = data.GetData();

            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryData.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryData.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryData.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryData.GetEnumerator());

            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);

            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] id) => data.GetItem((Guid)id.First()));

            _mockContext.Setup(m => m.Set<TEntity>()).Returns(mockSet.Object);
            _mockContext.Setup(m => m.Update(It.IsAny<TEntity>()));
            _mockContext.Setup(m => m.SaveChanges()).Returns(1);

            return _mockContext;
        }

        public static Mock<FalzoniContext> Dispose()
        {
            _mockContext.Object.Dispose();
            return _mockContext;
        }
    }

    public abstract class MockableDbSet<TEntity> : DbSet<TEntity> where TEntity : class
    {
        public abstract void AddOrUpdate(params TEntity[] entities);
        public abstract void AddOrUpdate(Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities);
    }
}
