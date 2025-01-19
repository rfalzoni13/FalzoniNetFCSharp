using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Infra.Data.Test.Repositories.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FalzoniNetFCSharp.Infra.Data.Repositories.Stock;
using FalzoniNetFCSharp.Test.Utils.Data.Stock;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Stock;

namespace FalzoniNetFCSharp.Infra.Data.Test.Repositories.Stock
{
    [TestClass]
    public class ProductRepositoryTest : BaseRepositoryTest<Product>
    {
        private IProductRepository _repository;

        #region Test Class configuration
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            testContext.WriteLine("Iniciando teste de repositório de produtos");
            data = new ProductData();
            ContextInitialize();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new ProductRepository(context.Object);
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
