using System;
using System.Data.Entity;

namespace FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        DbContextTransaction BeginTransaction();
    }
}
