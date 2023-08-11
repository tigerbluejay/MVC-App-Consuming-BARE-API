using MVCAppConsumingBAREAPI.Models.IdentityDTOs;

namespace MVCAppConsumingBAREAPI.Web.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);

        Task<T> RegisterAsync<T>(RegistrationRequestDTO registerRequestDTO);
    }
}
