using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.Interfaces
{
    public interface INotificationService
    {
       
        Task SendAsync(Notification notification); //Genel bildirim gönderme metodu
    }
}
