using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.Chat;
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
 
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
