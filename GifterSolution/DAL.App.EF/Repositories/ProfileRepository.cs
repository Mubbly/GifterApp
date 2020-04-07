﻿using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class ProfileRepository : EFBaseRepository<Profile, AppDbContext>, IProfileRepository
    {
        public ProfileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}