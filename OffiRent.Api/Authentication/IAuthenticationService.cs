using System.Threading.Tasks;
using OffiRent.Api.DataObjects.Authentication;
using OffiRent.Domain.Models.Identity;

namespace OffiRent.Api.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
        string GenerateJwtToken(User user);
    }
}