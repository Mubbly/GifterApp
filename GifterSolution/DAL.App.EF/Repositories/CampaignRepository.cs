﻿using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class CampaignRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Campaign, DALAppDTO.Campaign>,
        ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) :
            base(dbContext, new BaseMapper<DomainApp.Campaign, DALAppDTO.Campaign>())
        {
        }

        // // TODO: User stuff 
        //
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