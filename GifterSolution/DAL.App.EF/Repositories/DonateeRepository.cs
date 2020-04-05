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
    public class DonateeRepository : EFBaseRepository<Donatee, AppDbContext>, IDonateeRepository
    {
        public DonateeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Donatee>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(d => d.ActionType)
                .Include(d => d.Status)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(o => o.Owner!.AppUserId == userId && o.Animal!.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<Donatee> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(d => d.Id == id)
                .Include(d => d.ActionType)
                .Include(d => d.Status)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(a => a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(a =>
                //     a.Id == id && a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }
            return await RepoDbSet.AnyAsync(d => d.Id == id);
        }

        public Task DeleteAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DonateeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(d => d.ActionType)
                .Include(d => d.Status)
                .AsQueryable();
            
            return await query
                    .Select(d => new DonateeDTO()
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Gender = d.Gender,
                        Age = d.Age,
                        Bio = d.Bio,
                        GiftName = d.GiftName,
                        GiftDescription = d.GiftDescription,
                        GiftImage = d.GiftImage,
                        GiftUrl = d.GiftUrl,
                        ActiveFrom = d.ActiveFrom,
                        ActiveTo = d.ActiveTo,
                        IsActive = d.IsActive,
                        GiftReservedFrom = d.GiftReservedFrom,
                        ActionTypeId = d.ActionTypeId,
                        StatusId = d.StatusId,
                        CampaignDonateesCount = d.CampaignDonatees.Count,
                        ActionType = new ActionTypeDTO()
                        {
                            Id = d.ActionType!.Id,
                            Comment = d.ActionType!.Comment,
                            DonateesCount = d.ActionType!.Donatees.Count,
                            GiftsCount = d.ActionType!.Gifts.Count,
                            ActionTypeValue = d.ActionType!.ActionTypeValue,
                            ArchivedGiftsCount = d.ActionType!.ArchivedGifts.Count,
                            ReservedGiftsCount = d.ActionType!.ReservedGifts.Count,
                        },
                        Status = new StatusDTO()
                        {
                            Id = d.Status!.Id,
                            Comment = d.Status!.Comment,
                            DonateesCount = d.Status!.Donatees.Count,
                            GiftsCount = d.Status!.Gifts.Count,
                            StatusValue = d.Status!.StatusValue,
                            ArchivedGiftsCount = d.Status!.ArchivedGifts.Count,
                            ReservedGiftsCount = d.Status!.ReservedGifts.Count
                        }
                    }).ToListAsync();
        }

        public async Task<DonateeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(d => d.Id == id)
                .Include(d => d.ActionType)
                .Include(d => d.Status)
                .AsQueryable();

            return await query.Select(d => new DonateeDTO()
                    {
                        Id = d.Id,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Gender = d.Gender,
                        Age = d.Age,
                        Bio = d.Bio,
                        GiftName = d.GiftName,
                        GiftDescription = d.GiftDescription,
                        GiftImage = d.GiftImage,
                        GiftUrl = d.GiftUrl,
                        ActiveFrom = d.ActiveFrom,
                        ActiveTo = d.ActiveTo,
                        IsActive = d.IsActive,
                        GiftReservedFrom = d.GiftReservedFrom,
                        ActionTypeId = d.ActionTypeId,
                        StatusId = d.StatusId,
                        CampaignDonateesCount = d.CampaignDonatees.Count,
                        ActionType = new ActionTypeDTO()
                        {
                            Id = d.ActionType!.Id,
                            Comment = d.ActionType!.Comment,
                            DonateesCount = d.ActionType!.Donatees.Count,
                            GiftsCount = d.ActionType!.Gifts.Count,
                            ActionTypeValue = d.ActionType!.ActionTypeValue,
                            ArchivedGiftsCount = d.ActionType!.ArchivedGifts.Count,
                            ReservedGiftsCount = d.ActionType!.ReservedGifts.Count,
                        },
                        Status = new StatusDTO()
                        {
                            Id = d.Status!.Id,
                            Comment = d.Status!.Comment,
                            DonateesCount = d.Status!.Donatees.Count,
                            GiftsCount = d.Status!.Gifts.Count,
                            StatusValue = d.Status!.StatusValue,
                            ArchivedGiftsCount = d.Status!.ArchivedGifts.Count,
                            ReservedGiftsCount = d.Status!.ReservedGifts.Count
                        }
            }).FirstOrDefaultAsync();
        }
    }
}