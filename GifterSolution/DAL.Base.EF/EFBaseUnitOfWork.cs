using System.Threading.Tasks;
using Contracts.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TDbContext> : BaseUnitOfWork, IBaseUnitOfWork
    where TDbContext : DbContext
    {
        protected TDbContext UOWDbContext;

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UOWDbContext = uowDbContext;
        }
        
        public int SaveChanges()
        {
            return UOWDbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return UOWDbContext.SaveChangesAsync();
        }
    }
}