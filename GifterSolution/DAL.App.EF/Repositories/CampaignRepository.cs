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
    public class CampaignRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Campaign, DALAppDTO.CampaignDAL>,
        ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Campaign, DALAppDTO.CampaignDAL>())
        {
        }

        /** Includes donatees collection */
        public new async Task<IEnumerable<DALAppDTO.CampaignDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var campaigns =
                await query
                    .Include(c => c.CampaignDonatees)
                    .OrderBy(c => c.CreatedAt)
                    .Select(c => Mapper.Map(c))
                    .ToListAsync();
            return campaigns;
        }

        /** Includes donatees collection */
        public async Task<IEnumerable<DALAppDTO.CampaignDAL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            // TODO : Fix tracking, and populating middle tables by EF?
            var userCampaigns = 
                await RepoDbContext
                    .UserCampaigns
                    .AsNoTracking()
                    .Include(a => a.Campaign)
                    .Include(a => a.Campaign!.CampaignDonatees)
                    .Where(cd => cd.AppUserId == userId)
                    .OrderBy(e => e.CreatedAt)
                    .Select(e => Mapper.Map(e.Campaign!))
                    .ToListAsync();

            return userCampaigns;
        }

        // public async Task<IEnumerable<Campaign>> AllAsync(Guid? userId = null)
        // {
        //     return await base.AllAsync();
        // }
        //
        // public async Task<Campaign> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(c => c.Id == id)
        //         .AsQueryable();
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     return await RepoDbSet.AnyAsync(d => d.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var campaign = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(campaign);        
        // }
        //
        // public async Task<IEnumerable<CampaignDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet.AsQueryable();
        //
        //     return await query
        //         .Select(c => new CampaignDTO() 
        //             {
        //                 Id = c.Id,
        //                 Name = c.Name,
        //                 Description = c.Description,
        //                 Institution = c.Institution,
        //                 AdImage = c.AdImage,
        //                 ActiveFromDate = c.ActiveFromDate,
        //                 ActiveToDate = c.ActiveToDate,
        //                 IsActive = c.IsActive,
        //                 UserCampaignsCount = c.UserCampaigns.Count,
        //                 CampaignDonateesCount = c.CampaignDonatees.Count
        //             }).ToListAsync();
        // }
        //
        // public async Task<CampaignDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(c => c.Id == id)
        //         .AsQueryable();
        //
        //     return await query.Select(c => new CampaignDTO() 
        //         {
        //             Id = c.Id,
        //             Name = c.Name,
        //             Description = c.Description,
        //             Institution = c.Institution,
        //             AdImage = c.AdImage,
        //             ActiveFromDate = c.ActiveFromDate,
        //             ActiveToDate = c.ActiveToDate,
        //             IsActive = c.IsActive,
        //             UserCampaignsCount = c.UserCampaigns.Count,
        //             CampaignDonateesCount = c.CampaignDonatees.Count
        //     }).FirstOrDefaultAsync();
        // }
    }
}