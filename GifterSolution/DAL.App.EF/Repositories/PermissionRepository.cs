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
    public class PermissionRepository : EFBaseRepository<Permission, AppDbContext>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        // TODO: User stuff

        public async Task<IEnumerable<Permission>> AllAsync(Guid? userId = null)
        {
            return await base.AllAsync();
        }

        public async Task<Permission> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(p => p.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(p => p.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var permission = await FirstOrDefaultAsync(id, userId);
            base.Remove(permission);
        }

        public async Task<IEnumerable<PermissionDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(p => new PermissionDTO() 
                {
                    Id = p.Id,
                    PermissionValue = p.PermissionValue,
                    Comment = p.Comment,
                    UserPermissionsCount = p.UserPermissions.Count
                }).ToListAsync();
        }

        public async Task<PermissionDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(p => p.Id == id)
                .AsQueryable();
            return await query
                .Select(p => new PermissionDTO() 
                {
                    Id = p.Id,
                    PermissionValue = p.PermissionValue,
                    Comment = p.Comment,
                    UserPermissionsCount = p.UserPermissions.Count
                }).FirstOrDefaultAsync();
        }
    }
}