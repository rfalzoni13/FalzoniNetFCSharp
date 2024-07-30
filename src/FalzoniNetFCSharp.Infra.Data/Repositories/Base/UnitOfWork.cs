using FalzoniNetFCSharp.Domain.Interfaces.Base;
using System.Data.Entity;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Database.BeginTransaction().Dispose();
        }
    }
}
