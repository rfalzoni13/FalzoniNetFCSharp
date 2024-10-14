using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Infra.Data.Context.MySql;
using FalzoniNetFCSharp.Infra.Data.Context.PostgreSql;
using FalzoniNetFCSharp.Infra.Data.Context.SqlServer;
using FalzoniNetFCSharp.Infra.Data.Identity;
using FalzoniNetFCSharp.Utils.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace FalzoniNetFCSharp.Infra.Data.Context
{
    public abstract class FalzoniContext : IdentityDbContext<ApplicationUser>
    {
        #region Attributes
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        #endregion

        public FalzoniContext()
            : base("Falzoni", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public static FalzoniContext Create()
        {
            switch (ConfigurationHelper.ProviderName)
            {
                case "SqlServer":
                    return new FalzoniSqlServerContext();

                case "MySql":
                    return new FalzoniMySqlContext();

                case "PostgreSql":
                    return new FalzoniPostgreSqlContext();

                default:
                    throw new System.Exception("Erro ao definir provider");
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Create Generic instances of EntityBaseTypeConfiguration
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
