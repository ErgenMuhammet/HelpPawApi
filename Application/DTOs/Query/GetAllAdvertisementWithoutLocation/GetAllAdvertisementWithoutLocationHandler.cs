using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetAllAdvertisementWithoutLocation
{
    public class GetAllAdvertisementWithoutLocationHandler : IRequestHandler<GetAllAdvertisementWithoutLocationRequest, GetAllAdvertisementWithoutLocationResponse>
    {
        private readonly IAppContext _appContext;

        public GetAllAdvertisementWithoutLocationHandler(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<GetAllAdvertisementWithoutLocationResponse> Handle(GetAllAdvertisementWithoutLocationRequest request, CancellationToken cancellationToken)
        {
            List<Advertisements> advs =  _appContext.Advertisements.ToList();
            
            if (advs is not null)
            {
                return new GetAllAdvertisementWithoutLocationResponse
                {
                    Advertisements = advs,
                    IsSucces = true
                };
            }
            return new GetAllAdvertisementWithoutLocationResponse
            {
                IsSucces=false,
                Message = "Görüntülenecek ilan bulunmamaktadır"
            };
        }
    }
}
