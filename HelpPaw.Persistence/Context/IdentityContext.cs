using HelpPaw.Persistence.Configuration;
using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.AppRole;
using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Chat;
using HelpPawApi.Domain.Entities.Locations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;

namespace HelpPaw.Persistence.Context
{
    public class IdentityContext  : IdentityDbContext<AppUsers, AppRole, string>, IAppContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Location> location { get; set; }
        public DbSet<Advertisements> Advertisements { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new VetConfiguration());
        }
    }

}