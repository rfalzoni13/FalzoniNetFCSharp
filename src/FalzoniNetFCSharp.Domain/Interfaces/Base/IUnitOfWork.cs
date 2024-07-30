using System;
using System.Data.Entity;

namespace FalzoniNetFCSharp.Domain.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        DbContextTransaction BeginTransaction();
    }
}
