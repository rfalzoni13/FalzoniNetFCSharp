using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FalzoniNetFCSharp.Domain.Entities.Base;
using System.Collections.Generic;
using System.Linq;
using FalzoniNetFCSharp.Infra.Data.Context;
using Moq;
using FalzoniNetFCSharp.Test.Utils.Data.Base;
using FalzoniNetFCSharp.Infra.Data.Test.Mock.Context;

namespace FalzoniNetFCSharp.Infra.Data.Test.Repositories.Base
{
    public abstract class BaseRepositoryTest<T> where T : BaseEntity, new()
    {
        private IBaseRepository<T> _repository;
        protected static Mock<FalzoniContext> context;
        protected static BaseData<T> data;

        #region Test Class configuration
        private TestContext _testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }
        #endregion

        [TestMethod(displayName: "Should be return get success")]
        public virtual void Test_Get_Success()
        {
            Guid id = data.GetGuidFromList();

            T obj = _repository.Get(id);

            Assert.IsNotNull(obj);
            Assert.AreEqual(id, obj.Id);
        }

        [TestMethod(displayName: "Should be return get empty")]
        public virtual void Test_Get_Empty()
        {
            Guid id = Guid.Empty;

            T obj = _repository.Get(id);

            Assert.IsNull(obj);
            Assert.AreNotEqual(id, obj?.Id);
        }

        [TestMethod(displayName: "Should be return get all success")]
        public virtual void Test_GetAll_Success()
        {
            IEnumerable<T> list = _repository.GetAll();

            Assert.IsTrue(list.Count() > 0);
            Assert.AreNotEqual(list.Count(), 0);
        }

        [TestMethod(displayName: "Should be return success when calling create method")]
        public virtual void Test_Create_Success()
        {
            T obj = data.CreateObject();

            _repository.Add(obj);
        }

        [TestMethod(displayName: "Should be return failure when calling create method")]
        public virtual void Test_Create_Failure()
        {
            T obj = null;

            _repository.Add(obj);
        }

        [TestMethod(displayName: "Should be return success when calling update method")]
        public virtual void Test_Update_Success()
        {
            Guid id = data.GetGuidFromList();

            T obj = data.GetItem(id);

            obj = data.UpdateObject(obj);

            _repository.Update(obj);
        }

        [TestMethod(displayName: "Should be return failure when calling update method")]
        public virtual void Test_Update_Failure()
        {
            T obj = null;

            _repository.Update(obj);
        }

        [TestMethod(displayName: "Should be return success when calling delete method")]
        public virtual void Test_Delete_Success()
        {
            Guid id = data.GetGuidFromList();

            _repository.Delete(id);
        }

        [TestMethod(displayName: "Should be return failure when calling delete method")]
        public virtual void Test_Delete_Failure()
        {
            Guid id = Guid.Empty;

            _repository.Delete(id);
        }

        #region protected METHODS
        protected void RepositoryInitialize(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        protected static void ContextInitialize()
        {
            context = MockContextBuilder<T>.GetContext(data);
        }

        protected static void ContextDestroy()
        {
            context = MockContextBuilder<T>.Dispose();
        }
        #endregion
    }
}
