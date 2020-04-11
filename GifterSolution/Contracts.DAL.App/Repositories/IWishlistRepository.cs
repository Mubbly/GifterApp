using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IWishlistRepository : IBaseRepository<Wishlist>
    {
        Task<IEnumerable<Wishlist>> AllAsync(Guid? userId = null);
        Task<Wishlist> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<WishlistDTO>> DTOAllAsync(Guid? userId = null);
        Task<WishlistDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);    
    }
}