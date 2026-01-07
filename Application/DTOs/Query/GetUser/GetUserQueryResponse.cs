using HelpPawApi.Domain.Entities.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetUser
{
    public class GetUserQueryResponse
    {
        

        public List<UserDto>? User { get; set; }
        public bool IsSucces { get; set; }
        public string? Message { get; set; }

    }
    public class UserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
