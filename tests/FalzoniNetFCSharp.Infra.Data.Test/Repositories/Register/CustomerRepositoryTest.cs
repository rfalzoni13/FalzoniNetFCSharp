using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register;
using FalzoniNetFCSharp.Infra.Data.Repositories.Register;
using FalzoniNetFCSharp.Infra.Data.Test.Repositories.Base;
using FalzoniNetFCSharp.Test.Utils.Data.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FalzoniNetFCSharp.Infra.Data.Test.Repositories.Register
{
    [TestClass]
    public class CustomerRepositoryTest : BaseRepositoryTest<Customer>
    {
        private ICustomerRepository _repository;

        #region Test Class configuration
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            testContext.WriteLine("Iniciando teste de repositório de clientes");
            data = new CustomerData();
            ContextInitialize();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new CustomerRepository(context.Object);
            RepositoryInitialize(_repository);
        }

        [TestCleanup]
        public void TestCleanup() 
        {
            _repository.Dispose();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            ContextDestroy();
        }
        #endregion

        #region Self Methods
        [TestMethod(displayName: "Should be return get with include customer success")]
        public void Test_GetWithInclude_Success()
        {
            Guid id = data.GetGuidFromList();

            Customer customer = _repository.GetWithInclude(id);

            Assert.IsNotNull(customer);
            Assert.AreEqual(id, customer.Id);
            Assert.IsTrue(customer.Addresses.Count() > 0);
        }

        [TestMethod(displayName: "Should be return get with include customer empty")]
        public void Test_GetWithInclude_Empty()
        {
            Guid id = data.GetGuidWithoutInclude();

            Customer customer = _repository.GetWithInclude(id);

            Assert.IsNotNull(customer);
            Assert.AreEqual(id, customer?.Id);
            Assert.IsFalse(customer?.Addresses?.Any());
        }
        #endregion
    }
}
