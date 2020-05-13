using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TKey, TDbContext> : BaseUnitOfWork<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        protected readonly TDbContext UOWDbContext;

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UOWDbContext = uowDbContext;
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await UOWDbContext.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            return UOWDbContext.SaveChanges();
        }
    }
}