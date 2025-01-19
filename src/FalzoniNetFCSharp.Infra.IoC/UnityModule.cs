using FalzoniNetFCSharp.Application.IdentityConfiguration;
using FalzoniNetFCSharp.Application.ServiceApplication.Configuration;
using FalzoniNetFCSharp.Application.ServiceApplication.Identity;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Stock;
using FalzoniNetFCSharp.Infra.Data.Context.MySql;
using FalzoniNetFCSharp.Infra.Data.Context.PostgreSql;
using FalzoniNetFCSharp.Infra.Data.Context.SqlServer;
using FalzoniNetFCSharp.Infra.Data.Repositories.Base;
using FalzoniNetFCSharp.Infra.Data.Repositories.Register;
using FalzoniNetFCSharp.Infra.Data.Repositories.Stock;
using FalzoniNetFCSharp.Service.Base;
using FalzoniNetFCSharp.Service.Register;
using FalzoniNetFCSharp.Service.Stock;
using FalzoniNetFCSharp.Utils.Helpers;
using System.Data.Entity;
using Unity;


namespace FalzoniNetFCSharp.Infra.IoC
{
    public class UnityModule
    {
        private static UnityContainer _container = new UnityContainer();

        public static UnityContainer LoadModules()
        {
            // Repositories
            RegisterRepositorues();

            // Services
            RegisterServices();

            // Service Applications
            RegisterServiceApplications();

            // Complements
            RegisterComplements();

            //Context
            RegisterContext(ConfigurationHelper.ProviderName);

            return _container;
        }

        #region private METHODS
        private static void RegisterComplements()
        {
            //Complements
            _container.RegisterType<IUnitOfWork, UnitOfWork>();
            _container.RegisterType<IdentityOfWork>();
        }

        private static void RegisterServiceApplications()
        {
            _container.RegisterType<RoleServiceApplication>();
            _container.RegisterType<AccountServiceApplication>();
            _container.RegisterType<IdentityUtilityServiceApplication>();
            _container.RegisterType<UserServiceApplication>();
        }

        private static void RegisterServices()
        {
            _container.RegisterType(typeof(ServiceBase<>));

            _container.RegisterType<CustomerService>();
            _container.RegisterType<ProductService>();

        }

        private static void RegisterRepositorues()
        {
            _container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            _container.RegisterType<ICustomerRepository, CustomerRepository>();
            _container.RegisterType<ICustomerAddressRepository, CustomerAddressRepository>();
            _container.RegisterType<IProductRepository, ProductRepository>();
            _container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();

        }

        private static void RegisterContext(string configuration)
        {
            switch (configuration)
            {
                case "SqlServer":
                    _container.RegisterType<DbContext, FalzoniSqlServerContext>();
                    break;
                case "MySql":
                    _container.RegisterType<DbContext, FalzoniMySqlContext>();
                    break;
                case "PostgreSql":
                    _container.RegisterType<DbContext, FalzoniPostgreSqlContext>();
                    break;
            }   
        }
        #endregion
    }
}
