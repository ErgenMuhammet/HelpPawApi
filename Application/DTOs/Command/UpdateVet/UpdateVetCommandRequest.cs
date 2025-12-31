using HelpPawApi.Domain.Entities.Locations;
using MediatR;
using System.Text.Json.Serialization;


namespace HelpPawApi.Application.DTOs.Command.UpdateVet
{
    public class UpdateVetCommandRequest:IRequest<UpdateVetCommandResponse> 
    {
        [JsonIgnore]
        public string? EmailFromToken { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
    
        public DateTime Bhirtday{ get; set; }

        public string? VeterinaryClinicName { get; set; }        
        public Location ClinicLocation { get; set; }
    }
}
