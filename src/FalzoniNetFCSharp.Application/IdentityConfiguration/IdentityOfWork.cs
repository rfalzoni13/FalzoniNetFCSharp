using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base;
using FalzoniNetFCSharp.Infra.Data.Context;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Web;

namespace FalzoniNetFCSharp.Application.IdentityConfiguration
{
    public class IdentityOfWork
    {
        private FalzoniContext _context;

        public IdentityOfWork()
        {
            _context = HttpContext.Current.GetOwinContext().Get<FalzoniContext>();
        }

        public DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
