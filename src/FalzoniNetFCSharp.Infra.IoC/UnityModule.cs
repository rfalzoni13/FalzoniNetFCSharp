using FalzoniNetFCSharp.Application.ServiceApplication.Configuration;
using FalzoniNetFCSharp.Application.ServiceApplication.Identity;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Stock;
using FalzoniNetFCSharp.Infra.Data.Context.MySql;
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
        public static UnityContainer LoadModules()
        {
            var container = new UnityContainer();

            #region Repositories
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<ICustomerAddressRepository, CustomerAddressRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();
            #endregion

            #region Services
            container.RegisterType(typeof(ServiceBase<>));

            container.RegisterType<CustomerService>();
            container.RegisterType<ProductService>();
            #endregion

            #region Application
            container.RegisterType<RoleServiceApplication>();
            container.RegisterType<AccountServiceApplication>();
            container.RegisterType<IdentityUtilityServiceApplication>();
            container.RegisterType<UserServiceApplication>();
            #endregion

            //Complements
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            //Context
            switch (ConfigurationHelper.ProviderName)
            {
                case "SqlServer":
                    container.RegisterType<DbContext, FalzoniSqlServerContext>();
                    break;

                case "MySql":
                    container.RegisterType<DbContext, FalzoniMySqlContext>();
                    break;
            }

            return container;
        }
    }
}
