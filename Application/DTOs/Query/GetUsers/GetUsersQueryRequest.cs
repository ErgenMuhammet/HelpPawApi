using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetUsers
{
    public class GetUsersQueryRequest : IRequest<GetUsersQueryResponse>
    {
    }
}
