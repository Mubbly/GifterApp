using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class CampaignDonateeRepository : EFBaseRepository<CampaignDonatee, AppDbContext>, ICampaignDonateeRepository
    {
        public CampaignDonateeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        // TODO: User stuff

        public async Task<IEnumerable<CampaignDonatee>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Donatee)
                .Include(a => a.Campaign)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(o => o.Campaign!.AppUserId == userId && o.Donatee!.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<CampaignDonatee> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Donatee)
                .Include(a => a.Campaign)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(a => a.Campaign!.AppUserId == userId && a.Donatee!.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(a =>
                //     a.Id == id && a.Campaign!.AppUserId == userId && a.Donatee!.AppUserId == userId);
            }
            
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var campaign = await FirstOrDefaultAsync(id); // (id, userId);
            base.Remove(campaign);
        }

        public async Task<IEnumerable<CampaignDonateeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Campaign)
                .Include(o => o.Donatee)
                .AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(o => o.Donatee!.AppUserId == userId && o.Campaign!.AppUserId == userId);
            }

            return await query
                .Select(cd => new CampaignDonateeDTO()
                {
                    Id = cd.Id,
                    CampaignId = cd.CampaignId,
                    Comment = cd.Comment,
                    DonateeId = cd.DonateeId,
                    IsActive = cd.IsActive,
                    Donatee = new DonateeDTO()
                    {
                        Id = cd.Donatee!.Id,
                        ActionTypeId = cd.Donatee!.ActionTypeId,
                        ActiveFrom = cd.Donatee!.ActiveFrom,
                        Age = cd.Donatee!.Age,
                        ActiveTo = cd.Donatee!.ActiveTo,
                        Bio = cd.Donatee!.Bio,
                        CampaignDonateesCount = cd.Donatee!.CampaignDonatees.Count,
                        FirstName = cd.Donatee!.FirstName,
                        Gender = cd.Donatee!.Gender,
                        GiftDescription = cd.Donatee!.GiftDescription,
                        GiftImage = cd.Donatee!.GiftImage,
                        GiftName = cd.Donatee!.GiftName,
                        GiftUrl = cd.Donatee!.GiftUrl,
                        IsActive = cd.Donatee!.IsActive,
                        LastName = cd.Donatee!.LastName,
                        StatusId = cd.Donatee!.StatusId,
                        GiftReservedFrom = cd.Donatee!.GiftReservedFrom
                    },
                    Campaign = new CampaignDTO()
                    {
                        Id = cd.Campaign!.Id,
                        ActiveFromDate = cd.Campaign!.ActiveFromDate,
                        ActiveToDate = cd.Campaign!.ActiveToDate,
                        AdImage = cd.Campaign!.AdImage,
                        CampaignDonateesCount = cd.Campaign!.CampaignDonatees.Count,
                        Description = cd.Campaign!.Description,
                        Institution = cd.Campaign!.Institution,
                        IsActive = cd.Campaign!.IsActive,
                        Name = cd.Campaign!.Name,
                        UserCampaignsCount = cd.Campaign!.UserCampaigns.Count
                    }
                })
                .ToListAsync();
        }

        public async Task<CampaignDonateeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Donatee)
                .Include(a => a.Campaign)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(a => a.Campaign!.AppUserId == userId && a.Donatee!.AppUserId == userId);
            }

            return await query.Select(cd => new CampaignDonateeDTO()
            {
                Id = cd.Id,
                CampaignId = cd.CampaignId,
                Comment = cd.Comment,
                DonateeId = cd.DonateeId,
                IsActive = cd.IsActive,
                Donatee = new DonateeDTO()
                {
                Id = cd.Donatee!.Id,
                ActionTypeId = cd.Donatee!.ActionTypeId,
                ActiveFrom = cd.Donatee!.ActiveFrom,
                Age = cd.Donatee!.Age,
                ActiveTo = cd.Donatee!.ActiveTo,
                Bio = cd.Donatee!.Bio,
                CampaignDonateesCount = cd.Donatee!.CampaignDonatees.Count,
                FirstName = cd.Donatee!.FirstName,
                Gender = cd.Donatee!.Gender,
                GiftDescription = cd.Donatee!.GiftDescription,
                GiftImage = cd.Donatee!.GiftImage,
                GiftName = cd.Donatee!.GiftName,
                GiftUrl = cd.Donatee!.GiftUrl,
                IsActive = cd.Donatee!.IsActive,
                LastName = cd.Donatee!.LastName,
                StatusId = cd.Donatee!.StatusId,
                GiftReservedFrom = cd.Donatee!.GiftReservedFrom
            },
            Campaign = new CampaignDTO()
            {
                Id = cd.Campaign!.Id,
                ActiveFromDate = cd.Campaign!.ActiveFromDate,
                ActiveToDate = cd.Campaign!.ActiveToDate,
                AdImage = cd.Campaign!.AdImage,
                CampaignDonateesCount = cd.Campaign!.CampaignDonatees.Count,
                Description = cd.Campaign!.Description,
                Institution = cd.Campaign!.Institution,
                IsActive = cd.Campaign!.IsActive,
                Name = cd.Campaign!.Name,
                UserCampaignsCount = cd.Campaign!.UserCampaigns.Count
            }
            }).FirstOrDefaultAsync();
        }

    }
}