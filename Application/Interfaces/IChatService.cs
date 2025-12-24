using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.Interfaces
{
    public interface IChatService
    {
       Task SendToAllAsync(string user,string message);
       Task SendToUserAsync(string FromUser,string user,string message);
    }
}
