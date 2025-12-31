    using HelpPawApi.Application.Interfaces;
    using HelpPawApi.Domain.Entities.Advertisement;
    using HelpPawApi.Domain.Entities.AppUser;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    namespace HelpPawApi.Application.DTOs.Command.CreateAdvertisement
    {
        public class CreateAdvertisementsCommandHandler : IRequestHandler<CreateAdvertisementsCommandRequest, CreateAdvertisementsCommandResponse>
    { 


        private readonly UserManager<AppUsers> _userManager;
            private readonly IAppContext _context;
            public CreateAdvertisementsCommandHandler(UserManager<AppUsers> userManager, IAppContext context)
            {
                _userManager = userManager;
                _context = context;
            }
            async Task<CreateAdvertisementsCommandResponse> IRequestHandler<CreateAdvertisementsCommandRequest, CreateAdvertisementsCommandResponse>.Handle(CreateAdvertisementsCommandRequest request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.EmailFromToken);

                if (user == null)
                {
                    return new CreateAdvertisementsCommandResponse
                    {
                        IsSucces = false,
                        Message = "Token işlenemediği için kullanıcı bulunamadı."
                    };
                }

                Advertisements advs = new Advertisements
                {
                    CreatedTime = DateTime.UtcNow,
                    Location = request.Location,
                    Title = request.Title,
                    ImageUrl = request.ImageUrl,
                    IsActive = true,
                    Description = request.Description,
                    AddressDescription = request.AddressDescription,
                    userName = user.UserName, 

                    UserId = user.Id,
                };

                try
                {
                    await _context.Advertisements.AddAsync(advs);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {

                    return new CreateAdvertisementsCommandResponse
                    {
                        IsSucces = false,
                        Message = "Ilan yayinlanamadi.",
                        Error = ex.Message,
                        AdvertisementId = advs.Id,
                        UserId = user.Id,

                    }; 
                }

                return new CreateAdvertisementsCommandResponse
                {
                    IsSucces = true,
                    Message = "Ilan başarıyla   yayınlandı.",
                    Error = null,
                    AdvertisementId = advs.Id,
                    UserId = user.Id,
                };

            }
        }
    }
