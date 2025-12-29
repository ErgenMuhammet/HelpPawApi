using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.CreateUser
{
    public class CreateUserCommandRequest :IRequest<CreateUserCommandResponse>
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }  
        public string Password { get; set; }
        public string City{ get; set; }






    }
}
