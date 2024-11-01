﻿using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PlayerCredentialsRepository : RepositoryBase<PlayerCredentials>, IPlayerCredentialsRepository
    {
        public PlayerCredentialsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<bool?> Has(string email)
        {
            if (RepositoryContext.PlayerCredentials == null)
                return null;

            return await RepositoryContext.PlayerCredentials.FirstOrDefaultAsync(data => data.email == email) != null;
        }

        public async Task<PlayerCredentials?> Get(string email, string password)
        {
            if (RepositoryContext.PlayerCredentials == null)
                return null;

            return await RepositoryContext.PlayerCredentials.FirstOrDefaultAsync(data => data.email == email && data.password == password);
        }
    }
}
