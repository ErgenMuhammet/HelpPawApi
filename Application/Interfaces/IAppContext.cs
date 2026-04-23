using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.Chat;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.Interfaces
{
    public interface IAppContext
    {
         public DbSet<ChatMessage> Messages { get; set; }
         public DbSet<Advertisements> Advertisements { get; set; }
        DbSet<Notification> Notifications { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
