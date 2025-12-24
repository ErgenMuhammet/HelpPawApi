using HelpPawApi.Domain.Entities.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetUsers
{
    public class GetUsersQueryResponse
    {
        public List<AppUsers> Users;
        public bool IsSucces { get; set; }
        public string Message { get; set; }

    }
}
