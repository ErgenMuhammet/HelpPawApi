using HelpPawApi.Domain.Common;
using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Locations;
using System.Data.Common;
using System.Text.Json.Serialization;


namespace HelpPawApi.Domain.Entities.Advertisement
{
    public class Advertisements : EntityBase
    {
        

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string AddressDescription  { get; set; }

        public string userName { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public Location Location { get; set; }

        public string? VetId{ get; set; }
        public Vet Vet { get; set; }

        
    }
}
