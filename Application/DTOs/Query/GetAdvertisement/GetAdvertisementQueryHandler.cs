using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.AppUser;
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
            var advs =await _context.Advertisements.Include(x => x.User).Where(a => a.Id.ToString() == request.AdvertisementId.ToString()).FirstOrDefaultAsync();
                ;

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
                AdvsId = advs.Id.ToString(),
                Message = "İlan bilgileri başarı ile getirildi.",
                AddressDescription = advs.AddressDescription,
                Description = advs.Description,
                ImageUrl = advs.ImageUrl,
                Location = advs.Location,
                Title = advs.Title,
                UserName = advs.User.UserName,
                IsSucces = true,
                IsActive = advs.IsActive,
                CreatedDate = advs.CreatedTime

            };
        }
    }
}

