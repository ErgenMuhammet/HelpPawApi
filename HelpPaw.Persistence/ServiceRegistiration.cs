using HelpPaw.Persistence.Context;
using HelpPaw.Persistence.TokenService;
using HelpPawApi.Domain.Entities.AppRole;
using HelpPawApi.Domain.Entities.AppUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPaw.Infrustructure.ChatService;


namespace HelpPaw.Persistence
{
    public static class ServiceRegistiration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("database"))
            );

            services.AddIdentityCore<AppUsers>(options =>
            {
                // Geliştirme aşamasında şifre kurallarını gevşetelim (Opsiyonel)
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.User.RequireUniqueEmail = true; // Email eşsiz olsun
                options.User.AllowedUserNameCharacters = null;
            })
                .AddRoles<AppRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();


                services.AddScoped<ITokenServices, TokenServices>();
                services.AddScoped<IAppContext>(x => x.GetRequiredService<IdentityContext>());
               
        }
    }
}
