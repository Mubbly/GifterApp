using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using com.mubbly.gifterapp.DAL.Base.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class PermissionRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Permission, DALAppDTO.PermissionDAL>,
        IPermissionRepository
    {
        public PermissionRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Permission, DALAppDTO.PermissionDAL>())
        {
        }

        // // TODO: User stuff
        //
        // public async Task<IEnumerable<Permission>> AllAsync(Guid? userId = null)
        // {
        //     return await base.AllAsync();
        // }
        //
        // public async Task<Permission> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(p => p.Id == id)
        //         .AsQueryable();
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     return await RepoDbSet.AnyAsync(p => p.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var permission = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(permission);
        // }
        //
        // public async Task<IEnumerable<PermissionDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet.AsQueryable();
        //     return await query
        //         .Select(p => new PermissionDTO() 
        //         {
        //             Id = p.Id,
        //             PermissionValue = p.PermissionValue,
        //             Comment = p.Comment,
        //             UserPermissionsCount = p.UserPermissions.Count
        //         }).ToListAsync();
        // }
        //
        // public async Task<PermissionDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(p => p.Id == id)
        //         .AsQueryable();
        //     return await query
        //         .Select(p => new PermissionDTO() 
        //         {
        //             Id = p.Id,
        //             PermissionValue = p.PermissionValue,
        //             Comment = p.Comment,
        //             UserPermissionsCount = p.UserPermissions.Count
        //         }).FirstOrDefaultAsync();
        // }
    }
}