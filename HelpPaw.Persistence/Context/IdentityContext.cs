using HelpPaw.Persistence.Configuration;
using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.AppRole;
using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Chat;
using HelpPawApi.Domain.Entities.Locations;
using HelpPawApi.Domain.Entities.Notification;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HelpPaw.Persistence.Context
{
    public class IdentityContext
        : IdentityDbContext<AppUsers, AppRole, string>, IAppContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Location> Location { get; set; }
        public DbSet<Advertisements> Advertisements { get; set; }

    
        public DbSet<Notification> Notifications { get; set; }

       
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
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
