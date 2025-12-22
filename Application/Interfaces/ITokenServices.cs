using HelpPawApi.Domain.Entities.AppUser;

namespace HelpPawApi.Application.Interfaces
{
    public interface ITokenServices
    {
        public string CreateToken(AppUsers user);
        


    }
}
