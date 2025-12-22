using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Domain.Entities.AppUser
{
    public class Vet : AppUsers
    {
        
        public string VeterinaryClinicName { get; set; }
        public string VeterinerRegistiryNumber { get; set; }

        public ICollection<Advertisements> TreatedAdvertisements { get; set; }

    }
}
