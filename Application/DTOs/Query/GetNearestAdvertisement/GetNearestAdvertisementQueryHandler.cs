using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Locations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetNearestAdvertisement
{
    public class GetNearestAdvertisementQueryHandler : IRequestHandler<GetNearestAdvertisementQueryRequest, GetNearestAdvertisementQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly IAppContext _appContext;
        public GetNearestAdvertisementQueryHandler(UserManager<AppUsers> userManager, IAppContext appContext)
        {
            _appContext = appContext;
            _userManager = userManager;
        }
        public async Task<GetNearestAdvertisementQueryResponse> Handle(GetNearestAdvertisementQueryRequest request, CancellationToken cancellationToken)
        {

            var Vet = await _userManager.FindByEmailAsync(request.EmailFromToken);
            if (Vet == null || Vet.Location == null)
            {
                return new GetNearestAdvertisementQueryResponse
                {
                    Advertisements = null,
                    IsSucces = false,
                    Message = "Kullanıcı konumuna ulaşılamadığı için yakın ilanlar listelenemedi."
                };
            }

            List<Advertisements> Advs = await _appContext.Advertisements.
                                                 Include(x => x.Location).
                                                    Where(x => x.IsActive == true && x.Location != null).
                                                       ToListAsync(cancellationToken);

            var SortedAdvs = Advs.OrderBy(ad => CalculateDistance
                        (Vet.Location.latitude, Vet.Location.longitude, ad.Location.latitude, ad.Location.longitude)).ToList<Advertisements>();

            if (SortedAdvs.Count() > 0)
            {
                return new GetNearestAdvertisementQueryResponse
                {
                    Advertisements = SortedAdvs,
                    IsSucces = true,
                    Message = "İlanlar başarı ile getirildi"

                };         
            }

            return new GetNearestAdvertisementQueryResponse
            {
                Advertisements = null,
                IsSucces = false,
                Message = "İlanlar yüklenirken bir hata oldu."
            };

                
                
        }
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; 
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c; 

            return distance;
        }

        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}

