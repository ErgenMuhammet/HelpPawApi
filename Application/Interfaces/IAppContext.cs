using HelpPawApi.Domain.Entities.Advertisement;
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
         public DbSet<Advertisements> Advertisements { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
