using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace HelpPawApi.Application.DTOs.Query.GetAllAdvertisement
{
    public class GetAllAdvertisementQueryHandler : IRequestHandler<GetAllAdvertisementQueryRequest,GetAllAdvertisementQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly IAppContext _appContext;

        public GetAllAdvertisementQueryHandler(UserManager<AppUsers> userManager, IAppContext appContext)
        {
            _userManager = userManager;
            _appContext = appContext;
        }

        public async Task<GetAllAdvertisementQueryResponse> Handle(GetAllAdvertisementQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailFromToken);
            if (user == null)
            {
                return new GetAllAdvertisementQueryResponse 
                {
                    IsSucces= false,
                };
            }
            if (user.City is not null)
            {
              var advs = await _appContext.
                                        Advertisements.Include(x => x.User).
                                                          Where(x => x.AddressDescription.ToLower().Trim() == user.City.ToLower().Trim()).
                                                             ToListAsync<Advertisements>(cancellationToken);
                return new GetAllAdvertisementQueryResponse
                {
                    Advertisements = advs,
                    IsSucces = true,

                };
            }

            return new GetAllAdvertisementQueryResponse
            {
                IsSucces = false, 
            };
            

            
        }
    }
}
