using HelpPaw.Infrustructure.ChatService;
using HelpPaw.Infrustructure.EmailService;
using HelpPawApi.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPaw.Infrustructure
{
    public static class InfrastructureRegistiration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IChatService, ChatServices>();

        }
    }
}
