using HelpPawApi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetAdvertisement
{
    public class GetAdvertisementQueryHandler : IRequestHandler<GetAdvertisementQueryRequest, GetAdvertisementQueryResponse>
    {
        private readonly IAppContext _context;

        public GetAdvertisementQueryHandler(IAppContext Context)
        {
            _context = Context;
        }
        public async Task<GetAdvertisementQueryResponse> Handle(GetAdvertisementQueryRequest request, CancellationToken cancellationToken)
        {
            var advs = await _context.Advertisements.FirstOrDefaultAsync(x => x.Id.ToString() == request.AdvertisementId);

            if (advs == null)
            {
                return new GetAdvertisementQueryResponse
                {
                    Message = "İlan bilgileri yüklenirken hata oluştu.",
                    IsSucces = false,
                };

               
            }

            return new GetAdvertisementQueryResponse
            {
                AddressDescription = advs.AddressDescription,
                Description = advs.Description,
                ImageUrl = advs.ImageUrl,
                Location = advs.Location,
                Title = advs.Title,
                UserName = advs.User.UserName,
                IsSucces = true

            };
        }
    }
}

