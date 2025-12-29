using MediatR;
using System.Text.Json.Serialization;

namespace HelpPawApi.Application.DTOs.Query.GetAdvertisement
{
    public class GetAdvertisementQueryRequest : IRequest<GetAdvertisementQueryResponse>
    {
        [JsonIgnore]
        public string? AdvertisementId { get; set; }
    }
}
