using MVCAppConsumingBAREAPI.Models.IdentityDTOs;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;
using MVCAppConsumingBAREAPI.Utilities;

namespace MVCAppConsumingBAREAPI.Web.Services
{
    // the AuthService class is responsible for calling the UserController in the API (with login and register)
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string authBaseUrl;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration) :
            base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            authBaseUrl = configuration.GetValue<string>("ServiceUrls:BAREAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginRequestDTO,
                // here we get "api/v1/UsersAuth" route from the data annotation in the controller
                // and we get "login" from the action (method) name data annotation
                Url = authBaseUrl + "/api/v1/UsersAuth/login"
            };

            return SendAsync<T>(apiRequest);
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO registerRequestDTO)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registerRequestDTO,
                Url = authBaseUrl + "/api/v1/UsersAuth/register"
            };

            return SendAsync<T>(apiRequest);
        }
    }
}
