﻿using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DonateeRepository : BaseRepository<Donatee>, IDonateeRepository
    {
        public DonateeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}