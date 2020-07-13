using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class ArchivedGiftRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.ArchivedGift, DALAppDTO.ArchivedGiftDAL>,
        IArchivedGiftRepository
    {
        public ArchivedGiftRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.ArchivedGift, DALAppDTO.ArchivedGiftDAL>())
        {
        }
        
        /**
         * Get all ArchivedGifts - only gifts that they have archived / given as a gift to someone else.
         * Even gifts that are not yet confirmed to be archived by the owner appear here.
         */
        public async Task<IEnumerable<DALAppDTO.ArchivedGiftDAL>> GetAllGiftedByUserAsync(Guid userId, bool noTracking = true)
        {
            var archivedGifts = PrepareQuery(userId, noTracking);
            var personalArchivedGifts =
                await archivedGifts
                    .Where(a => a.UserGiverId == userId)
                    .Include(a => a.UserReceiver)
                    .OrderBy(a => a.CreatedAt)
                    .Select(a => Mapper.Map(a))
                    .ToListAsync();
            return personalArchivedGifts;
        }

        /**
         * Get all ArchivedGifts - only gifts that belong to current user / they have received as a gift.
         * Gift has to be confirmed by owner to be fully archived.
         */
        public async Task<IEnumerable<DALAppDTO.ArchivedGiftDAL>> GetAllReceivedByUserAsync(Guid userId, bool noTracking = true)
        {
            var archivedGifts = PrepareQuery(userId, noTracking);
            var personalArchivedGifts =
                await archivedGifts
                    .Where(a => a.IsConfirmed && a.UserReceiverId == userId)
                    .Include(a => a.UserGiver)
                    .OrderBy(a => a.CreatedAt)
                    .Select(a => Mapper.Map(a))
                    .ToListAsync();
            return personalArchivedGifts;
        }
        
        /**
         * Get all ArchivedGifts - only gifts that belong to current user / they have received as a gift.
         * Not yet confirmed to be archived by the user.
         */
        public async Task<IEnumerable<DALAppDTO.ArchivedGiftDAL>> GetAllPendingReceivedByUserAsync(Guid userId, bool noTracking = true)
        {
            var archivedGifts = PrepareQuery(userId, noTracking);
            var personalArchivedGifts =
                await archivedGifts
                    .Where(a => a.IsConfirmed == false && a.UserReceiverId == userId)
                    .Include(a => a.UserGiver)
                    .OrderBy(a => a.CreatedAt)
                    .Select(a => Mapper.Map(a))
                    .ToListAsync();
            return personalArchivedGifts;
        }
        
        /**
         * Get an ArchivedGift based on giftId - only if the gift is archived by them / given as a gift to someone else.
         * Gift has to be confirmed by owner to be fully archived.
         */
        public async Task<DALAppDTO.ArchivedGiftDAL> GetGiftedByGiftIdAsync(Guid giftId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var archivedGift = await query
                .Where(a => a.IsConfirmed && a.GiftId == giftId)
                .Where(a => a.UserGiverId == userId)
                .OrderBy(a => a.CreatedAt)
                .Include(a => a.Gift)
                .ThenInclude(g => g != null ? g.Wishlist : null)
                .ThenInclude(w => w != null ? w.AppUser : null)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(archivedGift);
        }

        /**
         * Get an ArchivedGift based on giftId - only if the gift belongs to current user / they have received as a gift.
         * Gift has to be confirmed by owner to be fully archived.
         */
        public async Task<DALAppDTO.ArchivedGiftDAL> GetReceivedByGiftIdAsync(Guid giftId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
        
            var archivedGift = await query
                .Where(a => a.IsConfirmed && a.GiftId == giftId)
                .Where(a => a.UserReceiverId == userId)
                .OrderBy(a => a.CreatedAt)
                .Include(a => a.Gift)
                .ThenInclude(g => g != null ? g.Wishlist : null)
                .ThenInclude(w => w != null ? w.AppUser : null)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(archivedGift);
        }
        
        /**
         * Get an ArchivedGift based on giftId - only if the gift belongs to current user / they have received as a gift.
         * Not yet confirmed to be archived by the user.
         */
        public async Task<DALAppDTO.ArchivedGiftDAL> GetPendingReceivedByGiftIdAsync(Guid giftId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
        
            var archivedGift = await query
                .Where(a => a.IsConfirmed == false && a.GiftId == giftId)
                .Where(a => a.UserReceiverId == userId)
                .OrderBy(a => a.CreatedAt)
                .Include(a => a.Gift)
                .ThenInclude(g => g != null ? g.Wishlist : null)
                .ThenInclude(w => w != null ? w.AppUser : null)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(archivedGift);
        }
    }
}