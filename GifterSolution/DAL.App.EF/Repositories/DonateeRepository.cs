using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class DonateeRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Donatee, DALAppDTO.DonateeDAL>,
        IDonateeRepository
    {
        public DonateeRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Donatee, DALAppDTO.DonateeDAL>())
        {
        }
        
        public async Task<IEnumerable<DALAppDTO.DonateeDAL>> GetAllForCampaignAsync(Guid campaignId, Guid? userId, bool noTracking = true)
        {
            var donatees = 
                await RepoDbContext
                .CampaignDonatees
                .Include(a => a.Donatee)
                .Where(cd => cd.CampaignId == campaignId)
                .Select(e => Mapper.Map(e.Donatee!))
                .ToListAsync();

            return donatees;
            
            // var campaignDonatees = await RepoDbContext.CampaignDonatees.ToListAsync();
            // var donatees = await RepoDbSet.ToListAsync();
            
            // return 
            //     from campaignDonatee in campaignDonatees
            //     join donatee in donatees
            //         on campaignDonatee.DonateeId equals donatee.Id
            //     where campaignId == campaignDonatee.CampaignId
            //     select Mapper.Map(donatee);
        }
        
        
        
        // public async Task<IEnumerable<DALAppDTO.DonateeDAL>> GetAllForCampaignAsync(Guid campaignId, Guid? userId, bool noTracking = true)
        // {
        //     var allCampaignDonatees = await UOW.CampaignDonatees.GetAllAsync();
        //     var donatees = await UOW.Donatees.GetAllAsync();
        //
        //     return 
        //         from campaignDonatee in allCampaignDonatees
        //         join donatee in donatees
        //             on campaignDonatee.DonateeId equals donatee.Id
        //         where campaignId == campaignDonatee.CampaignId
        //         select donatee;
        // }
        
        // public override async Task<IEnumerable<DALAppDTO.DonateeDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        // {
        //     var query = PrepareQuery(userId, noTracking);
        //     query = query
        //         .Include(g => g.AppUser)
        //         .Include(g => g.GpsSessionType)
        //         .ThenInclude(g => g!.Name)
        //         .ThenInclude(t => t!.Translations)
        //         .OrderByDescending(a => a.RecordedAt);
        //     var domainItems = await query.ToListAsync();
        //     var result = domainItems.Select(e => Mapper.Map(e));
        //     return result;
        // }

        // // TODO: User stuff 
        //
        // public async Task<IEnumerable<Donatee>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(d => d.ActionType)
        //         .Include(d => d.Status)
        //         .AsQueryable();
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<Donatee> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(d => d.Id == id)
        //         .Include(d => d.ActionType)
        //         .Include(d => d.Status)
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
        //     var donatee = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(donatee);
        // }
        //
        // public async Task<IEnumerable<DonateeDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(d => d.ActionType)
        //         .Include(d => d.Status)
        //         .AsQueryable();
        //     
        //     return await query
        //             .Select(d => new DonateeDTO()
        //             {
        //                 Id = d.Id,
        //                 FirstName = d.FirstName,
        //                 LastName = d.LastName,
        //                 Gender = d.Gender,
        //                 Age = d.Age,
        //                 Bio = d.Bio,
        //                 GiftName = d.GiftName,
        //                 GiftDescription = d.GiftDescription,
        //                 GiftImage = d.GiftImage,
        //                 GiftUrl = d.GiftUrl,
        //                 ActiveFrom = d.ActiveFrom,
        //                 ActiveTo = d.ActiveTo,
        //                 IsActive = d.IsActive,
        //                 GiftReservedFrom = d.GiftReservedFrom,
        //                 ActionTypeId = d.ActionTypeId,
        //                 StatusId = d.StatusId,
        //                 CampaignDonateesCount = d.CampaignDonatees.Count,
        //                 ActionType = new ActionTypeDTO()
        //                 {
        //                     Id = d.ActionType!.Id,
        //                     Comment = d.ActionType!.Comment,
        //                     DonateesCount = d.ActionType!.Donatees.Count,
        //                     GiftsCount = d.ActionType!.Gifts.Count,
        //                     ActionTypeValue = d.ActionType!.ActionTypeValue,
        //                     ArchivedGiftsCount = d.ActionType!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = d.ActionType!.ReservedGifts.Count,
        //                 },
        //                 Status = new StatusDTO()
        //                 {
        //                     Id = d.Status!.Id,
        //                     Comment = d.Status!.Comment,
        //                     DonateesCount = d.Status!.Donatees.Count,
        //                     GiftsCount = d.Status!.Gifts.Count,
        //                     StatusValue = d.Status!.StatusValue,
        //                     ArchivedGiftsCount = d.Status!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = d.Status!.ReservedGifts.Count
        //                 }
        //             }).ToListAsync();
        // }
        //
        // public async Task<DonateeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(d => d.Id == id)
        //         .Include(d => d.ActionType)
        //         .Include(d => d.Status)
        //         .AsQueryable();
        //
        //     return await query.Select(d => new DonateeDTO()
        //             {
        //                 Id = d.Id,
        //                 FirstName = d.FirstName,
        //                 LastName = d.LastName,
        //                 Gender = d.Gender,
        //                 Age = d.Age,
        //                 Bio = d.Bio,
        //                 GiftName = d.GiftName,
        //                 GiftDescription = d.GiftDescription,
        //                 GiftImage = d.GiftImage,
        //                 GiftUrl = d.GiftUrl,
        //                 ActiveFrom = d.ActiveFrom,
        //                 ActiveTo = d.ActiveTo,
        //                 IsActive = d.IsActive,
        //                 GiftReservedFrom = d.GiftReservedFrom,
        //                 ActionTypeId = d.ActionTypeId,
        //                 StatusId = d.StatusId,
        //                 CampaignDonateesCount = d.CampaignDonatees.Count,
        //                 ActionType = new ActionTypeDTO()
        //                 {
        //                     Id = d.ActionType!.Id,
        //                     Comment = d.ActionType!.Comment,
        //                     DonateesCount = d.ActionType!.Donatees.Count,
        //                     GiftsCount = d.ActionType!.Gifts.Count,
        //                     ActionTypeValue = d.ActionType!.ActionTypeValue,
        //                     ArchivedGiftsCount = d.ActionType!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = d.ActionType!.ReservedGifts.Count,
        //                 },
        //                 Status = new StatusDTO()
        //                 {
        //                     Id = d.Status!.Id,
        //                     Comment = d.Status!.Comment,
        //                     DonateesCount = d.Status!.Donatees.Count,
        //                     GiftsCount = d.Status!.Gifts.Count,
        //                     StatusValue = d.Status!.StatusValue,
        //                     ArchivedGiftsCount = d.Status!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = d.Status!.ReservedGifts.Count
        //                 }
        //     }).FirstOrDefaultAsync();
        // }
    }
}