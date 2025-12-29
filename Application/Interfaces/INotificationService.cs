using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendVetInterestedAsync(string userId); //Kullanıcıya veteriner ilgisi hakkında bildirim gönder
        Task SendNewMessageAsync(string userId); //Kullanıcıya yeni bir mesaj hakkında bildirim gönder
    }
}
