using HelpPawApi.Domain.Entities.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Domain.Entities.AppUser
{
    public class User : AppUsers
    {
        public ICollection<Advertisements> CreatedAdvertisements { get; set; }

    }
}
