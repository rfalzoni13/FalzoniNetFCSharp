[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FalzoniNetFCSharp.Presentation.Administrator.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FalzoniNetFCSharp.Presentation.Administrator.App_Start.NinjectWebCommon), "Stop")]

namespace FalzoniNetFCSharp.Presentation.Administrator.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Base;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Configuration;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Identity;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Register;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Stock;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Register;
    using FalzoniNetFCSharp.Presentation.Administrator.Clients.Stock;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {            // Dependency Injection of Client's Restful interfaces
            kernel.Bind(typeof(IBaseClient<,>), typeof(BaseClient<,>));


            kernel.Bind<IRoleClient>().To<RoleClient>().InRequestScope();
            kernel.Bind<IUserClient>().To<UserClient>().InRequestScope();

            // Register
            kernel.Bind<ICustomerClient>().To<CustomerClient>().InRequestScope();

            // Stock
            kernel.Bind<IProductClient>().To<ProductClient>().InRequestScope();

            // Account
            kernel.Bind<AccountClient>().ToSelf().InRequestScope();
            kernel.Bind<IdentityUtilityClient>().ToSelf().InRequestScope();

            //kernel.BindFilter<ProfileActionAttribute>(System.Web.Mvc.FilterScope.Global, 1).InRequestScope();
        }
    }
}