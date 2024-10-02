using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<PlayerProfiles>? PlayerProfiles { get; set; }
        public DbSet<ConfirmRegistrationCodes>? ConfirmRegistrationCodes { get; set; }
        public DbSet<PlayerCredentials>? PlayerCredentials { get; set; }
        public DbSet<RefreshTokens>? RefreshTokens { get; set; }
        public DbSet<Subscriptions>? Subscriptions { get; set; }
        public DbSet<CustomMaps>? CustomMaps { get; set; }
        public DbSet<Downloads>? Downloads { get; set; }
    }
}
