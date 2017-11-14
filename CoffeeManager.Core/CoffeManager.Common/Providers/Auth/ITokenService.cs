using System;
using System.Threading.Tasks;
using System.Net;
namespace CoffeManager.Common
{
    public interface ITokenService
    {
        Task<OAuthToken> GetUserToken(ICredentials credintials);
    }
}
