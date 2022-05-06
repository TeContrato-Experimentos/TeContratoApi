using System.Threading.Tasks;
using TeContrato.API.Domain.Services.Communications;

namespace TeContrato.API.Domain.Services
{
    public interface IAccountService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
    }
}