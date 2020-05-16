using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class ActionTypeRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.ActionType, DALAppDTO.ActionTypeDAL>,
        IActionTypeRepository
    {
        public ActionTypeRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.ActionType, DALAppDTO.ActionTypeDAL>())
        {
        }

        // public async Task<IEnumerable<DALAppDTO.ActionType>> GetAllAsync(Guid? userId = null)
        // {
        //     return await base.GetAllAsync();
        // }
        //
        // public override async Task<DALAppDTO.ActionType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(at => at.Id == id)
        //         .AsQueryable();
        //     return Mapper.Map(await query.FirstOrDefaultAsync());
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     return await RepoDbSet.AnyAsync(d => d.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var actionType = await FirstOrDefaultAsync(id, userId);
        //     await base.RemoveAsync(actionType);       
        // }

        // public async Task<IEnumerable<ActionTypeDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet.AsQueryable();
        //
        //     return await query
        //         .Select(at => new ActionTypeDTO() 
        //         {
        //             Id = at.Id,
        //             ActionTypeValue = at.ActionTypeValue,
        //             Comment = at.Comment,
        //             DonateesCount = at.Donatees!.Count,
        //             GiftsCount = at.Gifts!.Count,
        //             ArchivedGiftsCount = at.ArchivedGifts!.Count,
        //             ReservedGiftsCount = at.ReservedGifts!.Count,
        //         }).ToListAsync();
        // }
        //
        // public async Task<ActionTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(at => at.Id == id)
        //         .AsQueryable();
        //
        //     return await query.Select(at => new ActionTypeDTO 
        //     {
        //         Id = at.Id,
        //         ActionTypeValue = at.ActionTypeValue,
        //         Comment = at.Comment,
        //         DonateesCount = at.Donatees.Count,
        //         GiftsCount = at.Gifts.Count,
        //         ArchivedGiftsCount = at.ArchivedGifts.Count,
        //         ReservedGiftsCount = at.ReservedGifts.Count,
        //     }).FirstOrDefaultAsync();
        // }
    }
}