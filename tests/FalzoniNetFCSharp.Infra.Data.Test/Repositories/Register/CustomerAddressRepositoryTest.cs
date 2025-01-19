using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register;
using FalzoniNetFCSharp.Infra.Data.Repositories.Register;
using FalzoniNetFCSharp.Infra.Data.Test.Repositories.Base;
using FalzoniNetFCSharp.Test.Utils.Data.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FalzoniNetFCSharp.Infra.Data.Test.Repositories.Register
{
    [TestClass]
    public class CustomerAddressRepositoryTest : BaseRepositoryTest<CustomerAddress>
    {
        private ICustomerAddressRepository _repository;

        #region Test Class configuration
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            testContext.WriteLine("Iniciando teste de repositório de endereço de clientes");
            data = new CustomerAddressData();
            ContextInitialize();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new CustomerAddressRepository(context.Object);
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
    }
}
