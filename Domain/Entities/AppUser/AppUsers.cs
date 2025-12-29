using HelpPawApi.Domain.Entities.Chat;
using HelpPawApi.Domain.Entities.Locations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Domain.Entities.AppUser
{
    public abstract class AppUsers : IdentityUser
    {
        public string FullName { get; set; }
        public string? PhotoUrl { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

     
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        
        public Location? Location { get; set; }

       
        public List<ChatMessage> ReceivedMessages{ get; set; }
        public List<ChatMessage> SendMessages { get; set; }



    }
}
