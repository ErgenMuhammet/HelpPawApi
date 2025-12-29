using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

    namespace HelpPawApi.Application.DTOs.Query.GetUserAdvertisements
    {
        public class GetUserAdvertisementsQueryHandler : IRequestHandler<GetUserAdvertisementsQueryRequest, GetUserAdvertisementsQueryResponse>
        {
            private readonly IAppContext _context;
            private readonly UserManager<AppUsers> _userManager;


            public GetUserAdvertisementsQueryHandler(IAppContext context, UserManager<AppUsers> userManager)
            {
                _context = context;
                _userManager = userManager;

            }


            public async Task<GetUserAdvertisementsQueryResponse> Handle(
                                     GetUserAdvertisementsQueryRequest request,
                                               CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.EmailFromToken);

                if (user == null)
                {
                    return new GetUserAdvertisementsQueryResponse
                    {
                        IsSucces = false,
                        PastAdvertisements = null,
                        Message="Token okunamadı"
                    };
                }

            var Advs = new List<Advertisements>();

            if (user is User)
            {
               Advs = await _context.Advertisements
                    .Where(a => a.UserId == user.Id)
                    .OrderByDescending(a => a.CreatedTime).ToListAsync<Advertisements>(cancellationToken);


            }

            if (user is Vet)
            {
                Advs = await _context.Advertisements
                    .Where(a => a.VetId == user.Id)
                    .OrderByDescending(a => a.CreatedTime).ToListAsync<Advertisements>(cancellationToken);          
            }

            if (Advs.Count >= 0)
            {
                return new GetUserAdvertisementsQueryResponse
                {
                    PastAdvertisements = Advs,
                    IsSucces = true
                };
            }
            return new GetUserAdvertisementsQueryResponse
            {
                IsSucces = false,
                PastAdvertisements = null,
                Message = "görüntülenecek ilan bulunmamatadır"
            };


        }
        }
    }
   