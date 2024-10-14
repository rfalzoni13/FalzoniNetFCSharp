using System.Data.Entity;
using System.Transactions;

namespace FalzoniNetFCSharp.Infra.Data.Context.PostgreSql
{
    public class FalzoniPostgreSqlContext : FalzoniContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("net");
            base.OnModelCreating(modelBuilder);
        }
    }
}
