using HelpPawApi.Application.DTOs.Command.notification.CreateNotification;
using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Notification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.VetInterestedAdvertisement
{
    public class VetInterestedAdvertisementCommandHandler : IRequestHandler<VetInterestedAdvertisementCommandRequest, VetInterestedAdvertisementCommandResponse>
    {
        private readonly IAppContext _context;
        private readonly IMediator _mediator;

        public VetInterestedAdvertisementCommandHandler(IMediator mediator, IAppContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<VetInterestedAdvertisementCommandResponse> Handle(
                   VetInterestedAdvertisementCommandRequest request,
                   CancellationToken cancellationToken)
        {
            // ilanı bul
            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(x => x.Id.ToString() == request.AdvertisementId,cancellationToken);
            if (advertisement == null)
            {
                return new VetInterestedAdvertisementCommandResponse
                {
                    IsSuccess = false,
                    Message = "İlan bulunamadı."
                };
            }
            // ilan sahibine bildirim oluşturma
            await _mediator.Send(new CreateNotificationCommandRequest
            {
                UserId = advertisement.UserId,
                Title = "Veteriner İlgi Gösterdi",
                Message = "Bir veteriner ilanınıza ilgilendi. Detaylar için ilanı görüntüleyin.",
                Type = NotificationType.VetInterested,
            }, cancellationToken);

            return new VetInterestedAdvertisementCommandResponse
            {
                IsSuccess = true,
                Message = "İlan sahibine veteriner ilgisi bildirimi gönderildi."
            };
        }
    }
}
