using HelpPawApi.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ShowMyInformationForVet
{
    public class ShowMyInformationForVetQueryResponse
    {
        public string FullName { get; set; }
        public string? PhotoUrl { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string VeterinaryClinicName { get; set; }
        public string VeterinerRegistiryNumber { get; set; }
        public Location? Location { get; set; }
        public string Message{ get; set; }
        public bool IsSucces { get; set; }
    }

    
}
