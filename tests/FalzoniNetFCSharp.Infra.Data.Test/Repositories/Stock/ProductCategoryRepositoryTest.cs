using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Stock;
using FalzoniNetFCSharp.Infra.Data.Repositories.Stock;
using FalzoniNetFCSharp.Infra.Data.Test.Repositories.Base;
using FalzoniNetFCSharp.Test.Utils.Data.Stock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FalzoniNetFCSharp.Infra.Data.Test.Repositories.Stock
{
    [TestClass]
    public class ProductCategoryRepositoryTest : BaseRepositoryTest<ProductCategory>
    {
        private IProductCategoryRepository _repository;

        #region Test Class configuration
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            testContext.WriteLine("Iniciando teste de repositório de categorias de produtos");
            data = new ProductCategoryData();
            ContextInitialize();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new ProductCategoryRepository(context.Object);
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
